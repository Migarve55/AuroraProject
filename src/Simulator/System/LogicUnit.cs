namespace Simulator.System;

public class LogicUnit : Component
{
    private readonly Bus _bus;
    private readonly Register _regA;
    private readonly Register _regB;

    private ushort _result;
    private bool _mode;

    public LogicUnit(Bus bus, Register regA, Register regB)
    {
        _bus = bus;
        _regA = regA;
        _regB = regB;
        Name = "ALU";
    }

    public Flags Flags { get; private set; }

    public void OutToBus()
    {
        _bus.Write(_result, this);
    }
    
    // Operations

    public void SetOperationMode(bool sub)
    {
        _mode = sub;
    }

    public void DoOperation()
    {
        Flags = 0;
        try
        {
            checked
            {
                DoOperationInternal();
            }
        }
        catch (OverflowException oe)
        {
            Flags |= Flags.OF;
        }
        if (_result == 0)
        {
            Flags |= Flags.ZF;
        }
    }

    private void DoOperationInternal()
    {
        if (!_mode)
        {
            _result = (ushort)(_regA.Value + _regB.Value);
        }
        else
        {
            _result = (ushort)(_regA.Value - _regB.Value);
        }
    }

    public void Reset()
    {
        _result = 0;
    }
}