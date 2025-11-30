using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class SJMP
    {
        [Fact]
        public void ForwardJump()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x00, 0x80, 0x02, 0x00, 0x00, 0x00 });
            mem.ProgramCounter = 1;
            var instr = new ShortJump();

            instr.Execute(mem);

            Assert.Equal(5, mem.ProgramCounter);
        }

        [Fact]
        public void JumpToZero()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x00, 0x80, 0xfd, 0x00, 0x00, 0x00 });
            mem.ProgramCounter = 1;
            var instr = new ShortJump();

            instr.Execute(mem);

            Assert.Equal(0, mem.ProgramCounter);
        }
    }
}