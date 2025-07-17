using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace task17;


public interface ICommand
{
    void Execute();
}


public class ServerThread
{
    public BlockingCollection<ICommand> CommandQueue = new BlockingCollection<ICommand>();
    private Thread thread;
    public volatile bool is_running = true;
    public volatile bool soft_stop = false;

    public ServerThread()
    {
        thread = new Thread(RunCommand);
        thread.Start();
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
            if (CommandQueue.TryTake(out ICommand command, Timeout.Infinite))
            {
                command.Execute();
            }
            if (soft_stop && CommandQueue.Count == 0)
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

public class SimpleCommand : ICommand
{
    public void Execute()
    {
        System.Console.WriteLine("running...");
        Task.Delay(2000).Wait();
        System.Console.WriteLine("done");
    }
}
