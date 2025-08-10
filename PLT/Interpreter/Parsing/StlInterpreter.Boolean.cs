using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{

     public void LN(string operand)
    {
        SetAccumulator1(!_memory.GetBit(new PLCAddress(operand)));
    }

    public void A(string operand)
    {
        SetAccumulator1(GetAccumulator1() & _memory.GetBit(new PLCAddress(operand)));
    }

    public void AN(string operand)
    {
        SetAccumulator1(GetAccumulator1() & !_memory.GetBit(new PLCAddress(operand)));
    }

    public void O(string operand)
    {
        SetAccumulator1(GetAccumulator1() | _memory.GetBit(new PLCAddress(operand)));
    }

    public void ON(string operand)
    {
        SetAccumulator1(GetAccumulator1() | !_memory.GetBit(new PLCAddress(operand)));
    }

    public void X(string operand)
    {
        SetAccumulator1(GetAccumulator1() ^ _memory.GetBit(new PLCAddress(operand)));
    }

    public void NOT(string? operand = null)
    {
        SetAccumulator1(!GetAccumulator1());
    }

    public void SET(string? operand = null)
    {
        SetAccumulator1(true);
    }

    public void CLR(string? operand = null)
    {
        SetAccumulator1(false);
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
