using task17;

namespace task17tests;

public class ServerThreadTests
{
    [Fact]
    public void ServerThreadInit_Test()
    {
        List<ICommand> commands = new List<ICommand>();
        for (int i = 0; i < 100; i++)
        {
            commands.Add(new SimpleCommand());
        }

        ServerThread serverThread = new ServerThread();

        Assert.True(serverThread.CommandQueue.Count == 0);

        foreach (ICommand command in commands)
        {
            serverThread.AddCommand(command);
        }

        Assert.True(serverThread.CommandQueue.Count > 0);

        
    }

    [Fact]
    public void ServerThreadSoftStop_Test()
    {
        ServerThread server = new ServerThread();

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
        ServerThread server = new ServerThread();

        server.AddCommand(new SimpleCommand());
        server.AddCommand(new HardStop(server));
        server.AddCommand(new SimpleCommand());
        server.AddCommand(new SimpleCommand());
        

        Thread.Sleep(6000);

        Assert.True(server.CommandQueue.Count > 0);
        Assert.False(server.is_running);

        
    }

    [Fact]
    public void SoftStopIncorrectThreadException_Test()
    {
        ServerThread server = new ServerThread();
        SoftStop command = new SoftStop(server);
        string error_message = "";

        Thread thread = new Thread(() =>
        {
            try
            {
                command.Execute();
            }
            catch (InvalidOperationException ex)
            {
                error_message = ex.Message;
            }
        });

        thread.Start();
        thread.Join();

        Assert.Equal("incorrect thread", error_message);
    }

    [Fact]
    public void HardStopIncorrectThreadException_Test()
    {
        ServerThread server = new ServerThread();
        HardStop command = new HardStop(server);
        string error_message = "";

        Thread thread = new Thread(() =>
        {
            try
            {
                command.Execute();
            }
            catch (InvalidOperationException ex)
            {
                error_message = ex.Message;
            }
        });

        thread.Start();
        thread.Join();

        Assert.Equal("incorrect thread", error_message);
    }
}
