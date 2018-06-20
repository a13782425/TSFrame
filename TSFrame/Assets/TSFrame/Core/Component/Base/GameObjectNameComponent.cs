using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GameObjectNameComponent : IComponent, IReactiveComponent
{
    public long CurrentId
    {
        get
        {
            return OperatorIds.GAME_OBJECT_NAME;
        }
    }
    [DataDriven]
    private string name;
}

