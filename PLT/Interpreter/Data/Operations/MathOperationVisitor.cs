using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;
using System.Numerics;


namespace PLT.Interpreter.Data.Operations;

internal class MathOperationVisitor : AccumulatorOperationVisitorBase
{
    private readonly MathOperationType _type;
    public MathOperationVisitor(StlInterpreter interpreter, PLCAddress addr, MathOperationType type) : base(interpreter, addr)
    {
        _type = type;
    }

    private T Perform<T>(T left, T right) where T : INumber<T>
    {
        return _type switch
        {
            MathOperationType.ADD => left + right,
            MathOperationType.SUB => left - right,
            MathOperationType.MUL => left * right,
            MathOperationType.DIV => left / right,
            _ => throw new InvalidOperationException($"Unknown operation type: {_type}")
        };
    }

    public override void VisitInt()
    {
        var ac1 = Interpreter.GetIntAccumulator1();
        var ac2 = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.INT);
        Interpreter.SetIntAccumulator2(ac2);
        Interpreter.SetIntAccumulator1(Perform(ac1, ac2));
    }

    public override void VisitWord()
    {
        var ac1 = Interpreter.GetIntAccumulator1();
        var ac2 = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.WORD);
        Interpreter.SetIntAccumulator2(ac2);
        Interpreter.SetIntAccumulator1(Perform(ac1, ac2));
    }

    public override void VisitDWord()
    {
        var ac1 = Interpreter.GetUIntAccumulator1();
        var ac2 = Interpreter.GetMemory().GetValue<uint>(Addr, DataVar.DWORD);
        Interpreter.SetUIntAccumulator2(ac2);
        Interpreter.SetUIntAccumulator1(Perform(ac1, ac2));
    }

    public override void VisitReal()
    {
        var ac1 = Interpreter.GetFloatAccumulator1();
        var ac2 = Interpreter.GetMemory().GetValue<float>(Addr, DataVar.REAL);
        Interpreter.SetFloatAccumulator2(ac2);
        Interpreter.SetFloatAccumulator1(Perform(ac1, ac2));
    }

    public override void VisitBool()
    {
        throw new InvalidOperationException("Cannot peform math operations on boolean types.");
    }
}
