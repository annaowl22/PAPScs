
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System;
namespace Name
{
    class MoveReader
    {
        private static readonly Regex MovePattern = new Regex(@"^([a-h])([1-8])-([a-h])([1-8])$", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex GoalPattern = new Regex(@"^Г[О]+Л$",RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex LosePattern = new Regex(@"^Сдаюсь$",RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex CheckPattern = new Regex(@"^Анализ$",RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static List<Field> read()
        {
            List<Field> move = new List<Field>();
            Console.WriteLine("Введите ход в формате e2-e4, запросите АНАЛИЗ или признайте поражение словом СДАЮСЬ");
            string? input;
            while (true)
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ход не введён. Пожалуйста, повторите ввод корректно");
                    continue;
                }
                var match = MovePattern.Match(input.Trim());
                var goal = GoalPattern.Match(input.Trim());
                var check = CheckPattern.Match(input.Trim());
                var lose = LosePattern.Match(input.Trim());
                if (goal.Success)
                {
                    Console.WriteLine("Ты дурак? Мы в шахматы играем");
                }
                else if (check.Success)
                {
                    Console.WriteLine("Запрошен анализ ситуации");
                }
                else if (lose.Success)
                {
                    Console.WriteLine("Игрок признал поражение");
                    move.Add(new Field(0,0));
                    move.Add(new Field(0,0));
                    return move;
                }
                else if (!match.Success)
                {
                    Console.WriteLine("Неверный формат ввода. Правильный формат: e2-e4");
                }
                else
                {
                    Field fieldFrom = new Field(char.ToLower(input[0]) - 'a', input[1] - '1');
                    Field fieldTo = new Field(char.ToLower(input[3]) - 'a', input[4] - '1');
                    move.Add(fieldFrom);
                    move.Add(fieldTo);
                    return move;
                }
            }
        }
        public static Piece pawnTurn(PieceColor color, Field position)
        {
            Console.WriteLine("Введите символ фигуры, в которую хотите превратить пешку");
            string? input;
            while(true)
            {
                input = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ввод не должен быть пустым, выберите фигуру");
                }else if(input == "K")
                {
                    Console.WriteLine("Пешка не может превратиться в Короля");
                }else if(input == "p")
                {
                    Console.WriteLine("Пешка не может остаться Пешкой");
                }else if(input == "Q")
                {
                    Console.WriteLine("Пешка превращается в Ферзя");
                    return new Queen(color,position);
                }else if(input == "R")
                {
                    Console.WriteLine("Пешка превращается в Ладью");
                    return new Rook(color,position);
                }else if(input == "N")
                {
                    Console.WriteLine("Пешка превращается в Коня");
                    return new Knight(color,position);
                }else if(input == "B")
                {
                    Console.WriteLine("Пешка превращается в Слона");
                    return new Bishop(color,position);
                }else 
                {
                    Console.WriteLine("Некорректный ввод");
                }
            }
        }
    }
}