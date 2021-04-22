using System;
using System.Linq;
using System.Text.Json;

namespace Permutrations
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] arr = new int[] { 5, 7, 16, 1, 2 };
            int[] arr2 = new int[] { 3, 5, -1, 8, 12 };

            Console.WriteLine(Combination(arr2));

            Console.ReadLine();
        }

        public static string Combination(int[] array)
        {
            var largestNum = array.Max();
            int sum = 0;
            array = array.OrderBy(x => x).ToArray();
            Console.WriteLine(JsonSerializer.Serialize(array));

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];

                for (int y = 0; y < array.Length; y++)
                {
                    if (i != y)
                    {
                        sum += array[y];
                        if (sum == largestNum)
                        {
                            return "true";
                        }
                    }
                }

                for (var k = 0; k < array.Length; k++)
                { // dont need double loop because sorted backwards
                    if (i != k)
                    {
                        sum -= array[k];
                        if (sum == largestNum)
                        {
                            return "true";
                        }
                    }
                }
                sum = 0;
            }

            return "false";
        }
    }
}