namespace Assembler.Model;

public class Instruction
{
    public Instruction(string operationName)
    {
        OperationName = operationName;
    }

    public Instruction(string operationName, string parameter)
    {
        OperationName = operationName;
        Parameter = parameter;
    }

    public string OperationName { get; }
    public string? Parameter { get; }
    public string? Label { get; set; }
    
    // Meta data
    public int? LineNumber { get; set; }
}