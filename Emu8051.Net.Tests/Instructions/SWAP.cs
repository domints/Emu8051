using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class SWAP
    {
        [Fact]
        public void SwapsNibbles1()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0xC4 });
            mem.Accumulator = 0x01;
            mem.ProgramCounter = 0x00;
            var instr = new Swap();

            instr.Execute(mem);

            Assert.Equal(0x10, mem.Accumulator);
        }

        [Fact]
        public void SwapsNibbles2()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0xC4 });
            mem.Accumulator = 0xab;
            mem.ProgramCounter = 0x00;
            var instr = new Swap();

            instr.Execute(mem);

            Assert.Equal(0xba, mem.Accumulator);
        }
    }
}