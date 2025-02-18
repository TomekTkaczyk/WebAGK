using WebAGK.Module.Agents.Core.Exceptions;

namespace WebAGK.Module.Agents.Core.ValueObjects;

public sealed record RpuId {
    
    private readonly string _value;

    public RpuId(string value) {
        if (string.IsNullOrWhiteSpace(value)) {
            throw new InvalidRpuIdException(value);
        }
        
        _value = value;
    }
    
    public static implicit operator string(RpuId data) => data._value;

    public static implicit operator RpuId(string value) => new(value);
}