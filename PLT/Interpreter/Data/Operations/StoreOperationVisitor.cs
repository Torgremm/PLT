using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;


namespace PLT.Interpreter.Data.Operations;

internal class StoreOperationVisitor : AccumulatorOperationVisitorBase
{
    public StoreOperationVisitor(StlInterpreter interpreter, PLCAddress addr) : base(interpreter, addr)
    {
    }

    public override void VisitBool()
    {
        Interpreter.GetMemory().SetBit(Addr, Interpreter.GetAccumulator1());
    }

    public override void VisitInt()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.INT, Interpreter.GetIntAccumulator1());
    }

    public override void VisitShort()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.SHORT, Interpreter.GetShortAccumulator1());
    }

    public override void VisitWord()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.WORD, Interpreter.GetUShortAccumulator1());
    }

    public override void VisitDWord()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.DWORD, Interpreter.GetUIntAccumulator1());
    }

    public override void VisitReal()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.REAL, Interpreter.GetFloatAccumulator1());
    }
}
