namespace Simulator.System;

[Flags]
public enum ControlWord
{
    // Control
    HALT    = 1 << 00,  // Halt execution
    END     = 1 << 01,  // End of instruction
    // PC
    PC_INC  = 1 << 02,  // Inc PC
    PC_OUT  = 1 << 03,  // Program counter out
    PC_IN   = 1 << 04,  // Program counter in
    // SP
    SP_POP  = 1 << 05,  // Pop stack
    SP_PUSH = 1 << 06,  // Push stack
    SP_OUT  = 1 << 07,  // Stack pointer out
    SP_IN   = 1 << 08,  // Stack point in
    // RAM
    MAR_IN  = 1 << 09,  // Memory address register in
    MEM_IN  = 1 << 10,  // RAM data in
    MEM_OUT = 1 << 11,  // RAM data out
    // IR
    IR_IN   = 1 << 12,  // Instruction register in
    // Registers
    RA_IN   = 1 << 13,  // A register in
    RA_OUT  = 1 << 14,  // A register in
    RB_IN   = 1 << 15,  // B register in
    // ALU Operations
    FI      = 1 << 16,  // Flags in
    ALU_OUT = 1 << 17,  // ALU out
    ALU_SUB = 1 << 18,  // ALU subtract
    ALU_OP2 = 1 << 19,  // ALU subtract
    ALU_OP3 = 1 << 20,  // ALU subtract
    ALU_OP4 = 1 << 21,  // ALU subtract
}