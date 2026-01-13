// вариант 8: поиск максимального подмассива

using System;
using System.Diagnostics;

class Program
{
    static int FindMaxSubarray(int[] arr)
    {
        int max = int.MinValue;
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i; j < arr.Length; j++)
            {
                int sum = 0;
                for (int k = i; k <= j; k++)
                {
                    sum += arr[k];
                }
                if (sum > max) max = sum;
            }
        }
        return max;
    }

    static int FindMaxSubarrayKadane(int[] arr)
    {
        if (arr == null || arr.Length == 0)
            return 0;

        int maxCur = arr[0];
        int maxGlobal = arr[0];

        for (int i = 1; i < arr.Length; i++)
        {
            maxCur = Math.Max(arr[i], maxCur + arr[i]);

            // Обновляем глобальный максимум, если нужно
            if (maxCur > maxGlobal)
                maxGlobal = maxCur;
        }

        return maxGlobal;
    }

    static void MeasurePerformance()
    {
        Random rnd = new Random();

        Console.WriteLine("Сравнение производительности:");
        Console.WriteLine("=============================");
        Console.WriteLine("n\tСтарый (тики)\tНовый (тики)\tУскорение");
        Console.WriteLine("------------------------------------------------");

        int[] testSizes = { 5, 10, 50, 100, 200, 500, 1000 };

        foreach (int n in testSizes)
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next(-100, 100);
            }

            // Замер старой реализации
            Stopwatch swOld = Stopwatch.StartNew();
            int resultOld = FindMaxSubarray(arr);
            swOld.Stop();

            // Замер новой реализации
            Stopwatch swNew = Stopwatch.StartNew();
            int resultNew = FindMaxSubarrayKadane(arr);
            swNew.Stop();

            // Проверка корректности
            if (resultOld != resultNew)
            {
                Console.WriteLine($"Ошибка! Результаты не совпадают при n={n}");
                return;
            }

            double speedup = (double)swOld.ElapsedTicks / swNew.ElapsedTicks;
            Console.WriteLine($"{n}\t{swOld.ElapsedTicks,12}\t{swNew.ElapsedTicks,11}\t{speedup:F2}x");
        }
    }

    static void Main()
    {
        MeasurePerformance();

        // Демонстрация работы с исходным примером
        Console.WriteLine("\nДемонстрация работы:");
        Console.WriteLine("====================");

        int[] arr = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };

        Stopwatch sw = Stopwatch.StartNew();
        int max = FindMaxSubarray(arr);
        sw.Stop();

        Console.WriteLine("Максимальная сумма подмассива старого алгоритма: " + max);
        Console.WriteLine("Время: " + sw.ElapsedTicks + " тиков");

        Stopwatch sw2 = Stopwatch.StartNew();
        int max2 = FindMaxSubarrayKadane(arr);
        sw2.Stop();

        Console.WriteLine("Максимальная сумма подмассива нового алгоритма: " + max2);
        Console.WriteLine("Время: " + sw2.ElapsedTicks + " тиков");
    }
}