namespace Emu8051.Net.Core.Instructions.Rotate
{
    public class RotateLeft : IInstruction
    {
        public byte OpCode => 0x23;

        public string Mnemonic => "RL";

        public string VariantMnemonic => "RL A";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            var leftmostBit = (memory.Accumulator & 0x80) > 0;
            memory.Accumulator <<= 1;
            if (leftmostBit)
                memory.Accumulator |= 1;

            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}]";
        }
    }
}