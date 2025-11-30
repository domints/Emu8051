namespace Emu8051.Net.Core.Instructions.Move
{
    public class MoveCodeDptr : IInstruction
    {
        public byte OpCode => 0x93;

        public string Mnemonic => "MOVC";

        public string VariantMnemonic => "MOVC A,@A+DPTR";

        public int Cycles => 2;

        public void Execute(Memory memory)
        {
            var address = (ushort)(memory.Accumulator + memory.SFR.DPTR);
            var value = memory.ROM[address];
            memory.Accumulator = value;
            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            var sum = (ushort)(memory.Accumulator + memory.SFR.DPTR);
            return $"{Mnemonic} A [{memory.Accumulator:X2}], @A+DPTR [DPTR:{memory.SFR.DPTR:X4}, SUM:{sum:X4}, val:{memory.ROM[sum]}]";
        }
    }
}