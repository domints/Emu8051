namespace Emu8051.Net.Core.Instructions
{
    public class Swap : IInstruction
    {
        public byte OpCode => 0xC4;

        public string Mnemonic => "SWAP";

        public string VariantMnemonic => "SWAP A";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            var acc = memory.Accumulator;
            var newLow = acc >> 4;
            acc <<= 4;
            memory.Accumulator = (byte)(acc | newLow);
            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}]";
        }
    }
}