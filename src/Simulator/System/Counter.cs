namespace Simulator.System;

public class Counter : Component
{
    private readonly Bus _bus;

    public Counter(Bus bus, string name)
    {
        _bus = bus;
        Name = name;
    }

    public ushort Value { get; private set; }

    public void BusToCounter()
    {
        Value = _bus.Read(this);
    }

    public void CounterToBus()
    {
        _bus.Write(Value, this);
    }
    
    public void Increment()
    {
        unchecked
        {
            Value++;
        }
    }

    public void Decrement()
    {
        unchecked
        {
            Value--;
        }
    }

    public void Reset()
    {
        Value = 0;
    }
}