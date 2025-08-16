using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data;
using PLT.Interpreter;
using System.Text;

namespace PLT.Tests
{
    public class ParsingOperationsTests
    {
        [Fact]
        public void ParseAddress_ShouldReturnCorrectPLCAddress()
        {
            var address = new PLCAddress("MW10");
            Assert.Equal("SHORT 10", address.ToString());
        }

        [Fact]
        public void ParseBoolAddress_ShouldReturnCorrectType()
        {
            var address = new PLCAddress("M0.1");
            Assert.Equal("M0.1", address.ToString());
            Assert.True(address.DataType == DataVar.BOOL);
        }

        [Fact]
        public void ParseInstruction_ShouldRecognizeLoad()
        {
            var instructions = Parser.Parse("L MW0");
            Assert.Single(instructions.Instructions);
            Assert.Equal("L", instructions.Instructions[0].OpCode);
            Assert.Equal("MW0", instructions.Instructions[0].Operands[0]);
        }

        [Fact]
        public void ParseMultipleInstructions_ShouldReturnAll()
        {
            var code = new StringBuilder()
                .AppendLine("L MW0")
                .AppendLine("+I")
                .AppendLine("= MW2")
                .ToString();
            var instructions = Parser.Parse(code);
            Assert.Equal(3, instructions.Instructions.Count);
            Assert.Equal("L", instructions.Instructions[0].OpCode);
            Assert.Equal("ADD", instructions.Instructions[1].OpCode);
            Assert.Equal("STORE", instructions.Instructions[2].OpCode);
        }

        [Fact]
        public void ParseLabel_ShouldRecognizeLabel()
        {
            var instructions = Parser.Parse("START:\nL MW0\nNOP");
            Assert.True(instructions.Labels.Count > 0);
            Assert.Equal("START", instructions.Labels.Keys.First());
        }

        [Fact]
        public void ParseJumpInstruction_ShouldRecognizeJump()
        {
            var instructions = Parser.Parse("JC END");
            Assert.Single(instructions.Instructions);
            Assert.Equal("JC", instructions.Instructions[0].OpCode);
            Assert.Equal("END", instructions.Instructions[0].Operands[0]);
        }

        [Fact]
        public void ParseComment_ShouldIgnoreComment()
        {
            var code = new StringBuilder()
                .AppendLine("// This is a comment")
                .AppendLine("L MW0")
                .ToString();
            var instructions = Parser.Parse(code);
            Assert.Single(instructions.Instructions);
            Assert.Equal("L", instructions.Instructions[0].OpCode);
        }
    }
}