namespace PLT.Interpreter;

internal interface IStlInstructionSet
{
    void LD(string operand);
    void LDN(string operand);
    void A(string operand);
    void AN(string operand);
    void O(string operand);
    void ON(string operand);
    void X(string operand);
    void NOT(string? operand = null);
    void STORE(string operand);  // instead of '='
    void SET(string operand);
    void R(string operand);
    void NOP(string? operand = null);
    void MOV(string operandSrc, string operandDst);
}
