namespace Emu8051.Net.Core.Instructions.Move
{
    public class MoveDPTRLiteral : MoveBase
    {
        public override byte OpCode => 0x90;
        public override string VariantMnemonic => $"{Mnemonic} DPTR,#value16";

        public override int Cycles => 1;

        public override void Execute(Memory memory)
        {
            memory.SFR.DPH = memory.RomOffset(1);
            memory.SFR.DPL = memory.RomOffset(2);
            memory.ProgramCounter += 3;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} DPTR, #{memory.RomOffset(1)<<8|memory.RomOffset(2):X4}";
        }
    }
}