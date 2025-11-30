namespace Emu8051.Net.Core.Instructions.MoveExternal
{
    public class MoveExternalAccumulatorDptr : MoveExternalBase
    {
        public override byte OpCode => 0xE0;

        public override string VariantMnemonic => $"{Mnemonic} A,@DPTR";

        public override int Cycles => 1;

        public override void Execute(Memory memory)
        {
            StoreAccInExternalMemory(memory, memory.SFR.DPTR);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [0x{memory.Accumulator:X2}], DPTR [0x{memory.SFR.DPTR:X4}]";
        }
    }
}