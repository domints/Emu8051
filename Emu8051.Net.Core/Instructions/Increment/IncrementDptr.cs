namespace Emu8051.Net.Core.Instructions.Increment
{
    public class IncrementDptr : IncrementBase
    {
        public override byte OpCode => 0xA3;

        public override string VariantMnemonic => $"{Mnemonic} DPTR";

        public override void Execute(Memory memory)
        {
            memory.SFR.DPTR++;
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} DPTR [{memory.SFR.DPTR:X4}]";
        }
    }
}