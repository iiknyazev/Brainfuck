using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
        static Dictionary<int, int> bracketsOpen = new Dictionary<int, int>();
        static Dictionary<int, int> bracketsClose = new Dictionary<int, int>();
		static Stack<int> brackets = new Stack<int>();

		static void BodyLoop(IVirtualMachine vm)
		{
			brackets.Clear();
			bracketsOpen.Clear();
			bracketsClose.Clear();
			for (int i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[')
					brackets.Push(i);
				if (vm.Instructions[i] == ']')
				{
					var indx = brackets.Pop();
					bracketsOpen.Add(indx, i);
					bracketsClose.Add(i, indx);
				}
			}
		}

        public static void RegisterTo(IVirtualMachine vm)
		{
			BodyLoop(vm);

            vm.RegisterCommand('[', machine => {
				if (machine.Memory[machine.MemoryPointer] == 0)
					machine.InstructionPointer = bracketsOpen[machine.InstructionPointer];
			});
			vm.RegisterCommand(']', machine => {
                if (machine.Memory[machine.MemoryPointer] != 0)
					machine.InstructionPointer = bracketsClose[machine.InstructionPointer];
            });
		}
	}
}