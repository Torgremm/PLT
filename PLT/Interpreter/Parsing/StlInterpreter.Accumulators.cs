using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{
    public void TAK()
    {
        int value = GetIntAccumulator1();
        SetIntAccumulator1(GetIntAccumulator2());
        SetIntAccumulator2(value);
    }

    public void PUSH()
    {
        SetIntAccumulator2(GetIntAccumulator1());
    }

    public void POP()
    {
        SetIntAccumulator1(GetIntAccumulator2());
    }

    public void INC(string operand)
    {
        if (!int.TryParse(operand, out var step) || step < 0 || step > 255)
            throw new ArgumentException("INC expects an 8-bit unsigned constant (0..255).");

        SetByteAccumulator1((GetByteAccumulator1() + step) & 0xFF);
    }

    public void INC()
    {
        INC("1");
    }

    public void DEC(string operand)
    {
        if (!int.TryParse(operand, out var step) || step < 0 || step > 255)
            throw new ArgumentException("DEC expects an 8-bit unsigned constant (0..255).");

        SetByteAccumulator1((GetByteAccumulator1() - step) & 0xFF);
    }
    
    public void DEC()
    {
        DEC("1");
    }

}
