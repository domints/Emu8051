using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions.Add;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class ADD
    {
        [Fact]
        public void BasicAdd()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x01 });
            mem.Accumulator = 0x01;
            mem.ProgramCounter = 0x00;
            var instr = new AddLiteral();

            instr.Execute(mem);

            Assert.Equal(0x02, mem.Accumulator);
        }

        [Fact]
        public void BasicCarryAdd()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x01 });
            mem.Accumulator = 0x01;
            mem.Carry = true;
            mem.ProgramCounter = 0x00;
            var instr = new AddCarryLiteral();

            instr.Execute(mem);

            Assert.Equal(0x03, mem.Accumulator);
        }

        [Fact]
        public void ClearsCarry()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x01 });
            mem.Accumulator = 0x01;
            mem.ProgramCounter = 0x00;
            mem.Carry = true;
            var instr = new AddLiteral();

            instr.Execute(mem);

            Assert.False(mem.Carry);
        }

        [Fact]
        public void OverflowAdd()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0xFF });
            mem.Accumulator = 0x01;
            mem.ProgramCounter = 0x00;
            var instr = new AddLiteral();

            instr.Execute(mem);

            Assert.Equal(0x00, mem.Accumulator);
            Assert.True(mem.Carry);
            Assert.True(mem.AuxCarry);
        }

        [Fact]
        public void AuxOverflowAdd()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x0F });
            mem.Accumulator = 0x01;
            mem.ProgramCounter = 0x00;
            var instr = new AddLiteral();

            instr.Execute(mem);

            Assert.Equal(0x10, mem.Accumulator);
            Assert.False(mem.Carry);
            Assert.True(mem.AuxCarry);
        }

        [Fact]
        public void SignedOverflowPosAdd()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x7F }); // 127
            mem.Accumulator = 0x01;
            mem.ProgramCounter = 0x00;
            var instr = new AddLiteral();

            instr.Execute(mem);

            Assert.Equal(0x80, mem.Accumulator);
            Assert.False(mem.Carry);
            Assert.True(mem.AuxCarry);
            Assert.True(mem.SFR.Overflow);
        }

        [Fact]
        public void SignedOverflowNegAdd()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x24, 0x80 }); // -128
            mem.Accumulator = 0xff; // -1
            mem.ProgramCounter = 0x00;
            var instr = new AddLiteral();

            instr.Execute(mem);

            Assert.Equal(0x7f, mem.Accumulator);
            Assert.True(mem.Carry);
            Assert.False(mem.AuxCarry);
            Assert.True(mem.SFR.Overflow);
        }
    }
}