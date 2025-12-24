using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static List<string> TokenizeCBAS(string program)
    {
        List<string> tokens = new List<string>();
        StringBuilder token = new StringBuilder();
        bool inString = false;
        bool inComment = false;

        for (int i = 0; i < program.Length; i++)
        {
            char c = program[i];

            // Обработка комментариев
            if (inComment)
            {
                if (c == '\n')
                {
                    inComment = false;
                }
                continue;
            }

            // Начало комментария
            if (c == '/' && i + 1 < program.Length && program[i + 1] == '/')
            {
                inComment = true;
                i++; // Пропускаем второй '/'
                continue;
            }

            // Обработка строк
            if (inString)
            {
                if (c == '"')
                {
                    token.Append(c);
                    tokens.Add(token.ToString());
                    token.Clear();
                    inString = false;
                }
                else
                {
                    token.Append(c);
                }
                continue;
            }

            // Начало строки
            if (c == '"')
            {
                if (token.Length > 0)
                {
                    tokens.Add(token.ToString());
                    token.Clear();
                }
                token.Append(c);
                inString = true;
                continue;
            }

            // Пропуск пробельных символов
            if (char.IsWhiteSpace(c))
            {
                if (token.Length > 0)
                {
                    tokens.Add(token.ToString());
                    token.Clear();
                }
                continue;
            }

            // Операторы и разделители
            if (c == ';' || c == '{' || c == '}' || c == '(' || c == ')' || c == ',' ||
                c == '+' || c == '-' || c == '*' || c == '/' || c == '<' || c == '>')
            {
                if (token.Length > 0)
                {
                    tokens.Add(token.ToString());
                    token.Clear();
                }
                tokens.Add(c.ToString());
                continue;
            }

            // Оператор присваивания и сравнения
            if (c == '=')
            {
                if (token.Length > 0)
                {
                    tokens.Add(token.ToString());
                    token.Clear();
                }

                // Проверяем, не является ли это оператором сравнения ==
                if (i + 1 < program.Length && program[i + 1] == '=')
                {
                    tokens.Add("==");
                    i++; // Пропускаем второй '='
                }
                else
                {
                    tokens.Add("=");
                }
                continue;
            }

            // Оператор неравенства
            if (c == '!')
            {
                if (token.Length > 0)
                {
                    tokens.Add(token.ToString());
                    token.Clear();
                }

                // Проверяем, не является ли это оператором !=
                if (i + 1 < program.Length && program[i + 1] == '=')
                {
                    tokens.Add("!=");
                    i++; // Пропускаем '='
                }
                else
                {
                    tokens.Add("!");
                }
                continue;
            }

            // Все остальные символы добавляем к текущему токену
            token.Append(c);
        }

        // Добавляем последний токен, если он есть
        if (token.Length > 0)
        {
            tokens.Add(token.ToString());
        }

        return tokens;
    }

    static void Main()
    {
        Console.WriteLine("=== CBAS Interpreter ===");

        List<string> testFiles = new List<string>
        {
            "test_correct.cbas"
        };

        foreach (string filename in testFiles)
        {
            Console.WriteLine($"\n=== Testing {filename} ===");

            if (!File.Exists(filename))
            {
                Console.WriteLine($"Error: Cannot open file {filename}");
                continue;
            }

            string program = File.ReadAllText(filename);
            List<string> tokens = TokenizeCBAS(program);

            //Console.Write("Tokens: ");
            //foreach (string token in tokens)
            //{
            //    if (token == "\n")
            //        Console.Write("\\n ");
            //    else
            //        Console.Write($"{token} ");
            //}
            Console.WriteLine("\nOutput:");

            CBASInterpreter interpreter = new CBASInterpreter();
            interpreter.Interpret(tokens);

            List<string> errors = interpreter.Errors;
            if (errors.Count > 0)
            {
                Console.WriteLine("\n Errors:");
                foreach (string error in errors)
                {
                    Console.WriteLine($"  {error}");
                }
            }
        }
    }
}