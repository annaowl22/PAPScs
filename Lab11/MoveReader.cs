
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
namespace Name
{
    class MoveReader
    {
        private static readonly Regex MovePattern = new Regex(@"^([a-h])([1-8])-([a-h])([1-8])$", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex GoalPattern = new Regex(@"^Г[О]+Л$",RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public List<Field> read()
        {
            List<Field> move = new List<Field>();
            Console.WriteLine("Введите ход в формате e2-e4");
            string input = Console.Readline();
            while(1){
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ход не введён. Пожалуйста, повторите ввод корректно");
                    input = Console.Readline();
                    continue;
                }
                var match = MovePattern.Match(input.Trim());
                var goal = GoalPattern.Match(input.Trim());
                if(goal.Success)
                {
                    Console.WriteLine("Ты дурак? Мы в шахматы играем");
                }else if(!match.Success)
                {
                    Console.WriteLine("Неверный формат ввода. Правильный формат: e2-e4");
                }else
                {
                    Field fieldFrom = new Field(char.ToLower(input[0])-'a', int.Parse(input[1]) - 1);
                    Field fieldTo = new Field(char.ToLower(input[3])-'a', int.Parse(input[4]) - 1);
                    move.Add(fieldFrom);
                    move.Add(fieldTo);
                    return move;
                }
                input = Console.Readline();
            }
        }
    }
}