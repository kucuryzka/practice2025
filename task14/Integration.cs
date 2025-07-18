using System;
using System.Threading;
namespace task14;

public class DefiniteIntegral
{
    private static object lockObj = new object();
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double totalResult = 0;

        double interval = (b - a) / threadsnumber;

        int totalIntervals = (int)Math.Ceiling((b - a) / step);
        int intervalsPerThread = totalIntervals / threadsnumber;

        Parallel.For(0, threadsnumber, i =>
        {
            double start = a + i * intervalsPerThread * step;
            double end = (i == threadsnumber - 1) ? b : start + intervalsPerThread * step;

            double partResult = 0;
            for (double j = start; j < end; j += step)
            {
                double int_end = Math.Min(j + step, end);
                partResult += (function(j) + function(int_end)) / 2 * (int_end - j);
            }
            lock (lockObj)
            {
                totalResult += partResult;
            }
        });
        
        return totalResult;
    }

    public static double Integrate(double a, double b, Func<double, double> function, double step)
    {
        double result = 0;
        for (double i = a; i < b; i += step)
        {
            double int_end = Math.Min(i + step, b);
            result += (function(i) + function(int_end)) / 2 * (int_end - i);
        }

    
        return result;
    }
}
