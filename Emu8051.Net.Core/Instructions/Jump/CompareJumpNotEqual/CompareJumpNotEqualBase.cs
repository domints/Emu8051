namespace Emu8051.Net.Core.Instructions.Jump.CompareJumpNotEqual
{
    public abstract class CompareJumpNotEqualBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "CJNE";
        public abstract string VariantMnemonic { get; }
        public int Cycles => 2;

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);
    }
}