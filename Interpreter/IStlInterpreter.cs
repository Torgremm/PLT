namespace PLT.Interpreter
{
    public interface IStlInterpreter
    {
        void LoadProgram(string stlCode);
        void SetInput(string address, bool value);
        void RunCycle();
        bool GetOutput(string address);
        void Reset();
    }
}
