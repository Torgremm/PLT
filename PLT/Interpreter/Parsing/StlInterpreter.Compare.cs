using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{

    public void EQ(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new CompareOperationVisitor(this, addr, ComparisonType.Equal));
    }

    public void NEQ(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new CompareOperationVisitor(this, addr, ComparisonType.NotEqual));
    }

    public void LEQ(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new CompareOperationVisitor(this, addr, ComparisonType.LessOrEqual));
    }

    public void GEQ(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new CompareOperationVisitor(this, addr, ComparisonType.GreaterOrEqual));
    }

    public void LT(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new CompareOperationVisitor(this, addr, ComparisonType.Less));
    }

    public void GT(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new CompareOperationVisitor(this, addr, ComparisonType.Greater));
    }

}
