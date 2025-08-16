using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{

    //private static readonly PLCAddress dummyAddress = new PLCAddress("MW0");
    // private DataVar GetDataVar(string c)
    // {
    //     return c switch
    //     {
    //         "D" => DataVar.INT,
    //         "I" => DataVar.SHORT,
    //         "R" => DataVar.REAL,
    //         _ => throw new NotSupportedException($"Data type '{c}' is not supported.")
    //     };
    // }
    public void EQ(string operand)
    {
        GetDataVar(operand).Accept(new CompareOperationVisitor(this, dummyAddress, ComparisonType.Equal));
    }

    public void NEQ(string operand)
    {
        GetDataVar(operand).Accept(new CompareOperationVisitor(this, dummyAddress, ComparisonType.NotEqual));
    }

    public void LEQ(string operand)
    {
        GetDataVar(operand).Accept(new CompareOperationVisitor(this, dummyAddress, ComparisonType.LessOrEqual));
    }

    public void GEQ(string operand)
    {
        GetDataVar(operand).Accept(new CompareOperationVisitor(this, dummyAddress, ComparisonType.GreaterOrEqual));
    }

    public void LT(string operand)
    {
        GetDataVar(operand).Accept(new CompareOperationVisitor(this, dummyAddress, ComparisonType.Less));
    }

    public void GT(string operand)
    {
        GetDataVar(operand).Accept(new CompareOperationVisitor(this, dummyAddress, ComparisonType.Greater));
    }

}
