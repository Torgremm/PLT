using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;
using System.Numerics;

namespace PLT.Interpreter.Data.Operations;

internal class CompareOperationVisitor : AccumulatorOperationVisitorBase
{
    private readonly ComparisonType _type;
    public CompareOperationVisitor(StlInterpreter interpreter, PLCAddress addr, ComparisonType type) : base(interpreter, addr)
    {
        _type = type;
    }

    private bool CompareSigned<T>(T left, T right) where T : IBinaryInteger<T>
    {
        bool less = left < right;
        bool greater = left > right;
        bool equal = left == right;

        Interpreter.GetStatusFlags().CC0 = less || equal;
        Interpreter.GetStatusFlags().CC1 = greater || equal;

        return _type switch
        {
            ComparisonType.Equal => equal,
            ComparisonType.NotEqual => less || greater,
            ComparisonType.Less => less,
            ComparisonType.LessOrEqual => less || equal,
            ComparisonType.Greater => greater,
            ComparisonType.GreaterOrEqual => greater || equal,
            _ => throw new InvalidOperationException($"Unknown comparison type: {_type}")
        };
    }

    private bool CompareUnsigned<T>(T left, T right) where T : IBinaryInteger<T>, IUnsignedNumber<T>
    {
        bool less = left < right;
        bool greater = left > right;
        bool equal = left == right;

        Interpreter.GetStatusFlags().CC0 = less || equal;
        Interpreter.GetStatusFlags().CC1 = greater || equal;

        return _type switch
        {
            ComparisonType.Equal => equal,
            ComparisonType.NotEqual => less || greater,
            ComparisonType.Less => less,
            ComparisonType.LessOrEqual => less || equal,
            ComparisonType.Greater => greater,
            ComparisonType.GreaterOrEqual => greater || equal,
            _ => throw new InvalidOperationException($"Unknown comparison type: {_type}")
        };
    }

    private bool CompareReal(float left, float right)
    {
        bool less = left < right;
        bool greater = left > right;
        bool equal = left == right;

        Interpreter.GetStatusFlags().CC0 = less || equal;
        Interpreter.GetStatusFlags().CC1 = greater || equal;

        return _type switch
        {
            ComparisonType.Equal => equal,
            ComparisonType.NotEqual => less || greater,
            ComparisonType.Less => less,
            ComparisonType.LessOrEqual => less || equal,
            ComparisonType.Greater => greater,
            ComparisonType.GreaterOrEqual => greater || equal,
            _ => throw new InvalidOperationException($"Unknown comparison type: {_type}")
        };
    }

    public override void VisitInt()
    {
        var ac1 = Interpreter.GetIntAccumulator1();
        var ac2 = Interpreter.GetIntAccumulator2();

        Interpreter.SetRLO(CompareSigned(ac2, ac1));
    }

    public override void VisitShort()
    {
        var ac1 = Interpreter.GetShortAccumulator1();
        var ac2 = Interpreter.GetShortAccumulator2();

        Interpreter.SetRLO(CompareSigned(ac2, ac1));
    }

    public override void VisitWord()
    {
        var ac1 = Interpreter.GetUShortAccumulator1();
        var ac2 = Interpreter.GetUShortAccumulator2();

        Interpreter.SetRLO(CompareUnsigned(ac2, ac1));
    }

    public override void VisitDWord()
    {
        var ac1 = Interpreter.GetUIntAccumulator1();
        var ac2 = Interpreter.GetUIntAccumulator2();

        Interpreter.SetRLO(CompareUnsigned(ac2, ac1));
    }

    public override void VisitReal()
    {
        var ac1 = Interpreter.GetFloatAccumulator1();
        var ac2 = Interpreter.GetFloatAccumulator2();

        Interpreter.SetRLO(CompareReal(ac2,ac1));
    }

    public override void VisitBool()
    {
        throw new InvalidOperationException("Cannot compare type bool");
    }
}
