using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{
    public void ADD(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new MathOperationVisitor(this, addr, MathOperationType.ADD));
    }

    public void SUB(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new MathOperationVisitor(this, addr, MathOperationType.SUB));
    }

    public void MUL(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new MathOperationVisitor(this, addr, MathOperationType.MUL));
    }

    public void DIV(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new MathOperationVisitor(this, addr, MathOperationType.DIV));
    }

}
