namespace Simulator.System;

public class ComputerStateSnapshot
{
    public ComputerStateSnapshot(
        long clockCycle, 
        bool clockPulse,
        int microstep,
        ushort programCounter, 
        ushort instructionRegister, 
        string instruction, 
        ushort stackPointer, 
        ushort[] stack, 
        ushort memoryAddressRegister, 
        ushort memoryPointerContent, 
        ushort registerA, 
        ushort registerB, 
        ControlWord controlWord, 
        Flags logicUnitFlags, 
        string busReader, 
        string busWriter, 
        ushort busValue)
    {
        ClockCycle = clockCycle;
        ClockPulse = clockPulse;
        Microstep = microstep;
        ProgramCounter = programCounter;
        InstructionRegister = instructionRegister;
        Instruction = instruction;
        StackPointer = stackPointer;
        Stack = stack;
        MemoryAddressRegister = memoryAddressRegister;
        MemoryPointerContent = memoryPointerContent;
        RegisterA = registerA;
        RegisterB = registerB;
        ControlWord = controlWord;
        LogicUnitFlags = logicUnitFlags;
        BusReader = busReader;
        BusWriter = busWriter;
        BusValue = busValue;
    }

    public long ClockCycle { get; }
    public bool ClockPulse { get; }
    public ushort ProgramCounter { get; }
    public int Microstep { get; }
    // Ir
    public ushort InstructionRegister { get; }
    public string Instruction { get; }
    // Stack
    public ushort StackPointer { get; }
    public ushort[] Stack { get; }
    // Registers
    public ushort MemoryAddressRegister { get; }
    public ushort MemoryPointerContent { get; }
    public ushort RegisterA { get; }
    public ushort RegisterB { get; }
    // Flags
    public ControlWord ControlWord { get; }
    public Flags LogicUnitFlags { get; }
    // Bus
    public string BusReader { get; }
    public string BusWriter { get; }
    public ushort BusValue { get; }
}