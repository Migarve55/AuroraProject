using System.Text;
using Simulator.System;

namespace ConsoleSimulator;

public class ConsoleRunner
{
    public void RunProgram(ushort[] program)
    {
        var computer = new Computer();
        computer.WriteMemory(program);
        while (!computer.DoClockCycle())
        {
            PrintStatus(computer);
        }
        PrintStatus(computer);
    }

    private void PrintStatus(Computer computer)
    {
        var snapshot = computer.TakeStateSnapshot();
        Console.WriteLine(GetPrintableStatus(snapshot));
    }
    
    private string GetPrintableStatus(ComputerStateSnapshot snapshot)
    {
        StringBuilder sb = new StringBuilder();
        var pulse = snapshot.ClockPulse ? "High" : "Low";
        sb.AppendLine($"Cycle: {snapshot.ClockCycle} - {pulse}");
        sb.AppendLine($"PC: {snapshot.ProgramCounter:D4} - Step: {snapshot.Microstep:D2}/15");
        // IR
        sb.AppendLine($"IR: {snapshot.InstructionRegister:x4} ({snapshot.Instruction,-4})");
        // Stack
        if (snapshot.Stack.Length > 0)
        {
            sb.AppendLine($"SP: {snapshot.StackPointer:x4} ({snapshot.Stack.Last():x4})[{snapshot.Stack.Length}]");
        }
        else
        {
            sb.AppendLine($"SP: {snapshot.StackPointer:x4} (----)[0]");
        }
        // MAR
        sb.AppendLine($"MR: {snapshot.MemoryAddressRegister:x4} ({snapshot.MemoryPointerContent:x4})");
        // Registers
        sb.AppendLine($"RA: {snapshot.RegisterA:x4} - RB: {snapshot.RegisterB:x4}");
        sb.AppendLine("-------------------------");
        // Flags and Control word
        sb.AppendLine($"FLAG: {snapshot.LogicUnitFlags:F}");
        sb.AppendLine($"WORD: {snapshot.ControlWord:F}");
        sb.AppendLine("-------------------------");
        // Main bus
        string busStatus = $"[{snapshot.BusWriter}] -({snapshot.BusValue:x4})-> [{snapshot.BusReader}]";
        sb.AppendLine($"BUS: {busStatus}");
        return sb.ToString();
    }
}