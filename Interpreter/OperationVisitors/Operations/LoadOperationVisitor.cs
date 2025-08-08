namespace PLT.Interpreter;

internal class LoadOperationVisitor : AccumulatorOperationVisitorBase
{
    public LoadOperationVisitor(StlInterpreter interpreter, PLCAddress addr) : base(interpreter, addr)
    {
    }

    public override void VisitBool()
    {
        var bit = Interpreter.GetMemory().GetBit(Addr);
        Interpreter.SetBoolAccumulator(bit);
    }

    public override void VisitInt()
    {
        var value = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.INT);
        Interpreter.SetIntAccumulator(value);
    }

    public override void VisitWord()
    {
        var value = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.WORD);
        Interpreter.SetIntAccumulator(value);
    }

    public override void VisitDWord()
    {
        var value = Interpreter.GetMemory().GetValue<uint>(Addr, DataVar.DWORD);
        Interpreter.SetUIntAccumulator(value);
    }

    public override void VisitReal()
    {
        var value = Interpreter.GetMemory().GetValue<float>(Addr, DataVar.REAL);
        Interpreter.SetFloatAccumulator(value);
    }
}
