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

    public void XN(string operand)
    {
        SetRLO(GetRLO() ^ !_memory.GetBit(new PLCAddress(operand)));
    }

    public void NOT()
    {
        SetRLO(!GetRLO());
    }

    public void SET()
    {
        SetRLO(true);
    }

    public void CLR()
    {
        SetRLO(false);
    }

    public void SAVE()
    {
        GetStatusFlags().BR = GetRLO();
    }

    public void FP(string operand)
    {
        var addr = new PLCAddress(operand);
        bool prev = _memory.GetBit(addr);
        bool current = GetRLO();

        bool risingEdge = !prev && current;
        SetRLO(risingEdge);

        _memory.SetBit(addr, current);
    }

    public void FN(string operand)
    {
        var addr = new PLCAddress(operand);
        bool prev = _memory.GetBit(addr);
        bool current = GetRLO();

        bool fallingEdge = prev && !current;
        SetRLO(fallingEdge);

        _memory.SetBit(addr, current);
    }

}
