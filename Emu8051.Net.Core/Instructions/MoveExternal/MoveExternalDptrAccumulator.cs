namespace Emu8051.Net.Core.Instructions.MoveExternal
{
    public class MoveExternalDptrAccumulator : MoveExternalBase
    {
        public override byte OpCode => 0xF0;

        public override string VariantMnemonic => $"{Mnemonic} @DPTR,A";

        public override int Cycles => 1;

        public override void Execute(Memory memory)
        {
            ReadAccFromExternalMemory(memory, memory.SFR.DPTR);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} DPTR [{memory.SFR.DPTR:X4}], A [{memory.Accumulator:X2}]";
        }
    }
}