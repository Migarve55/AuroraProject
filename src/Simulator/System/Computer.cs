using System.Text;

namespace Simulator.System;

public class Computer
{
    private readonly Bus _bus;
    
    private readonly Register _instructionRegister;   // IR
    private readonly Register _regA;                  // REG A
    private readonly Register _regB;                  // REG B      
    private readonly Register _memoryAddressRegister; // MAR

    private readonly Counter _programCounter;         // PC
    private readonly Counter _stackPointer;           // SP  
    
    private readonly Memory _memory;
    private readonly LogicUnit _logicUnit;
    private readonly ControlUnit _controlUnit;

    private bool _high;
    private long _cycle;

    public Computer()
    {
        // Create system bus
        _bus = new Bus();
        // Create registers
        _instructionRegister = new Register(_bus, "IR");
        _regA = new Register(_bus, "RA");
        _regB = new Register(_bus, "RB");
        _memoryAddressRegister = new Register(_bus, "MAR");
        // Create counters
        _programCounter = new Counter(_bus, "PC");
        _stackPointer = new Counter(_bus, "SP");
        // Create components
        _memory = new Memory(_bus, _memoryAddressRegister); 
        _logicUnit = new LogicUnit(_bus, _regA, _regB);
        _controlUnit = new ControlUnit(_instructionRegister, _logicUnit);
    }

    /// <summary>
    /// Runs a cycle of the clock
    /// </summary>
    /// <returns>If the simulator has stopped</returns>
    public bool DoClockCycle()
    {
        _high = !_high;
        if (_high)
        {
            _cycle++;
        }
        DoClockTick();
        return IsSet(ControlWord.HALT);
    }
    
    /// <summary>
    /// Simulates clock pulse
    /// </summary>
    private void DoClockTick()
    {
        // Update counter
        if (_high)
        {
            if (IsSet(ControlWord.PC_INC))
            {
                _programCounter.Increment();
            }

            // Change SP
            if (IsSet(ControlWord.SP_POP))
            {
                _stackPointer.Increment();
            }
        
            if (IsSet(ControlWord.SP_PUSH))
            {
                _stackPointer.Decrement();
            }
        }
        else
        {
            if (IsSet(ControlWord.END))
            {
                _controlUnit.ResetStep();
            }
            else
            {
                _controlUnit.Step();
            }
            _controlUnit.UpdateControlUnit();
        }
        
        // Set bus signals
        _bus.NewCycle();
        WriteToBus();
        ReadFromBus();
        // The bus is updated in the low cycles
        if (!_high)
        {
            _bus.EndCycle();
            // In case the IR has been modified
            _controlUnit.UpdateControlUnit();
        }

        // Update LU
        _logicUnit.SetOperationMode(IsSet(ControlWord.ALU_SUB));
        _logicUnit.DoOperation();
        if (_high && IsSet(ControlWord.FI))
        {
            _controlUnit.FlagsIn();
        }
        else
        {
            _controlUnit.UpdateControlUnit();
        }
    }

    public void Reset()
    {
        _cycle = 0;
        _high = false;
        _bus.Reset();
        _controlUnit.Reset();
        _logicUnit.Reset();
        _programCounter.Reset();
        _stackPointer.Reset();
        _instructionRegister.Reset();
        _regA.Reset();
        _regB.Reset();
        _memoryAddressRegister.Reset();
    }

    public void WriteMemory(ushort[] data)
    {
        _memory.Program(data);
    }

    public void WriteMemory(string data)
    {
        _memory.Program(data);
    }

    public ComputerStateSnapshot TakeStateSnapshot()
    {
        string instruction = _controlUnit.GetInstructionName();
        ushort memoryValue = _memory.PeekMemory(_memoryAddressRegister.Value);
        return new ComputerStateSnapshot(
                _cycle,
                _high,
                _controlUnit.MicroStep,
                _programCounter.Value,
                _instructionRegister.Value,
                instruction,
                _stackPointer.Value,
                GetStackSnapshot(),
                _memoryAddressRegister.Value,
                memoryValue,
                _regA.Value,
                _regB.Value,
                _controlUnit.ControlWord,
                _controlUnit.Flags,
                _bus.ReaderName,
                _bus.WriterName,
                _bus.Value
            );
    }

    private ushort[] GetStackSnapshot()
    {
        var stack = new List<ushort>();

        if (_stackPointer.Value == 0)
        {
            return stack.ToArray();
        }
        
        for (int address = Memory.MemSize - 1; address >= _stackPointer.Value; address--)
        {
            stack.Add(_memory.PeekMemory((ushort) address));
        }

        return stack.ToArray();
    }

    public ushort[] GetMemorySnapshot()
    {
        return _memory.GetCopy();
    }

    private void WriteToBus()
    {
        if (IsSet(ControlWord.MEM_OUT))
        {
            _memory.MemToBus();
        }
        
        if (IsSet(ControlWord.PC_OUT))
        {
            _programCounter.CounterToBus();
        }

        if (IsSet(ControlWord.SP_OUT))
        {
            _stackPointer.CounterToBus();
        }

        if (IsSet(ControlWord.ALU_OUT))
        {
            _logicUnit.OutToBus();
        }
        
        if (IsSet(ControlWord.RA_OUT))
        {
            _regA.RegToBus();
        }
    }

    private void ReadFromBus()
    {
        if (IsSet(ControlWord.MAR_IN))
        {
            _memoryAddressRegister.BusToReg();
        }
        
        if (IsSet(ControlWord.MEM_IN))
        {
            _memory.BusToMem();
        }

        if (IsSet(ControlWord.IR_IN))
        {
            _instructionRegister.BusToReg();
        }

        if (IsSet(ControlWord.PC_IN))
        {
            _programCounter.BusToCounter();
        }
        
        if (IsSet(ControlWord.RA_IN))
        {
            _regA.BusToReg();
        }
        
        if (IsSet(ControlWord.RB_IN))
        {
            _regB.BusToReg();
        }
        
        if (IsSet(ControlWord.SP_IN))
        {
            _stackPointer.BusToCounter();
        }
    }

    private bool IsSet(ControlWord controlWord)
    {
        return _controlUnit.ControlWord.HasFlag(controlWord);
    }
}