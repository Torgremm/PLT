namespace PLT.Interpreter
{
    internal class Instruction
    {
        public string Opcode { get; set; }
        public string Operand { get; set; }

        public Instruction(string opcode, string operand)
        {
            Opcode = opcode;
            Operand = operand;
        }
    }
}
