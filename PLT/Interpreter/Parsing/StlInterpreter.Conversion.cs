using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{

    public void RND(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new ConversionOperationVisitor(this, addr, ConversionType.RND));
    }

    public void TRUNC(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new ConversionOperationVisitor(this, addr, ConversionType.TRUNC));
    }

    public void RND_UP(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new ConversionOperationVisitor(this, addr, ConversionType.RND_UP));
    }

    public void RND_DOWN(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new ConversionOperationVisitor(this, addr, ConversionType.RND_DOWN));
    }


}
