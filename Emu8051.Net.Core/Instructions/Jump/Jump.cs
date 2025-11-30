namespace Emu8051.Net.Core.Instructions.Jump
{
    public class Jump : IInstruction
    {
        public byte OpCode => 0x73;

        public string Mnemonic => "JMP";

        public string VariantMnemonic => $"{Mnemonic} @A+DPTR";

        public int Cycles => 2;

        public void Execute(Memory memory)
        {
            var address = (ushort)(memory.Accumulator + memory.SFR.DPTR);
            memory.ProgramCounter = address;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} @A[0x{memory.Accumulator:X2}]+DPTR[0x{memory.SFR.DPTR:X4}] (SUM:0x{memory.Accumulator+memory.SFR.DPTR:X4})";
        }
    }
}