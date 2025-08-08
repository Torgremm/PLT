namespace PLT.Interpreter.Data;
internal interface IDataVarVisitor
{
    void VisitInt();
    void VisitWord();
    void VisitDWord();
    void VisitReal();
    void VisitBool();
}
