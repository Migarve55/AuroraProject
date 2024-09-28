namespace Assembler.Model;

public class Program
{
    public Program(IEnumerable<Instruction> instructions)
    {
        Instructions = instructions;
    }

    public IEnumerable<Instruction> Instructions { get; }
}