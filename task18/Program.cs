﻿using task17;

using ScottPlot;
using ServerCommands;

namespace task18;

public class Program
{
    public void Main(string[] args)
    {
        var scheduler = new Scheduler();
        var server = new ServerThread(scheduler);

        int totalCommands = 5;
        int commandTotalCount = 5;

        var commands = new List<LongRunningCommand>();

        for (int i = 0; i < totalCommands; i++)
        {
            var cmd = new LongRunningCommand(scheduler, commandTotalCount);
            commands.Add(cmd);
            server.AddCommand(cmd);
        }

        while (commands.Exists(cmd => cmd.IsRunning()))
        {
            Thread.Sleep(100);
        }

        List<double> times = new List<double>();
        foreach (var cmd in commands)
        {
            times.Add((double)(cmd.execution_time.Sum() / commandTotalCount));
        }

        var plt = new ScottPlot.Plot();
        double[] nums = Enumerable.Range(1, totalCommands).Select(x => (double)x).ToArray();
        double[] avgTimes = times.ToArray();
        plt.XLabel("command number");
        plt.YLabel("avg time(ms)");
        plt.Add.Scatter(nums, avgTimes);
        plt.SavePng(@"C:\practice2025\plot.png", 400, 400);


        for (int i = 0; i < totalCommands; i++)
        {
            string res = $"command number: {i + 1}\n average performance time: {avgTimes[i]}\n\n";
            File.AppendAllText(@"C:\practice2025\result.txt", res);
        }

        server.is_running = false;

    }
}
