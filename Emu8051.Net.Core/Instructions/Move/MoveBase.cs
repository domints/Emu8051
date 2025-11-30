namespace Emu8051.Net.Core.Instructions.Move
{
    public abstract class MoveBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "MOV";
        public abstract int Cycles { get; }
        public abstract string VariantMnemonic { get; }

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);
    }
}