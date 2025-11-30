using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions.Exchange;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class XCH
    {
        [Fact]
        public void ExchangeIRAM()
        {
            var mem = new Memory(16);
            mem.LoadRom(new byte[] { 0xC5, 0x01 });
            mem.Accumulator = 0xab;
            mem.ProgramCounter = 0x00;
            mem.RAM[1] = 0xcd;
            var instr = new ExchangeIRAM();

            instr.Execute(mem);

            Assert.Equal(0xcd, mem.Accumulator);
            Assert.Equal(0xab, mem.RAM[1]);
        }

        [Fact]
        public void ExchangeIRAM2()
        {
            var mem = new Memory(16);
            mem.LoadRom(new byte[] { 0xC5, 15 });
            mem.Accumulator = 0xab;
            mem.ProgramCounter = 0x00;
            mem.RAM[15] = 0xef;
            var instr = new ExchangeIRAM();

            instr.Execute(mem);

            Assert.Equal(0xef, mem.Accumulator);
            Assert.Equal(0xab, mem.RAM[15]);
        }

        [Fact]
        public void ExchangeRegister()
        {
            var mem = new Memory(128);
            mem.LoadRom(new byte[] { 0xC8 });
            mem.Accumulator = 0xab;
            mem.ProgramCounter = 0x00;
            mem.SFR.CurrentBank = 0;
            mem.RAM[0x00] = 0xef;
            var instr = new ExchangeRegister(0);

            instr.Execute(mem);

            Assert.Equal(0xef, mem.Accumulator);
            Assert.Equal(0xab, mem.RAM[0x0]);
        }

        [Fact]
        public void ExchangeRegisterBank1()
        {
            var mem = new Memory(128);
            mem.LoadRom(new byte[] { 0xC9 });
            mem.Accumulator = 0xab;
            mem.ProgramCounter = 0x00;
            mem.SFR.CurrentBank = 1;
            mem.RAM[0x09] = 0xef;
            var instr = new ExchangeRegister(1);

            instr.Execute(mem);

            Assert.Equal(0xef, mem.Accumulator);
            Assert.Equal(0xab, mem.RAM[0x09]);
        }
    }
}