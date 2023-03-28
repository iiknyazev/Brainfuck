using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		private Dictionary<char, Action<IVirtualMachine>> commands = new Dictionary<char, Action<IVirtualMachine>>();

		public VirtualMachine(string program, int memorySize = 30000)
		{
			Instructions = program;
			Memory = new byte[memorySize];
			MemoryPointer = 0;
			InstructionPointer = 0;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			commands[symbol] = execute;
		}

		public void Run()
		{
			while (InstructionPointer < Instructions.Length)
			{
				if (commands.ContainsKey(Instructions[InstructionPointer]))
				{
					Action<IVirtualMachine> action = commands[Instructions[InstructionPointer]];
					action(this);
				}
				InstructionPointer++;
			}
		}
	}
}