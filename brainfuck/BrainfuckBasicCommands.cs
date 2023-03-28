using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => { write((char)vm.Memory[vm.MemoryPointer]); });
			vm.RegisterCommand('+', b => {
                unchecked
                {
                    vm.Memory[vm.MemoryPointer]++;
                }
            });
			vm.RegisterCommand('-', b => {
                unchecked
                {
                    vm.Memory[vm.MemoryPointer]--;
                }
            });
			vm.RegisterCommand(',', b => { 
                vm.Memory[vm.MemoryPointer] = (byte)read(); 
            });
			vm.RegisterCommand('>', b => {
                vm.MemoryPointer = vm.MemoryPointer < vm.Memory.Length - 1
                ? ++vm.MemoryPointer
                : 0;
            });
			vm.RegisterCommand('<', b => {
                vm.MemoryPointer = vm.MemoryPointer > 0 
                ? --vm.MemoryPointer 
                : vm.Memory.Length - 1;
            });
            for (int i = 48; i < 123; i++)
            {
                if ((i >= 48 && i < 58) || (i >= 65 && i < 91) || (i >= 97 && i < 123))
                {
                    var j = i;
                    vm.RegisterCommand((char)j, b => { vm.Memory[vm.MemoryPointer] = (byte)j; });
                }
            }
        }
    }
}