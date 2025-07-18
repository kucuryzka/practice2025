using System;
using System.Threading;
using System.Diagnostics;
using ScottPlot;
using task14;

namespace task15;

public class IntegralOptimization
{
    public static void Main(string[] args)
    {
        double START = -100;
        double END = 100;
        var SIN = (double x) => Math.Sin(x);
        double ANS = 0;

        List<double> steps = new List<double>() { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
        double min_appropriate_step = 1;
        foreach (var step in steps)
        {
            if (Math.Abs(DefiniteIntegral.Solve(START, END, SIN, step, 8) - ANS) <= 1e-4)
            {
                min_appropriate_step = step;
                break;
            }
        }


        double[] threadsQuantity = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];
        double[] threadTimes = new double[threadsQuantity.Length];

        for (int i = 0; i < threadsQuantity.Length; i++)
        {
            int currentQuantity = (int)threadsQuantity[i];
            double threadTime = 0;
            for (int j = 0; j < 10; j++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                double res = DefiniteIntegral.Solve(START, END, SIN, min_appropriate_step, currentQuantity);
                sw.Stop();

                threadTime += sw.Elapsed.TotalMilliseconds;
            }

            threadTimes[i] = threadTime / 10;
        }



        double minTime = threadTimes.Min();
        double optimalThreadsQuantity = threadTimes.ToList().IndexOf(minTime) + 1;


        var plot = new Plot();
        plot.Add.Scatter(threadsQuantity, threadTimes);
        plot.XLabel("number of threads");
        plot.YLabel("time");
        plot.SavePng(@"C:\practice2025\plot.png", 400, 400);


        double singleThreadTime = 0;
        for (int i = 0; i < 10; i++)
        {
            Stopwatch sw = Stopwatch.StartNew();
            double res = DefiniteIntegral.Integrate(START, END, SIN, min_appropriate_step);
            sw.Stop();

            singleThreadTime += sw.Elapsed.TotalMilliseconds;
        }
        singleThreadTime /= 10;

        double optimizationPercent = (singleThreadTime - minTime) / minTime * 100;

        string str_res = $"minimal appropriate step: {min_appropriate_step}\n optimal threads quantity: {optimalThreadsQuantity}\n single thread time: {singleThreadTime}\n multi-thread time: {minTime}\n optimization percent: {optimizationPercent}";
        File.WriteAllText("result.txt", str_res);
    }
}
