using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using ServerCommands;
using System.Diagnostics;

namespace task17;


public class ServerThread
{
    public BlockingCollection<ICommand> CommandQueue = new BlockingCollection<ICommand>();
    private readonly IScheduler? scheduler;
    public Thread thread;
    public volatile bool is_running = true;
    public volatile bool soft_stop = false;

    public ServerThread(IScheduler scheduler)
    {
        thread = new Thread(RunCommand);
        thread.Start();
        this.scheduler = scheduler;
    }

    public void AddCommand(ICommand command)
    {
        if (!is_running)
        {
            System.Console.WriteLine("server not working");
            return;
        }

        CommandQueue.Add(command);
    }

    private void RunCommand()
    {
        while (is_running)
        {
            if (CommandQueue.TryTake(out ICommand command))
            {
                command.Execute();
                continue;
            }

            if (scheduler != null && scheduler.HasCommand())
            {
                var cmd = scheduler.Select();
                cmd.Execute();
                continue;

            }
            if (soft_stop && CommandQueue.Count == 0 && !scheduler!.HasCommand())
            {
                is_running = false;
            }
        }
    }

    public void SoftStop()
    {   
        if (Thread.CurrentThread != thread)
        {
            throw new InvalidOperationException("incorrect thread");
        }

        soft_stop = true;
        
    }

    public void HardStop()
    {
        if (Thread.CurrentThread != thread)
        {
            throw new InvalidOperationException("incorrect thread");
        }

        is_running = false;
        CommandQueue.CompleteAdding();
    }

    

}

public class SoftStop : ICommand
{
    private ServerThread _server;

    public SoftStop(ServerThread server)
    {
        _server = server;
    }

    public void Execute()
    {
        _server.SoftStop();
    }
}

public class HardStop : ICommand
{
    private ServerThread _server;

    public HardStop(ServerThread server)
    {
        _server = server;
    }

    public void Execute()
    {
        _server.HardStop();
    }
}


public class LongRunningCommand : ICommand
{
    private IScheduler scheduler;
    public int total;
    public int count;

    public List<long> execution_time = new List<long>();

    public LongRunningCommand(IScheduler scheduler, int total)
    {
        count = 0;
        this.scheduler = scheduler;
        this.total = total;
    }

    public bool IsRunning() => count < total;

    public void Execute()
    {
        if (count == total) return;

        var sw = Stopwatch.StartNew();
        if (IsRunning())
        {

            count++;
            if (IsRunning())
            {
                scheduler.Add(this);
            }
        }
        Thread.Sleep(50);
        sw.Stop();

        execution_time.Add(sw.ElapsedMilliseconds);
    }
}
