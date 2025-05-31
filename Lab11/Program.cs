using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;
namespace Name
{
    class Program()
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в игру!");
            string? input;
            ChessSetup setup;
            State state;
            Game game;
            while(true)
            {
                Console.WriteLine("Напишите 0, если хотите выйти, 1 для настройки игры или любую другую клавишу, чтобы сыграть классическую партию");
                input = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ввод не может быть пустым. Следуйте инструкции");
                    continue;
                }
                if(input == "0")
                {
                    break;
                }
                else if(input == "1")
                {
                    Console.WriteLine("Выберите шахматную ситуацию:");
                    Console.WriteLine("1. Пешка белых проходит и превращается.");
                    Console.WriteLine("2. Мат в один ход - белые ходят.");
                    Console.WriteLine("3. Мат в два хода - белые ходят.");
                    Console.WriteLine("4. Мат в три хода - белые ходят.");
                    Console.WriteLine("5. Мат в два хода - черные ходят.");
                    Console.WriteLine("[Enter]. Вернуться назад");
                    input = Console.ReadLine();
                    if(string.IsNullOrWhiteSpace(input))
                    {
                        continue;
                    }
                    else if (input == "1")
                    {
                        setup = new ChessPawnTurnPosition();
                        state = new WhiteTurnState();
                    }
                    else if (input == "2")
                    {
                        setup = new MateInOneTurnWhiteStart();
                        state = new WhiteTurnState();
                    }
                    else if (input == "3")
                    {
                        setup = new MateInTwoTurnsWhiteStart();
                        state = new WhiteTurnState();
                    }
                    else if (input == "4")
                    {
                        setup = new MateInThreeTurnsWhiteStart();
                        state = new WhiteTurnState();
                    }
                    else if (input == "5")
                    {
                        setup = new MateInTwoTurnsWhiteStartSmall();
                        state = new BlackTurnState();
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    setup = new ChessStartingPosition();
                    state = new WhiteTurnState();
                }
                game = new Game(state, setup);
                game.RegisterObserver(new Notator());
                Console.WriteLine("Хотите подключить оценивание позиции? Введите любое число, если да, иначе пропустите ввод");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    game.RegisterObserver(new Onexbet());
                }
                game.run();
            }
            Console.WriteLine("До следующей сессии!");
        }
    }
}