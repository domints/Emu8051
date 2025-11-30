namespace Emu8051.Net.Core.Instructions.Jump.CompareJumpNotEqual
{
    public class CompareJumpNotEqualAccumulator : CompareJumpNotEqualBase
    {

        public override byte OpCode => 0xB4;
        public override string VariantMnemonic => $"{Mnemonic} A,#value,rel_addr";

        public override void Execute(Memory memory)
        {
            var value = memory.RomOffset(1);
            var addressOffset = (sbyte)memory.RomOffset(2);
            memory.ProgramCounter += 3;
            var registerValue = memory.Accumulator;
            if (registerValue != value)
                memory.ProgramCounter = (ushort)(memory.ProgramCounter + addressOffset);

            memory.Carry = registerValue < value;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], #{memory.RomOffset(1):X2}, {(sbyte)memory.RomOffset(2)}";
        }
    }
}