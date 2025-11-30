namespace Emu8051.Net.Core
{
    public static class Consts
    {
        public const byte Parity = 0x01;
        public const byte Overflow = 0x04;
        public const byte AuxCarry = 0x40;
        public const byte Carry = 0x80;
        public const byte UserFlag1 = 0x02;
        public const byte UserFlag0 = 0x20;
        public const byte BankSelect = 0x18;

        public const byte ProgramStatusWord = 0xD0;
    }
}