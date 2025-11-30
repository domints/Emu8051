namespace Emu8051.Net.Core.Instructions.Decrement
{
    public abstract class DecrementBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "DEC";
        public abstract string VariantMnemonic { get; }
        public int Cycles => 1;

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);
    }
}