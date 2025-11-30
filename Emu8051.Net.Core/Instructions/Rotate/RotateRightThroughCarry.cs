namespace Emu8051.Net.Core.Instructions.Rotate
{
    public class RotateRightThroughCarry : IInstruction
    {
        public byte OpCode => 0x13;

        public string Mnemonic => "RRC";

        public string VariantMnemonic => "RRC A";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            var rightmostBit = (memory.Accumulator & 0x01) > 0;
            memory.Accumulator >>= 1;
            if (memory.Carry)
                memory.Accumulator |= 0x80;

            memory.Carry = rightmostBit;

            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}]";
        }
    }
}