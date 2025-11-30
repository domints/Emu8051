namespace Emu8051.Net.Core.Instructions.Complement
{
    public class ComplementAccumulator : IInstruction
    {
        public byte OpCode => 0xF4;

        public string Mnemonic => "CPL";

        public string VariantMnemonic => "CPL A";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            memory.Accumulator = (byte)(0xFF - memory.Accumulator);
            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}]";
        }
    }
}