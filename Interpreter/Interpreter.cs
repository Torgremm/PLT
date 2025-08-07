namespace PLT.Interpreter
{
    public class StlInterpreter : IStlInterpreter
    {
        private readonly Parser _parser = new();
        private readonly MemoryModel _memory = new();
        private Executor _executor;
        private List<Instruction> _instructions = new();

        public StlInterpreter()
        {
            _executor = new Executor(_memory);
        }

        public void LoadProgram(string stlCode)
        {
            _instructions = _parser.Parse(stlCode);
            _memory.Reset();
        }

        public void SetInput(string address, bool value)
        {
            _memory.SetValue(address, value);
        }

        public void RunCycle()
        {
            _executor.ExecuteInstructions(_instructions);
        }

        public bool GetOutput(string address)
        {
            return _memory.GetValue(address);
        }

        public void Reset()
        {
            _memory.Reset();
        }
    }
}
