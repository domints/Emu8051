namespace Emu8051.Net.Core.Instructions.Decrement
{
    public class DecrementAccumulator : DecrementBase
    {
        public override byte OpCode => 0x14;

        public override string VariantMnemonic => $"{Mnemonic} A";

        public override void Execute(Memory memory)
        {
            memory.Accumulator--;
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}]";
        }
    }
}