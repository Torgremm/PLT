using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;

namespace PLT.Interpreter.Data.Operations;

internal class CompareOperationVisitor : AccumulatorOperationVisitorBase
{

    private readonly ComparisonType _type;
    public CompareOperationVisitor(StlInterpreter interpreter, PLCAddress addr, ComparisonType type) : base(interpreter, addr)
    {
        _type = type;
    }

    private bool Compare<T>(T left, T right) where T : IComparable<T>
    {
        int result = left.CompareTo(right);
        return _type switch
        {
            ComparisonType.Equal => result == 0,
            ComparisonType.NotEqual => result != 0,
            ComparisonType.Less => result < 0,
            ComparisonType.LessOrEqual => result <= 0,
            ComparisonType.Greater => result > 0,
            ComparisonType.GreaterOrEqual => result >= 0,
            _ => throw new InvalidOperationException($"Unknown comparison type: {_type}")
        };
    }

    public override void VisitInt()
    {
        var ac1 = Interpreter.GetIntAccumulator();
        var ac2 = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.INT);
        Interpreter.SetIntAccumulator2(ac2);

        Interpreter.SetBoolAccumulator(Compare(ac1, ac2));
    }

    public override void VisitWord()
    {
        var ac1 = Interpreter.GetIntAccumulator();
        var ac2 = Interpreter.GetMemory().GetValue<int>(Addr, DataVar.WORD);
        Interpreter.SetIntAccumulator2(ac2);

        Interpreter.SetBoolAccumulator(Compare(ac1, ac2));
    }

    public override void VisitDWord()
    {
        var ac1 = Interpreter.GetUIntAccumulator();
        var ac2 = Interpreter.GetMemory().GetValue<uint>(Addr, DataVar.DWORD);
        Interpreter.SetUIntAccumulator2(ac2);

        Interpreter.SetBoolAccumulator(Compare(ac1, ac2));
    }

    public override void VisitReal()
    {
        var ac1 = Interpreter.GetFloatAccumulator();
        var ac2 = Interpreter.GetMemory().GetValue<float>(Addr, DataVar.REAL);
        Interpreter.SetFloatAccumulator2(ac2);

        Interpreter.SetBoolAccumulator(Compare(ac1,ac2)); //Tolerance needed?
    }

    public override void VisitBool()
    {
        throw new InvalidOperationException("Cannot compare type bool");
    }
}
