namespace Emu8051.Net.Core.Instructions.Clear
{
    public class ClearCarry : ClearBase
    {
        public override byte OpCode => 0xC3;
        public override string VariantMnemonic => $"{Mnemonic} C";

        public override void Execute(Memory memory)
        {
            memory.Carry = false;
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} C [{(memory.Carry?1:0)}]";
        }
    }
}