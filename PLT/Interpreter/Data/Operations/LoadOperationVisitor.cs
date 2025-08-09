using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;

namespace PLT.Interpreter.Data.Operations;
internal class LoadOperationVisitor : AccumulatorOperationVisitorBase
{
    public LoadOperationVisitor(StlInterpreter interpreter, PLCAddress addr) : base(interpreter, addr)
    {
    }

    public override void VisitBool()
    {
        var bit = Interpreter.GetMemory().GetBit(Addr);
        Interpreter.SetAccumulator1(bit);
    }

    public override void VisitInt()
    {
        var value = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.INT);
        Interpreter.SetIntAccumulator1(value);
    }

    public override void VisitWord()
    {
        var value = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.WORD);
        Interpreter.SetIntAccumulator1(value);
    }

    public override void VisitDWord()
    {
        var value = Interpreter.GetMemory().GetValue<uint>(Addr, DataVar.DWORD);
        Interpreter.SetUIntAccumulator1(value);
    }

    public override void VisitReal()
    {
        var value = Interpreter.GetMemory().GetValue<float>(Addr, DataVar.REAL);
        Interpreter.SetFloatAccumulator1(value);
    }
}
