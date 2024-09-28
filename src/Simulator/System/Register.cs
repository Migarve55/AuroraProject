namespace Simulator.System;

public class Register : Component
{
    private readonly Bus _bus;

    public Register(Bus bus, string name)
    {
        _bus = bus;
        Name = name;
    }

    public ushort Value { get; private set; }

    public void BusToReg()
    {
        Value = _bus.Read(this);
    }

    public void RegToBus()
    {
        _bus.Write(Value, this);
    }

    public void Reset()
    {
        Value = 0;
    }
}