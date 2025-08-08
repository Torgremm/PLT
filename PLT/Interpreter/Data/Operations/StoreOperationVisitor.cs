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
        Interpreter.GetMemory().SetBit(Addr, Interpreter.GetBoolAccumulator());
    }

    public override void VisitInt()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.INT, Interpreter.GetIntAccumulator());
    }

    public override void VisitWord()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.WORD, Interpreter.GetIntAccumulator());
    }

    public override void VisitDWord()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.DWORD, Interpreter.GetUIntAccumulator());
    }

    public override void VisitReal()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.REAL, Interpreter.GetFloatAccumulator());
    }
}
