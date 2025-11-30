using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Xunit;

namespace Emu8051.Net.Tests
{
    public class SFR
    {
        [Fact]
        public void GetRegisterBank0()
        {
            var sfr = new SpecialFunctionRegisters();
            sfr[Consts.ProgramStatusWord] = 0b11100111;

            Assert.Equal((byte)0, sfr.CurrentBank);
        }

        [Fact]
        public void GetRegisterBank3()
        {
            var sfr = new SpecialFunctionRegisters();
            sfr[Consts.ProgramStatusWord] = 0b00011000;

            Assert.Equal((byte)3, sfr.CurrentBank);
        }

        [Fact]
        public void SetRegisterBank1()
        {
            var sfr = new SpecialFunctionRegisters();
            sfr[Consts.ProgramStatusWord] = 0b10000000;
            sfr.CurrentBank = 1;

            Assert.Equal(0b10001000, sfr[Consts.ProgramStatusWord]);
        }

        [Fact]
        public void SetRegisterBank2()
        {
            var sfr = new SpecialFunctionRegisters();
            sfr[Consts.ProgramStatusWord] = 0b01000000;
            sfr.CurrentBank = 2;

            Assert.Equal(0b01010000, sfr[Consts.ProgramStatusWord]);
        }

        [Fact]
        public void ForbidsHighBank()
        {
            var sfr = new SpecialFunctionRegisters();
            sfr[Consts.ProgramStatusWord] = 0b00000000;
            Assert.Throws<InvalidOperationException>(() => sfr.CurrentBank = 4);

        }
    }
}