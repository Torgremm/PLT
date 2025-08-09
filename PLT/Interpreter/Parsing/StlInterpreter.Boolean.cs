using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{

    public void LN(string operand)
    {
        _boolAccumulator = !_memory.GetBit(new PLCAddress(operand));
    }

    public void A(string operand)
    {
        _boolAccumulator &= _memory.GetBit(new PLCAddress(operand));
    }

    public void AN(string operand)
    {
        _boolAccumulator &= !_memory.GetBit(new PLCAddress(operand));
    }

    public void O(string operand)
    {
        _boolAccumulator |= _memory.GetBit(new PLCAddress(operand));
    }

    public void ON(string operand)
    {
        _boolAccumulator |= !_memory.GetBit(new PLCAddress(operand));
    }

    public void X(string operand)
    {
        _boolAccumulator ^= _memory.GetBit(new PLCAddress(operand));
    }

    public void NOT(string? operand = null)
    {
        _boolAccumulator = !_boolAccumulator;
    }

    public void SET(string? operand = null)
    {
        _boolAccumulator = true;
    }

    public void CLR(string? operand = null)
    {
        _boolAccumulator = false;
    }

    public void S(string operand)
    {
        _memory.SetBit(new PLCAddress(operand), true);
    }

    public void R(string operand)
    {
        _memory.SetBit(new PLCAddress(operand), false);
    }

}
