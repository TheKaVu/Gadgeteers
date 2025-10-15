using System;

namespace Source.Gadgeteers.Game
{
    [Flags]
    public enum DamageType
    {
        Impact = 1,
        Slash = 2,
        Ballistic = 4,
        Blast = 8,
        Toxic = 16,
        Laser = 32,
        Surface = 64,
        Internal = 128
    }
}