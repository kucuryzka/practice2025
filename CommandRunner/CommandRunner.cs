using CommandLib;
using System.Reflection;

namespace CommandRunner;

public class CommandRunner
{
    static void Main(string[] args)
    {
        var assembly = Assembly.LoadFrom("FileSystemCommands.dll");


        var dirsize_type = assembly.GetType("FileSystemCommands.DirectorySizeCommand");
        var dirsize_command = (ICommand)Activator.CreateInstance(dirsize_type, @"..\practice2025\");
        dirsize_command.Execute();

        var findfiles_type = assembly.GetType("FileSystemCommands.FindFilesCommand");
        var findfiles_command = (ICommand)Activator.CreateInstance(dirsize_type, @"..\practice2025\");
        findfiles_command.Execute();
    }
}
