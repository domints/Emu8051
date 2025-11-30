namespace Emu8051.Net.Core.Instructions.Increment
{
    public abstract class IncrementBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "INC";
        public abstract string VariantMnemonic { get; }
        public int Cycles => 1;

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);
    }
}