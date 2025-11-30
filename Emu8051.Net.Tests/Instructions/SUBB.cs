using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions.Add;
using Emu8051.Net.Core.Instructions.Subtract;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class SUBB
    {
        [Fact]
        public void BasicSub()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x01 });
            mem.Accumulator = 0x03;
            mem.ProgramCounter = 0x00;
            var instr = new SubtractLiteral();

            instr.Execute(mem);

            Assert.Equal(0x02, mem.Accumulator);
        }

        [Fact]
        public void SubWithCarry()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x54 }); // 84
            mem.Accumulator = 0xc9; // 201
            mem.Carry = true;
            mem.ProgramCounter = 0x00;
            var instr = new SubtractLiteral();

            instr.Execute(mem);

            Assert.Equal(0x74, mem.Accumulator); // 116
        }

        [Fact]
        public void ClearsCarry()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x01 });
            mem.Accumulator = 0x03;
            mem.ProgramCounter = 0x00;
            mem.Carry = true;
            var instr = new SubtractLiteral();

            instr.Execute(mem);

            Assert.False(mem.Carry);
        }

        [Fact]
        public void OverflowSub()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x01 });
            mem.Accumulator = 0x00;
            mem.ProgramCounter = 0x00;
            var instr = new SubtractLiteral();

            instr.Execute(mem);

            Assert.Equal(0xFF, mem.Accumulator);
            Assert.True(mem.Carry);
            Assert.True(mem.AuxCarry);
        }

        [Fact]
        public void AuxOverflowSub()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x0F });
            mem.Accumulator = 0x11;
            mem.ProgramCounter = 0x00;
            var instr = new SubtractLiteral();

            instr.Execute(mem);

            Assert.Equal(0x02, mem.Accumulator);
            Assert.False(mem.Carry);
            Assert.True(mem.AuxCarry);
        }

        [Fact]
        public void SignedOverflowPosSub()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0xFF }); // -1
            mem.Accumulator = 0x7F; // 127
            mem.ProgramCounter = 0x00;
            var instr = new SubtractLiteral();

            instr.Execute(mem);

            Assert.Equal(0x80, mem.Accumulator);
            Assert.True(mem.Carry);
            Assert.False(mem.AuxCarry);
            Assert.True(mem.SFR.Overflow);
        }

        [Fact]
        public void SignedOverflowNegSub()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x01 }); // 1
            mem.Accumulator = 0x80; // -128
            mem.ProgramCounter = 0x00;
            var instr = new SubtractLiteral();

            instr.Execute(mem);

            Assert.Equal(0x7f, mem.Accumulator);
            Assert.False(mem.Carry);
            Assert.True(mem.AuxCarry);
            Assert.True(mem.SFR.Overflow);
        }
    }
}