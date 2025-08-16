using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using System;

namespace PLT.Tests.InterpreterUnitTests
{
    public class ShiftRotateTests
    {
        private StlInterpreter CreateInterpreter()
        {
            var memory = new MemoryModel();
            return new StlInterpreter(memory);
        }

        [Fact]
        public void SSI_ShouldShiftShortRight()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetShortAccumulator1((short)0b101010101010);
            interpreter.SSI("4");
            Assert.Equal((short)(0b101010101010 >> 4), interpreter.GetShortAccumulator1());
        }

        [Fact]
        public void SSD_ShouldShiftIntRight()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetIntAccumulator1(0xF0F0F0F);
            interpreter.SSD("8");
            Assert.Equal(0xF0F0F0F >> 8, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void SLW_ShouldShiftUShortLeft()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUShortAccumulator1(0x1234);
            interpreter.SLW("3");
            Assert.Equal((ushort)(0x1234 << 3), interpreter.GetUShortAccumulator1());
        }

        [Fact]
        public void SRW_ShouldShiftUShortRight()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUShortAccumulator1(0x1234);
            interpreter.SRW("2");
            Assert.Equal((ushort)(0x1234 >> 2), interpreter.GetUShortAccumulator1());
        }

        [Fact]
        public void SLD_ShouldShiftUIntLeft()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0x12345678u);
            interpreter.SLD("4");
            Assert.Equal(0x12345678u << 4, interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void SRD_ShouldShiftUIntRight()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0x12345678u);
            interpreter.SRD("8");
            Assert.Equal(0x12345678u >> 8, interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void RLD_ShouldRotateUIntLeft()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0x12345678u);
            interpreter.RLD("8");
            Assert.Equal((0x12345678u << 8) | (0x12345678u >> (32 - 8)), interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void RRD_ShouldRotateUIntRight()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0x12345678u);
            interpreter.RRD("8");
            Assert.Equal((0x12345678u >> 8) | (0x12345678u << (32 - 8)), interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void RLDA_ShouldRotateUIntLeft()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0xAABBCCDDu);
            interpreter.RLDA("16");
            Assert.Equal((0xAABBCCDDu << 16) | (0xAABBCCDDu >> (32 - 16)), interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void RRDA_ShouldRotateUIntRight()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0xAABBCCDDu);
            interpreter.RRDA("16");
            Assert.Equal((0xAABBCCDDu >> 16) | (0xAABBCCDDu << (32 - 16)), interpreter.GetUIntAccumulator1());
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void SSI_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.SSI(operand));
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void SSD_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.SSD(operand));
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void SLW_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.SLW(operand));
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void SRW_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.SRW(operand));
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void SLD_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.SLD(operand));
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void SRD_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.SRD(operand));
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void RLD_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.RLD(operand));
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void RRD_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.RRD(operand));
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void RLDA_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.RLDA(operand));
        }

        [Theory]
        [InlineData("notanumber")]
        [InlineData("")]
        public void RRDA_ShouldThrowOnInvalidOperand(string operand)
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.RRDA(operand));
        }
    }
}