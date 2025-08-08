using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;

namespace PLT.Interpreter.Data.Operations;

internal class SubOperationVisitor : AccumulatorOperationVisitorBase
{
    public SubOperationVisitor(StlInterpreter interpreter, PLCAddress addr) : base(interpreter, addr)
    {
    }

    public override void VisitInt()
    {
        var left = Interpreter.GetIntAccumulator();
        var right = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.INT);
        Interpreter.SetIntAccumulator(left - right);
    }

    public override void VisitWord()
    {
        var left = Interpreter.GetIntAccumulator();
        var right = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.WORD);
        Interpreter.SetIntAccumulator(left - right);
    }

    public override void VisitDWord()
    {
        var left = Interpreter.GetUIntAccumulator();
        var right = Interpreter.GetMemory().GetValue<uint>(Addr, DataVar.DWORD);
        Interpreter.SetUIntAccumulator(left - right);
    }

    public override void VisitReal()
    {
        var left = Interpreter.GetFloatAccumulator();
        var right = Interpreter.GetMemory().GetValue<float>(Addr, DataVar.REAL);
        Interpreter.SetFloatAccumulator(left - right);
    }

    public override void VisitBool()
    {
        throw new InvalidOperationException("Cannot subtract type bool");
    }
}
