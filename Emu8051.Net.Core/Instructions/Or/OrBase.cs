namespace Emu8051.Net.Core.Instructions.Or
{
    public abstract class OrBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "ORL";
        public abstract string VariantMnemonic { get; }
        public abstract int Cycles { get; }

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);
    }
}