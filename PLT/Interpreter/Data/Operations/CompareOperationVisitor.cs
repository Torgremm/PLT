using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;

namespace PLT.Interpreter.Data.Operations;

internal class CompareOperationVisitor : AccumulatorOperationVisitorBase
{
    private const float eps = 0.00001f;
    private readonly ComparisonType _type;
    public CompareOperationVisitor(StlInterpreter interpreter, PLCAddress addr, ComparisonType type) : base(interpreter, addr)
    {
        _type = type;
    }

    private bool Compare(float left, float right)
    {
        float diff = Math.Abs(left - right);
        return _type switch
        {
            ComparisonType.Equal => diff < eps,
            ComparisonType.NotEqual => diff >= eps,
            ComparisonType.Less => left < right && diff >= eps,
            ComparisonType.LessOrEqual => left < right || diff < eps,
            ComparisonType.Greater => left > right && diff >= eps,
            ComparisonType.GreaterOrEqual => left > right || diff < eps,
            _ => throw new InvalidOperationException($"Unknown comparison type: {_type}")
        };
    }

    public override void VisitInt()
    {
        var ac1 = Interpreter.GetIntAccumulator1();
        var ac2 = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.INT);
        Interpreter.SetIntAccumulator2(ac2);

        Interpreter.SetAccumulator1(Compare(ac1, ac2));
    }

    public override void VisitWord()
    {
        var ac1 = Interpreter.GetIntAccumulator1();
        var ac2 = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.WORD);
        Interpreter.SetIntAccumulator2(ac2);

        Interpreter.SetAccumulator1(Compare(ac1, ac2));
    }

    public override void VisitDWord()
    {
        var ac1 = Interpreter.GetUIntAccumulator1();
        var ac2 = Interpreter.GetMemory().GetValue<uint>(Addr, DataVar.DWORD);
        Interpreter.SetUIntAccumulator2(ac2);

        Interpreter.SetAccumulator1(Compare(ac1, ac2));
    }

    public override void VisitReal()
    {
        var ac1 = Interpreter.GetFloatAccumulator1();
        var ac2 = Interpreter.GetMemory().GetValue<float>(Addr, DataVar.REAL);
        Interpreter.SetFloatAccumulator2(ac2);

        Interpreter.SetAccumulator1(Compare(ac1,ac2));
    }

    public override void VisitBool()
    {
        throw new InvalidOperationException("Cannot compare type bool");
    }
}
