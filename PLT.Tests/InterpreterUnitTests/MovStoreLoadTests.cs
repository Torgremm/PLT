// filepath: c:\Users\tormo\Documents\repo\PLT\PLT.Tests\InterpreterUnitTests\MovStoreLoadTestsTest.cs
using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data;
using System;

namespace PLT.Tests.InterpreterUnitTests
{
    public class MovStoreLoadTestsTest
    {
        private MemoryModel CreateMemory()
        {
            var memory = new MemoryModel();
            memory.RegisterVariable(DataVar.BOOL, true); // M0.0 - Bool
            memory.RegisterVariable(DataVar.SHORT, 0x1234); // MW2 - Short
            memory.RegisterVariable(DataVar.WORD, 0x5678); // MW4 - Word
            memory.RegisterVariable(DataVar.DWORD, 0x87654321u); // MD8 - DWord
            memory.RegisterVariable(DataVar.INT, 0x1234567); // MD12 - DInt
            memory.RegisterVariable(DataVar.REAL, 3.14f); // MD16 - Real

            return memory;
        }

        private StlInterpreter CreateInterpreter()
        {
            return new StlInterpreter(CreateMemory());
        }

        [Fact]
        public void L_ShouldLoadBool()
        {
            var interpreter = CreateInterpreter();
            interpreter.L("M0.0");
            Assert.True(interpreter.GetAccumulator1());
        }

        [Fact]
        public void L_ShouldLoadInt()
        {
            var interpreter = CreateInterpreter();
            interpreter.L("MD12");
            Assert.Equal(0x1234567u, interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void L_ShouldLoadShort()
        {
            var interpreter = CreateInterpreter();
            interpreter.L("MW2");
            Assert.Equal(0x1234u, interpreter.GetUShortAccumulator1());
        }

        [Fact]
        public void L_ShouldLoadWord()
        {
            var interpreter = CreateInterpreter();
            interpreter.L("MW4");
            Assert.Equal(0x5678, interpreter.GetUShortAccumulator1());
        }

        [Fact]
        public void L_ShouldLoadDWord()
        {
            var interpreter = CreateInterpreter();
            interpreter.L("MD8");
            Assert.Equal(0x87654321u, interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void L_ShouldLoadReal()
        {
            var interpreter = CreateInterpreter();
            interpreter.L("MD16");
            Assert.Equal(BitConverter.SingleToInt32Bits(3.14f), interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void STORE_ShouldStoreBool()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetAccumulator1(false);
            interpreter.STORE("M0.0");
            Assert.False(interpreter.GetMemory().GetBit(new PLCAddress("M0.0")));
        }

        [Fact]
        public void STORE_ShouldStoreInt()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0x01234321);
            interpreter.STORE("MD5");
            Assert.Equal(0x01234321u, interpreter.GetMemory().GetValue<uint>(new PLCAddress("MD5"), DataVar.INT));
        }

        [Fact]
        public void STORE_ShouldStoreShort()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUShortAccumulator1(0x1234);
            interpreter.STORE("MW2");
            Assert.Equal(0x1234, interpreter.GetMemory().GetValue<ushort>(new PLCAddress("MW2"), DataVar.SHORT));
        }

        [Fact]
        public void STORE_ShouldStoreWord()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUShortAccumulator1(0x4321);
            interpreter.STORE("MW4");
            Assert.Equal(0x4321, interpreter.GetMemory().GetValue<ushort>(new PLCAddress("MW4"), DataVar.WORD));
        }

        [Fact]
        public void STORE_ShouldStoreDWord()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0xCAFEBABE);
            interpreter.STORE("MD6");
            Assert.Equal(0xCAFEBABE, interpreter.GetMemory().GetValue<uint>(new PLCAddress("MD6"), DataVar.DWORD));
        }

        [Fact]
        public void STORE_ShouldStoreReal()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetFloatAccumulator1(2.71f);
            interpreter.STORE("MD8");
            var stored = interpreter.GetMemory().GetValue<float>(new PLCAddress("MD8"), DataVar.REAL);
            Assert.Equal(BitConverter.SingleToUInt32Bits(2.71f), BitConverter.SingleToUInt32Bits(stored));
        }

        [Fact]
        public void MOV_ShouldMoveBool()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetAccumulator1(true);
            interpreter.MOV("M0.0", "M0.1");
            Assert.True(interpreter.GetMemory().GetBit(new PLCAddress("M0.1")));
        }

        [Fact]
        public void MOV_ShouldMoveInt()
        {
            var interpreter = CreateInterpreter();
            interpreter.MOV("MD12", "MD2");
            Assert.Equal(0x1234567, interpreter.GetMemory().GetValue<int>(new PLCAddress("MD2"), DataVar.INT));
        }

        [Fact]
        public void MOV_ShouldMoveShort()
        {
            var interpreter = CreateInterpreter();
            interpreter.MOV("MW2", "MW12");
            Assert.Equal(0x1234, interpreter.GetMemory().GetValue<ushort>(new PLCAddress("MW12"), DataVar.SHORT));
        }

        [Fact]
        public void MOV_ShouldMoveWord()
        {
            var interpreter = CreateInterpreter();
            interpreter.MOV("MW4", "MW14");
            Assert.Equal(0x5678, interpreter.GetMemory().GetValue<ushort>(new PLCAddress("MW14"), DataVar.WORD));
        }

        [Fact]
        public void MOV_ShouldMoveDWord()
        {
            var interpreter = CreateInterpreter();
            interpreter.MOV("MD8", "MD9");
            Assert.Equal(0x87654321u, interpreter.GetMemory().GetValue<uint>(new PLCAddress("MD9"), DataVar.DWORD));
        }

        [Fact]
        public void MOV_ShouldMoveReal()
        {
            var interpreter = CreateInterpreter();
            interpreter.MOV("MD16", "MD1");
            var stored = interpreter.GetMemory().GetValue<float>(new PLCAddress("MD1"), DataVar.REAL);
            Assert.Equal(3.14f, stored);
        }
    }
}