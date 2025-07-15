using System;
using System.Threading;
namespace task14;

public class DefiniteIntegral
{
    private static object lockObj = new object();
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        int intervals = (int)Math.Ceiling((b - a) / step);
        double thread_intervals = (b - a) / threadsnumber;
        Barrier barrier = new Barrier(threadsnumber + 1);

        double result = 0;

        for (int i = 0; i < threadsnumber; i++)
        {
            double int_start = a + i * thread_intervals;
            double int_end = (i == threadsnumber - 1) ? b : int_start + thread_intervals;

            new Thread(() =>
            {
                double res = Integrate(int_start, int_end, function, step);
                lock (lockObj) { result += res; }
                barrier.SignalAndWait();
            }).Start();
        }
        barrier.SignalAndWait();
        return result;
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
