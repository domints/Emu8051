namespace Emu8051.Net.Core.Instructions
{
    public interface IInstruction
    {
        byte OpCode { get; }
        string Mnemonic { get; }
        string VariantMnemonic { get; }
        int Cycles { get; }
        void Execute(Memory memory);
        string ValueString(Memory memory);
    }
}