namespace Assembler.Parser;

public class ParserException : Exception
{
    public ParserException(int lineNumber, string msg) : base(msg)
    {
        LineNumber = lineNumber;
    }
    
    public int LineNumber { get; }
}