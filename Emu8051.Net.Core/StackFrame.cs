using System.Text;

namespace Emu8051.Net.Core
{
    public record StackFrame(ushort Address, string Name)
    {
        protected virtual bool PrintMembers(StringBuilder stringBuilder)
        {
            stringBuilder.Append($"{Address:x4}: {Name}");
            return true;
        }
    }
}