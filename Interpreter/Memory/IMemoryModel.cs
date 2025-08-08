namespace PLT.Interpreter
{
    internal interface IMemoryModel
    {
        void RegisterVariable(DataVar variableType);

        void SetValue(PLCAddress address, DataVar type, object value);

        T GetValue<T>(PLCAddress address, DataVar type);

        void Reset();
    }
}
