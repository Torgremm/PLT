using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{

    private static readonly PLCAddress dummyAddress = new PLCAddress("MW0");
    private DataVar GetDataVar(string c)
    {
        return c switch
        {
            "D" => DataVar.INT,
            "I" => DataVar.SHORT,
            "R" => DataVar.REAL,
            _ => throw new NotSupportedException($"Data type '{c}' is not supported.")
        };
    }

    public void ADD(string operand)
    {
        GetDataVar(operand).Accept(new MathOperationVisitor(this, dummyAddress, MathOperationType.ADD));
    }

    public void SUB(string operand)
    {
        GetDataVar(operand).Accept(new MathOperationVisitor(this, dummyAddress, MathOperationType.SUB));
    }

    public void MUL(string operand)
    {
        GetDataVar(operand).Accept(new MathOperationVisitor(this, dummyAddress, MathOperationType.MUL));
    }

    public void DIV(string operand)
    {
        GetDataVar(operand).Accept(new MathOperationVisitor(this, dummyAddress, MathOperationType.DIV));
    }

}
