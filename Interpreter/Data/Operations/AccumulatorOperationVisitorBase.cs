using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;


namespace PLT.Interpreter.Data.Operations;
abstract class AccumulatorOperationVisitorBase : IDataVarVisitor
{
    protected readonly StlInterpreter Interpreter;
    protected readonly PLCAddress Addr;

    protected AccumulatorOperationVisitorBase(StlInterpreter interpreter, PLCAddress addr)
    {
        Interpreter = interpreter;
        Addr = addr;
    }

    public abstract void VisitInt();
    public abstract void VisitWord();
    public abstract void VisitDWord();
    public abstract void VisitReal();
    public abstract void VisitBool();
}
