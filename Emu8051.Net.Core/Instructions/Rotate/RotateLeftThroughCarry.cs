namespace Emu8051.Net.Core.Instructions.Rotate
{
    public class RotateLeftThroughCarry : IInstruction
    {
        public byte OpCode => 0x33;

        public string Mnemonic => "RLC";

        public string VariantMnemonic => "RLC A";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            var leftmostBit = (memory.Accumulator & 0x80) > 0;
            memory.Accumulator <<= 1;
            if (memory.Carry)
                memory.Accumulator |= 1;

            memory.Carry = leftmostBit;

            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}]";
        }
    }
}