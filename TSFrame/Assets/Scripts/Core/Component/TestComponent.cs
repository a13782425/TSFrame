using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class TestComponent : IComponent, IReactiveComponent
{
    public Int64 CurrentId { get { return ComponentIds.TEST; } }

    [DataDriven]
    public string VALUE;
    [DataDriven]
    private string Test1;
    //[DataDriven]
    //public string Value { get { return GetValue<string>("value"); } set { SetValue("value", value); } }
    //[DataDriven]
    //public string Value { get; set; }
}
