using System;
using System.Collections.Generic;
using System.IO;

namespace CSharp
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, double> age = new();
            Dictionary<string, double> height = new();
            Dictionary<string, double> weight = new();
            Dictionary<string, int> count = new();

            string inputFilename = "input.txt";
            StreamReader sr = new StreamReader(inputFilename);

            string line = sr.ReadLine();

            //Пока считываемая строка непустая, цикл работает
            while (line != null)
            {
                string[] words = line.Split(" ");

                double temp = 0;

                //Подсчёт количества игроков в командах
                if (count.TryGetValue(words[1], out var k))
                    count[words[1]] = k + 1;
                else
                    count.Add(words[1], 1);

                //Подсчёт общего возраста команды
                if (age.TryGetValue(words[1], out temp))
                    age[words[1]] = temp + Convert.ToDouble(words[6]);
                else
                    age.Add(words[1], Convert.ToDouble(words[6]));

                //Подсчёт общего роста команды
                if (height.TryGetValue(words[1], out temp))
                    height[words[1]] = temp + Convert.ToDouble(words[7]);
                else
                    height.Add(words[1], Convert.ToDouble(words[7]));

                //Подсчёт общего веса команды
                if (weight.TryGetValue(words[1], out temp))
                    weight[words[1]] = temp + Convert.ToDouble(words[8]);
                else
                    weight.Add(words[1], Convert.ToDouble(words[8]));

                //Считывание строки с файла
                line = sr.ReadLine();
            }

            //Закрываем поток для считывания
            sr.Close();

            Dictionary<string, double> data = new();

            foreach (var item in count)
            {
                data.Add(item.Key, (age[item.Key] + weight[item.Key] + height[item.Key]) / item.Value);
            }

            //Берем максимальное значение, для первого минимального
            double min = 10000;
            string minName = "";

            //Поиск минимального среди средних значений данных команд
            foreach (var item in data)
            {
                if (item.Value < min)
                {
                    min = item.Value;
                    minName = item.Key;
                }
            }

            string outputFilename = "output.txt";
            StreamWriter sw = new StreamWriter(outputFilename);

            //Вывод названия команды
            sw.WriteLine(minName);

            sw.Close();
        }
    }
}   
