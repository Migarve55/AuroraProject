namespace Simulator.System;

[Flags]
public enum Flags
{
    ZF = 0b0001, // Zero
    CF = 0b0010, // Carry
    NF = 0b0100, // Negative
    OF = 0b1000, // Overflow
}