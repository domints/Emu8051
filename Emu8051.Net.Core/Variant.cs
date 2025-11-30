namespace Emu8051.Net.Core
{
    public enum Variant
    {
        /// <summary>
        /// No ROM, 128B RAM
        /// </summary>
        _8031,
        /// <summary>
        /// No ROM, 256B RAM
        /// </summary>
        _8032,
        /// <summary>
        /// 4KB ROM, 128B RAM
        /// </summary>
        _8051,
        /// <summary>
        /// 8KB ROM, 256B RAM
        /// </summary>
        _8052
    }
}