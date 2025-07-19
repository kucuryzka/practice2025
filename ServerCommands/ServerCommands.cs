using System.Collections.Concurrent;
namespace ServerCommands;


public interface ICommand
{
    void Execute();
}


public interface IScheduler
{
    bool HasCommand();
    ICommand Select();
    void Add(ICommand cmd);
}

public class Scheduler : IScheduler
{
    Queue<ICommand> _commands = new Queue<ICommand>();
    public bool HasCommand() => _commands.Count > 0;

    public ICommand Select()
    {
        if (this.HasCommand())
        {
            var cmd = _commands.Dequeue();
            _commands.Enqueue(cmd);
            return cmd;
        }
        return null;
    }

    public void Add(ICommand cmd)
    {
        if (cmd != null) _commands.Enqueue(cmd);
    }
}