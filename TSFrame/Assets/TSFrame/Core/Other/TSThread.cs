using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace TSFrame.ECS
{
    public class TSThread
    {
        private const int FALSE = 0;
        private const int TRUE = 1;

        private static TSThread _instance = null;
        public static TSThread Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TSThread();
                }
                return _instance;
            }
        }

        private Thread _currentThread = null;

        private Queue<int> _indexQueue = null;

        private List<ValueModel> _valueModelPool = null;

        private List<ValueModel> _mainThreadList = null;

        private List<ValueModel> _subThreadList = null;

        private int _valueLock = 0;

        public int OperatorLock = 0;

        /// <summary>
        /// 开始线程
        /// </summary>
        public void Run()
        {
            if (_currentThread != null)
            {
                _currentThread.Abort();
                _currentThread = null;
            }

            _currentThread = new Thread(new ThreadStart(ThreadLoop));
            _currentThread.IsBackground = true;

            if (Observer.IsPlaying)
            {
                _currentThread.Start();
            }
        }
        /// <summary>
        /// 停止线程
        /// </summary>
        public void Stop()
        {
            if (_currentThread != null)
            {
                _currentThread.Abort();
                _currentThread = null;
            }
        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="component"></param>
        /// <param name="propertyId"></param>
        /// <param name="value"></param>
        public void SetValue(Entity entity, NormalComponent component, int propertyId, object value)
        {
            Begin: if (Interlocked.CompareExchange(ref _valueLock, 1, 0) == FALSE)
            {
                ValueModel valueModel = null;
                if (_indexQueue.Count < 1)
                {
                    valueModel = new ValueModel(_valueModelPool.Count);
                    _valueModelPool.Add(null);
                }
                else
                {
                    int index = _indexQueue.Dequeue();
                    valueModel = _valueModelPool[index];
                    _valueModelPool[index] = null;
                }
                valueModel.CurrentEntity = entity;
                valueModel.CurrentComponent = component;
                valueModel.CurrentPropertyId = propertyId;
                valueModel.CurrentValue = value;
                _mainThreadList.Add(valueModel);
                Interlocked.Exchange(ref _valueLock, 0);
            }
            else
            {
                goto Begin;
            }
        }
        /// <summary>
        /// 替换
        /// </summary>
        private void Swap()
        {
            Begin: if (Interlocked.CompareExchange(ref _valueLock, 1, 0) == FALSE)
            {
                _subThreadList.AddRange(_mainThreadList);
                _mainThreadList.Clear();
                Interlocked.Exchange(ref _valueLock, 0);
            }
            else
            {
                goto Begin;
            }
        }

        private void ThreadLoop()
        {
            while (Observer.IsPlaying)
            {
                Swap();
                Begin: if (Interlocked.CompareExchange(ref OperatorLock, 1, 0) == FALSE)
                {
                    List<ValueModel> tempList = new List<ValueModel>(_subThreadList);
                    _subThreadList.Clear();

                    foreach (ValueModel item in tempList)
                    {
                        item.CurrentComponent.PropertyArray[item.CurrentPropertyId].Setter(item.CurrentEntity, item.CurrentComponent, item.CurrentValue);
                    }
                    Interlocked.Exchange(ref OperatorLock, 0);
                }
                else
                {
                    goto Begin;
                }
                Thread.Sleep((int)Observer.DeltaTime * 1000);
            }
        }

        #region ctor

        public TSThread()
        {
            _indexQueue = new Queue<int>();
            _valueModelPool = new List<ValueModel>();
            _mainThreadList = new List<ValueModel>();
            _subThreadList = new List<ValueModel>();
        }

        ~TSThread()
        {
            if (_currentThread != null)
            {
                _currentThread.Abort();
            }
        }

        #endregion
    }
}