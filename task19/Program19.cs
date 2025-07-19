using ServerCommands;
using task17;

namespace task19;

public class Program
{
    public static void Main(string[] args)
    {
        int commandQuantity = 5;
        int commandCall = 3;

        var scheduler = new Scheduler();
        var server = new ServerThread(scheduler);

        var commands = new List<TestCommand>();
        for (int i = 1; i < commandQuantity + 1; i++)
        {
            var cmd = new TestCommand(i, scheduler, commandCall);
            scheduler.Add(cmd);
            commands.Add(cmd);
        }


        while (commands.Any(c => c.counter < commandCall))
            Thread.Sleep(10);

        server.AddCommand(new HardStop(server));
        Thread.Sleep(1000);

        double[] executionTimes = commands.Select(cmd => (double)cmd.execution_time).ToArray();
        double[] commandId = { 1, 2, 3, 4, 5 };

        var plt = new ScottPlot.Plot();
        plt.XLabel("command quantity");
        plt.YLabel("time(ms)");
        plt.Add.Scatter(commandId, executionTimes);

        plt.SavePng(@"C:\practice2025\plot19.png", 600, 300);

        double totalExecutionTime = executionTimes.Sum();

        string res = $"Command quantity: 5, every command was called 3 times\n Total execution time: {totalExecutionTime}";
        File.WriteAllText(@"C:\practice2025\result19.txt", res);

    }
}
