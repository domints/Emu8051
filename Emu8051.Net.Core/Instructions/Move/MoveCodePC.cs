namespace Emu8051.Net.Core.Instructions.Move
{
    public class MoveCodePC : IInstruction
    {
        public byte OpCode => 0x83;

        public string Mnemonic => "MOVC";

        public string VariantMnemonic => "MOVC A,@A+PC";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            memory.ProgramCounter++;
            var address = memory.ProgramCounter + memory.Accumulator;
            memory.Accumulator = memory.ROM[address];
        }

        public string ValueString(Memory memory)
        {
            var sum = (ushort)(memory.Accumulator + memory.ProgramCounter);
            return $"{Mnemonic} A [{memory.Accumulator:X2}], @A+PC [PC:{memory.ProgramCounter:X4}, SUM:{sum:X4}, val:{memory.ROM[sum]}]";
        }
    }
}