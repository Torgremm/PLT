using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{

    public void LN(string operand)
    {
        SetIntAccumulator2(GetIntAccumulator1());
        SetRLO(!_memory.GetBit(new PLCAddress(operand)));
    }

    public void A(string operand)
    {
        SetRLO(GetRLO() & _memory.GetBit(new PLCAddress(operand)));
    }

    public void AN(string operand)
    {
        SetRLO(GetRLO() & !_memory.GetBit(new PLCAddress(operand)));
    }

    public void O(string operand)
    {
        SetRLO(GetRLO() | _memory.GetBit(new PLCAddress(operand)));
    }

    public void ON(string operand)
    {
        SetRLO(GetRLO() | !_memory.GetBit(new PLCAddress(operand)));
    }

    public void X(string operand)
    {
        SetRLO(GetRLO() ^ _memory.GetBit(new PLCAddress(operand)));
    }

    public void NOT(string? operand = null)
    {
        SetRLO(!GetRLO());
    }

    public void SET(string? operand = null)
    {
        SetRLO(true);
    }

    public void CLR(string? operand = null)
    {
        SetRLO(false);
    }

    // public void S(string operand)
    // {
    //     _memory.SetBit(new PLCAddress(operand), true);
    // }

    // public void R(string operand)
    // {
    //     _memory.SetBit(new PLCAddress(operand), false);
    // }

}
