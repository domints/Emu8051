namespace Emu8051.Net.Core.Instructions.Rotate
{
    public class RotateRight : IInstruction
    {
        public byte OpCode => 0x03;

        public string Mnemonic => "RR";

        public string VariantMnemonic => "RR A";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            var rightmostBit = (memory.Accumulator & 0x01) > 0;
            memory.Accumulator >>= 1;
            if (rightmostBit)
                memory.Accumulator |= 0x80;

            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}]";
        }
    }
}