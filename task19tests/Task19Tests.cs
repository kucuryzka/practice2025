using task17;
using ServerCommands;
using Xunit;

namespace task19tests;

public class Task19Tests
{
    [Fact]
    public void CommandsExecuteProperly_Test()
    {
         var scheduler = new Scheduler();
        var server = new ServerThread(scheduler);

        var commands = new TestCommand[5];
        for (int i = 0; i < 5; i++)
        {
            commands[i] = new TestCommand(i + 1, scheduler, 3);
            scheduler.Add(commands[i]);
        }

        Thread.Sleep(1000);
        server.AddCommand(new HardStop(server));

        

        foreach (var cmd in commands)
        {
            Assert.Equal(3, cmd.counter);
        }
    }
}