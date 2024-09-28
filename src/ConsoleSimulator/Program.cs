using System.Diagnostics;
using ConsoleSimulator;
using Simulator.System;

var assembler = new Assembler.Assembler();

Console.WriteLine("Running assembler...");
var sw = Stopwatch.StartNew();
var program = assembler.Compile("funcCall.txt");
sw.Stop();
Console.WriteLine($"Assembly successful ({sw.ElapsedMilliseconds}ms)");
Console.WriteLine("Starting simulator");
var runner = new ConsoleRunner();
runner.RunProgram(program);
Console.WriteLine("Simulator ended");
