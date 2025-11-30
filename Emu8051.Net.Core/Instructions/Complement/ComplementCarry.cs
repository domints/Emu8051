namespace Emu8051.Net.Core.Instructions.Complement
{
    public class ComplementCarry : IInstruction
    {
        public byte OpCode => 0xB3;

        public string Mnemonic => "CPL";

        public string VariantMnemonic => "CPL C";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            memory.Carry = !memory.Carry;
            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} C [{(memory.Carry?1:0)}]";
        }
    }
}