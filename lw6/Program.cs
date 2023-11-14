using System;
using System.Collections.Generic;

interface ILab8QueuePartBasicLevel
{
    int A { get; set; }
    int B { get; set; }
    int C { get; set; }
    List<int> GetSequence(int N);
}

class Lab8QueuePartBasicLevel : ILab8QueuePartBasicLevel
{
    public int A { get; set; }
    public int B { get; set; }
    public int C { get; set; }

    public Lab8QueuePartBasicLevel(int a, int b, int c)
    {
        A = a;
        B = b;
        C = c;
    }

    public List<int> GetSequence(int N)
    {
        List<int> result = new List<int>();
        Queue<int> queueA = new Queue<int>();
        Queue<int> queueB = new Queue<int>();
        Queue<int> queueC = new Queue<int>();

        queueA.Enqueue(A);
        queueB.Enqueue(B);
        queueC.Enqueue(C);

        for (int i = 0; i < N; i++)
        {
            int minValue = Math.Min(queueA.Peek(), Math.Min(queueB.Peek(), queueC.Peek()));
            result.Add(minValue);

            if (minValue == queueA.Peek())
            {
                queueA.Dequeue();
                queueA.Enqueue(minValue * A);
                queueB.Enqueue(minValue * B);
                queueC.Enqueue(minValue * C);
            }
            else if (minValue == queueB.Peek())
            {
                queueB.Dequeue();
                queueB.Enqueue(minValue * B);
                queueC.Enqueue(minValue * C);
            }
            else if (minValue == queueC.Peek())
            {
                queueC.Dequeue();
                queueC.Enqueue(minValue * C);
            }
        }

        return result;
    }
}

namespace lw6
{
    internal class Program
    {
        static bool IsCorrect(string input, out int processedCharacters)
        {
            Stack<char> stack = new Stack<char>();
            processedCharacters = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                processedCharacters++;

                if (IsOpeningBracket(currentChar))
                {
                    stack.Push(currentChar);
                }
                else if (IsClosingBracket(currentChar))
                {
                    if (stack.Count == 0 || !AreMatchingBrackets(stack.Pop(), currentChar))
                    {
                        return false; // Некоректне розташування дужок
                    }
                }
            }

            return stack.Count == 0; // Рядок коректний, якщо всі відкриваючі дужки були закриті.
        }

        static bool IsOpeningBracket(char c)
        {
            return c == '(' || c == '{' || c == '[' || c == '<';
        }

        static bool IsClosingBracket(char c)
        {
            return c == ')' || c == '}' || c == ']' || c == '>';
        }

        static bool AreMatchingBrackets(char opening, char closing)
        {
            return (opening == '(' && closing == ')') ||
                   (opening == '{' && closing == '}') ||
                   (opening == '[' && closing == ']') ||
                   (opening == '<' && closing == '>');
        }

        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("1. Згенерувати послідовність");
                Console.WriteLine("2. Перевірити дужки");
                Console.WriteLine("0. Вийти");
                Console.Write("Введіть свій вибір: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Введіть A: ");
                            int A = int.Parse(Console.ReadLine());
                            Console.Write("Введіть B: ");
                            int B = int.Parse(Console.ReadLine());
                            Console.Write("Введіть C: ");
                            int C = int.Parse(Console.ReadLine());
                            Console.Write("Введіть N: ");
                            int N = int.Parse(Console.ReadLine());

                            ILab8QueuePartBasicLevel sequenceGenerator = new Lab8QueuePartBasicLevel(A, B, C);
                            List<int> result = sequenceGenerator.GetSequence(N);

                            Console.WriteLine("Згенерована послідовність:");
                            foreach (var number in result)
                            {
                                Console.Write($"{number} ");
                            }
                            Console.WriteLine();
                            break;

                        case 2:
                            Console.Write("Введіть рядок для перевірки дужок: ");
                            string inputString = Console.ReadLine();
                            int processedCharacters;
                            bool isCorrect = IsCorrect(inputString, out processedCharacters);

                            if (isCorrect)
                            {
                                Console.WriteLine("Рядок має правильне розташування дужок.");
                            }
                            else
                            {
                                Console.WriteLine("Рядок має неправильне розташування дужок.");
                            }

                            Console.WriteLine($"Оброблено символів: {processedCharacters}");
                            break;

                        case 0:
                            Console.WriteLine("Вихід з програми. До побачення!");
                            break;

                        default:
                            Console.WriteLine("Неправильний вибір. Будь ласка, введіть правильну опцію.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неправильний ввід. Будь ласка, введіть число.");
                }

                Console.WriteLine();
            } while (choice != 0);
        }
    }
}
