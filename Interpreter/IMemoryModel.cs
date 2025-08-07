namespace PLT.Interpreter
{
    public interface IMemoryModel
    {
        void RegisterVariable(DataVar variableType);

        void SetValue<DataVar>(string address, DataVar value);

        DataVar GetValue<DataVar>(string address);

        void Reset();
    }
}
