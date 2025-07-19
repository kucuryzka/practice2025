using System.Collections.Concurrent;
using System.Diagnostics;
namespace ServerCommands;


public interface ICommand
{
    void Execute();
}

public class TestCommand : ICommand
{
    public int id;
    private readonly IScheduler scheduler;
    public int total_count;
    public int counter = 0;
    public long execution_time;
    private Stopwatch sw = new Stopwatch();

    public TestCommand(int id, IScheduler scheduler, int total_count)
    {
        this.id = id;
        this.scheduler = scheduler;
        this.total_count = total_count;
    }


    public void Execute()
    {
        if (counter == total_count) return;
        if (counter == 0)
            sw.Start();

        counter++;
        Console.WriteLine($"thread: {id} call: {counter}");

        Thread.Sleep(10);

        if (counter < total_count)
        {
            scheduler.Add(this);
        }
        else
        {
            sw.Stop();
            execution_time = sw.ElapsedMilliseconds;
        }
    }
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