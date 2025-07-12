using Xunit;
using FileSystemCommands;

namespace task08tests;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

        long size = new FileInfo(Path.Combine(testDir, "test1.txt")).Length + new FileInfo(Path.Combine(testDir, "test2.txt")).Length;

        var command = new DirectorySizeCommand(testDir);
        command.Execute();

        Assert.Equal(size, command._size);

        Directory.Delete(testDir, true);

    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute();

        Assert.Equal(1, command.FilesArray.Length);

        Directory.Delete(testDir, true);
    }
}