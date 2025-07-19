using Xunit;
using task17;
using ServerCommands;

namespace task17tests;

public class SimpleCommand : ICommand
{
    public bool IsCompleted = false;
    public void Execute()
    {
        IsCompleted = true;
    }
}
public class ServerThreadTests
{
    private ServerThread server;
    private Scheduler scheduler;

    public ServerThreadTests()
    {
        scheduler = new Scheduler();
        server = new ServerThread(scheduler);
    }

    [Fact]
    public void ServerExecutesSimpleCommand_Test()
    {
        var command = new SimpleCommand();
        server.AddCommand(command);

        Thread.Sleep(1000);

        Assert.True(command.IsCompleted);
    }

    [Fact]
    public void ServerExecutesLongRunningCommand_Test()
    {
        var command1 = new LongRunningCommand(scheduler, 5);
        var command2 = new LongRunningCommand(scheduler, 3);

        server.AddCommand(command1);
        server.AddCommand(command2);

        Thread.Sleep(2000);

        Assert.False(command1.IsRunning());
        Assert.False(command2.IsRunning());


    }


    [Fact]
    public void ServerThreadSoftStop_Test()
    {
        ServerThread server = new ServerThread(scheduler);

        server.AddCommand(new SimpleCommand());
        server.AddCommand(new SoftStop(server));
        server.AddCommand(new SimpleCommand());
        server.AddCommand(new SimpleCommand());


        Thread.Sleep(6000);

        Assert.True(server.CommandQueue.Count == 0);
        Assert.True(server.soft_stop);
    }

    [Fact]
    public void ServerHardStop_Test()
    {
        ServerThread server = new ServerThread(scheduler);

        server.AddCommand(new SimpleCommand());
        server.AddCommand(new HardStop(server));
        server.AddCommand(new SimpleCommand());
        server.AddCommand(new SimpleCommand());


        Thread.Sleep(6000);

        Assert.True(server.CommandQueue.Count > 0);
        Assert.False(server.is_running);


        }
}