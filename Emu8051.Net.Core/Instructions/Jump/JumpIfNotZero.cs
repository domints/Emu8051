namespace Emu8051.Net.Core.Instructions.Jump
{
    public class JumpIfNotZero : IInstruction
    {
        public byte OpCode => 0x70;

        public string Mnemonic => "JNZ";

        public string VariantMnemonic => "JNZ rel_addr";

        public int Cycles => 2;

        public void Execute(Memory memory)
        {
            if (memory.Accumulator != 0)
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
            return $"{Mnemonic} rel {(sbyte)memory.RomOffset(1)} (ACC:{memory.Accumulator:X2})";
        }
    }
}