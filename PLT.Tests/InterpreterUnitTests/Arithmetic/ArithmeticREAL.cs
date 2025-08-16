using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data;
using PLT.Interpreter;
using System;

namespace PLT.Tests.InterpreterUnitTests
{
    public class ArithmeticRealTest
    {
        private StlInterpreter CreateInterpreter(float left, float right)
        {
            MemoryModel memory = new MemoryModel();
            var interpreter = new StlInterpreter(memory);
            interpreter.SetFloatAccumulator1(left);
            interpreter.SetFloatAccumulator2(right);
            return interpreter;
        }

        [Fact]
        public void Add_ShouldSumValues()
        {
            var interpreter = CreateInterpreter(5,2);
            interpreter.ADD("R");
            Assert.Equal(7, interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void Sub_ShouldSubtractValues()
        {
            var interpreter = CreateInterpreter(2,8);
            interpreter.SUB("R");
            Assert.Equal(6, interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void Mul_ShouldMultiplyValues()
        {
            var interpreter = CreateInterpreter(3,7);
            interpreter.MUL("R");
            Assert.Equal(21, interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void Div_ShouldDivideValues()
        {
            var interpreter = CreateInterpreter(3,6);
            interpreter.DIV("R");
            Assert.Equal(2, interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void Abs_ShouldReturnAbsoluteValue()
        {
            var interpreter = CreateInterpreter(-7,0);
            interpreter.ABS();
            Assert.Equal(7, interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void Sqr_ShouldSquareValue()
        {
            var interpreter = CreateInterpreter(4,0);
            interpreter.SQR();
            Assert.Equal(16, interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void Sqrt_ShouldReturnSquareRoot()
        {
            var interpreter = CreateInterpreter(9,0);
            interpreter.SQRT();
            Assert.Equal(3, interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void Sqrt_NegativeValue_ShouldThrow()
        {
            var interpreter = CreateInterpreter(-1,0);
            Assert.Throws<InvalidOperationException>(() => interpreter.SQRT());
        }

        [Fact]
        public void Exp_ShouldReturnExponential()
        {
            var interpreter = CreateInterpreter(2,0);
            interpreter.EXP();
            Assert.Equal(MathF.Exp(2), interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void Ln_ShouldReturnNaturalLog()
        {
            var interpreter = CreateInterpreter(MathF.E, 0);
            interpreter.LN();
            Assert.Equal(1, interpreter.GetFloatAccumulator1(), 3);
        }

        [Fact]
        public void Ln_NonPositiveValue_ShouldThrow()
        {
            var interpreter = CreateInterpreter(0,0);
            Assert.Throws<InvalidOperationException>(() => interpreter.LN());
        }

        [Fact]
        public void Sin_ShouldReturnSine()
        {
            var interpreter = CreateInterpreter(MathF.PI / 2,0);
            interpreter.SIN();
            Assert.Equal(1, interpreter.GetFloatAccumulator1(), 3);
        }

        [Fact]
        public void Cos_ShouldReturnCosine()
        {
            var interpreter = CreateInterpreter(0,0);
            interpreter.COS();
            Assert.Equal(1, interpreter.GetFloatAccumulator1(), 3);
        }

        [Fact]
        public void Tan_ShouldReturnTangent()
        {
            var interpreter = CreateInterpreter(0,0);
            interpreter.TAN();
            Assert.Equal(0, interpreter.GetFloatAccumulator1(), 3);
        }

        [Fact]
        public void Asin_ValidRange_ShouldReturnArcsine()
        {
            var interpreter = CreateInterpreter(1,0);
            interpreter.ASIN();
            Assert.Equal(MathF.PI / 2, interpreter.GetFloatAccumulator1(), 3);
        }

        [Fact]
        public void Asin_OutOfRange_ShouldThrow()
        {
            var interpreter = CreateInterpreter(2,0);
            Assert.Throws<InvalidOperationException>(() => interpreter.ASIN());
        }

        [Fact]
        public void Acos_ValidRange_ShouldReturnArccosine()
        {
            var interpreter = CreateInterpreter(1,0);
            interpreter.ACOS();
            Assert.Equal(0, interpreter.GetFloatAccumulator1(), 3);
        }

        [Fact]
        public void Acos_OutOfRange_ShouldThrow()
        {
            var interpreter = CreateInterpreter(-2,0);
            Assert.Throws<InvalidOperationException>(() => interpreter.ACOS());
        }

        [Fact]
        public void Atan_ShouldReturnArctangent()
        {
            var interpreter = CreateInterpreter(1,0);
            interpreter.ATAN();
            Assert.Equal(MathF.Atan(1), interpreter.GetFloatAccumulator1(), 3);
        }
    }
}