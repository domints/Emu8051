using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class XCHD
    {
        [Fact]
        public void ExchangesDigit()
        {
            var mem = new Memory(256);
            mem.LoadRom(new byte[] { 0xD6 });
            mem.Accumulator = 0xab;
            mem.ProgramCounter = 0x00;
            mem.SFR.CurrentBank = 0;
            mem.RAM[0x00] = 0x08;
            mem.RAM[0x08] = 0xcd;
            var instr = new ExchangeDigitAccumulatorAtR(0);

            instr.Execute(mem);

            Assert.Equal(0xad, mem.Accumulator);
            Assert.Equal(0xcb, mem.RAM[0x08]);
        }

        [Fact]
        public void ExchangeRelativeBank1()
        {
            var mem = new Memory(256);
            mem.LoadRom(new byte[] { 0xD6 });
            mem.Accumulator = 0xab;
            mem.ProgramCounter = 0x00;
            mem.SFR.CurrentBank = 1;
            mem.RAM[0x08] = 0x10;
            mem.RAM[0x10] = 0xcd;
            var instr = new ExchangeDigitAccumulatorAtR(0);

            instr.Execute(mem);

            Assert.Equal(0xad, mem.Accumulator);
            Assert.Equal(0xcb, mem.RAM[0x10]);
        }
    }
}