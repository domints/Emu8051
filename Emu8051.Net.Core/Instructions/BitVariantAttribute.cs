namespace Emu8051.Net.Core.Instructions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BitVariantAttribute : Attribute
    {
        public int CommonBits { get; }
        public BitVariantAttribute(int commonBits)
        {
            this.CommonBits = commonBits;
            
        }
    }
}