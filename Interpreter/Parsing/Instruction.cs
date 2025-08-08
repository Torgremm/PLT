namespace PLT.Interpreter;

internal class Instruction
{
    public string OpCode { get; }
    public string[] Operands { get; }

    public Instruction(string opCode, string[] operands)
    {
        OpCode = opCode;
        Operands = operands;
    }
}


