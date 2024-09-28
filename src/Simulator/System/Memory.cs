namespace Simulator.System;

public class Memory : Component
{
    public const int MemSize = ushort.MaxValue + 1;

    private readonly Bus _bus;
    private readonly Register _addressRegister;

    private ushort[] _mem = new ushort[MemSize];

    public Memory(Bus bus, Register addressRegister)
    {
        _bus = bus;
        _addressRegister = addressRegister;
        Name = "MEM";
    }

    public ushort PeekMemory(ushort address)
    {
        return _mem[address];
    }

    public void MemToBus()
    {
        ushort address = _addressRegister.Value;
        _bus.Write(_mem[address], this);
    }

    public void BusToMem()
    {
        ushort address = _addressRegister.Value;
        _mem[address] = _bus.Read(this);
    }

    public void ClearMemory()
    {
        _mem = new ushort[MemSize];
    }

    public void Program(ushort[] data)
    {
        OverwriteMemory(data);
    }
    
    public void Program(string data)
    {
        if (data.Length % 4 != 0)
        {
            throw new InvalidOperationException("input data length must be multiple of 4");
        }

        int dataLenght = data.Length / 4;
        ushort[] values = new ushort[dataLenght];
        for (int i = 0; i < dataLenght; i++)
        {
            string fragment = data.Substring(i * 4, 4);
            ushort value = (ushort)Convert.ToInt16(fragment, 16);
            values[i] = value;
        }
        OverwriteMemory(values);
    }

    private void OverwriteMemory(ushort[] data)
    {
        ClearMemory();
        data.CopyTo(_mem, 0);
    }

    public ushort[] GetCopy()
    {
        ushort[] copy = new ushort[_mem.Length];
        _mem.CopyTo(copy, _mem.Length);
        return copy;
    }
}