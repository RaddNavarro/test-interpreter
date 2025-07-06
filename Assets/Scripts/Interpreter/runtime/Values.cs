#nullable enable
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

public enum ValueType
{
    Null,
    Number,
    Boolean,

}

public abstract class RuntimeVal
{
    public abstract ValueType type { get; }
}

public class NullVal : RuntimeVal
{
    [JsonConverter(typeof(StringEnumConverter))]
    public override ValueType type => ValueType.Null;
    public string? value { get; }

    public NullVal(string? value)
    {
        this.value = value;
    }

}
public class NumberVal : RuntimeVal
{
    [JsonConverter(typeof(StringEnumConverter))]
    public override ValueType type => ValueType.Number;
    public double value { get; }

    public NumberVal(double value)
    {
        this.value = value;
    }
}

public class BooleanVal : RuntimeVal
{
    [JsonConverter(typeof(StringEnumConverter))]
    public override ValueType type => ValueType.Boolean;
    public bool value { get; }

    public BooleanVal(bool value)
    {
        this.value = value;
    }
}
public class Values : MonoBehaviour
{
    public NumberVal MkNum(double n = 0)
    {
        return new NumberVal(n);
    }

    public NullVal MkNull()
    {
        return new NullVal(null);
    }

    public BooleanVal MkBool(bool b = true)
    {
        return new BooleanVal(b);
    }


}
