namespace Simulator.System;

public class Bus
{
    private Component? _writing;
    private Component? _reading;

    public ushort Value { get; private set; }

    public string WriterName => _writing?.Name ?? "---";
    public string ReaderName => _reading?.Name ?? "---";

    public void Write(ushort newValue, Component writer)
    {
        if (_writing is not null)
        {
            throw new InvalidOperationException($"Trying to write to bus from '{writer.Name}' but bus is already being written by '{_writing}'");
        }
        
        _writing = writer;
        Value = newValue;
    }

    public ushort Read(Component reader)
    {
        _reading = reader;
        return Value;
    }

    public void NewCycle()
    {
        _writing = null;
        _reading = null;
        Value = 0;
    }

    public void EndCycle()
    {
    }
    
    public void Reset()
    {
        Value = 0;
    }
}