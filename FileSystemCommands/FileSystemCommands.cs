using CommandLib;
namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand
{
    private readonly string _path;
    public long _size;

    public DirectorySizeCommand(string path)
    {
        _path = path;
        _size = 0;
    }

    public void Execute()
    {
        if (!Directory.Exists(_path)) return;

        var files = new DirectoryInfo(_path).GetFiles("*", SearchOption.AllDirectories);
        foreach (var file in files) { _size += file.Length; }
    }
}


public class FindFilesCommand : ICommand
{
    private readonly string _path;
    private string _mask;
    public string[] FilesArray { get; set; } = new string[0];

    public FindFilesCommand(string path, string mask)
    {
        _path = path;
        _mask = mask;
    }

    public void Execute()
    {
        if (!Directory.Exists(_path)) return;

        FilesArray = Directory.GetFiles(_path, _mask, SearchOption.AllDirectories);
    }
}
