using System.Collections.Immutable;

namespace Emu8051.Net.Core
{
    public class Memory
    {
        public ImmutableArray<byte> ROM { get; private set; }
        public byte[] RAM { get; init; }
        public SpecialFunctionRegisters SFR { get; private init; }
        public DirectMemoryProxy DirectMemory { get; private init; }
        public byte[] ExternalMemory { get; private init; }


        public byte StackPointer { get => SFR.StackPointer; set => SFR.StackPointer = value; }
        public ushort ProgramCounter { get; set; } = 0x0000;
        public bool Carry { get => SFR.Carry; set => SFR.Carry = value; }
        public bool AuxCarry { get => SFR.AuxCarry; set => SFR.AuxCarry = value; }
        public byte Accumulator { get => SFR.Accumulator; set => SFR.Accumulator = value; }
        
        public byte CurrentBank => SFR.CurrentBank;
        //public EmuCore Core { get; }

        public Memory(/*EmuCore core, */int ramSize, int externalMemorySize = 0)
        {
            //Core = core;
            RAM = new byte[ramSize];
            SFR = new SpecialFunctionRegisters
            {
                StackPointer = 0x07
            };
            DirectMemory = new DirectMemoryProxy(RAM, SFR);
            if (externalMemorySize > 0)
            {
                ExternalMemory = new byte[externalMemorySize];
            }
            else
            {
                ExternalMemory = Array.Empty<byte>();
            }
        }

        public void LoadRom(byte[] romContents)
        {
            if (romContents.Length > ushort.MaxValue)
                throw new InvalidDataException("ROM is too long for this core.");
            ROM = romContents.ToImmutableArray();
        }

        public void PushToStack(byte value)
        {
            if (StackPointer == RAM.Length - 1)
                throw new InvalidOperationException("8051 Stack Overflow Exception!");

            StackPointer++;
            RAM[StackPointer] = value;
        }

        public byte PopFromStack()
        {
            var value = RAM[StackPointer];
            if (StackPointer == 0)
                throw new InvalidOperationException("8051 Stack Underflow Exception!");

            StackPointer--;
            return value;
        }

        public byte PeekStack()
        {
            return RAM[StackPointer];
        }

        public byte Rom()
        {
            return ROM[ProgramCounter];
        }

        public byte RomOffset(int offset)
        {
            return ROM[ProgramCounter + offset];
        }

        public byte GetBankRegisterValue(int register)
        {
            return RAM[CurrentBank * 8 + register];
        }

        public void SetBankRegisterValue(int register, byte value)
        {
            RAM[CurrentBank * 8 + register] = value;
        }

        public bool GetBit(byte bitAddress)
        {
            if (bitAddress >= 0x80)
            {
                var registerAddress = bitAddress / 8;
                var registerBit = bitAddress % 8;
                return (SFR[registerAddress * 8] >> registerBit & 0x01) == 1;
            }
            else
            {
                var registerAddress = bitAddress / 8;
                var registerBit = bitAddress % 8;
                return (RAM[registerAddress + 0x20] >> registerBit & 0x01) == 1;
            }
        }

        public void SetBit(byte bitAddress, bool value)
        {
            if (bitAddress >= 0x80)
            {
                var registerAddress = bitAddress / 8;
                var registerBit = bitAddress % 8;
                if (value)
                    SFR[registerAddress * 8] |= (byte)(1 << registerBit);
                else
                    SFR[registerAddress * 8] &= (byte)~(1 << registerBit);
            }
            else
            {
                var registerAddress = bitAddress / 8;
                var registerBit = bitAddress % 8;
                if (value)
                    RAM[registerAddress + 0x20] |= (byte)(1 << registerBit);
                else
                    RAM[registerAddress + 0x20] &= (byte)~(1 << registerBit);
            }
        }
    }
}