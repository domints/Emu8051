namespace Emu8051.Net.Core.Instructions.Jump
{
    public class JumpIfCarry : IInstruction
    {
        public byte OpCode => 0x40;

        public string Mnemonic => "JC";

        public string VariantMnemonic => "JC reladdr";

        public int Cycles => 2;

        public void Execute(Memory memory)
        {
            if (memory.Carry)
            {
                var offset = (sbyte)memory.RomOffset(1);
                var newPc = memory.ProgramCounter + 2 + offset;
                memory.ProgramCounter = (ushort)newPc;
            }
            else
            {
                memory.ProgramCounter += 2;
            }
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} rel {(sbyte)memory.RomOffset(1)} (C:{(memory.Carry ? 1 : 0)})";
        }
    }
}