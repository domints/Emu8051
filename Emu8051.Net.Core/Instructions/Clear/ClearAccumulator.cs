namespace Emu8051.Net.Core.Instructions.Clear
{
    public class ClearAccumulator : ClearBase
    {
        public override byte OpCode => 0xe4;
        public override string VariantMnemonic => $"{Mnemonic} A";

        public override void Execute(Memory memory)
        {
            memory.Accumulator = 0x00;
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}]";
        }
    }
}