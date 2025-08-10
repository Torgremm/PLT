namespace PLT.Interpreter.Data;
internal interface IDataVarVisitor
{
    void VisitInt();   // signed 32-bit
    void VisitShort(); // signed 16-bit
    void VisitWord();  // unsigned 16-bit
    void VisitDWord(); // unsigned 32-bit
    void VisitReal();  // 32-bit float
    void VisitBool();  // boolean
}
