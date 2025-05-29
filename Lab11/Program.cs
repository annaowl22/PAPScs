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
                if(input == null)
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
                    Console.WriteLine("[Enter]. Вернуться назад");
                    input = Console.ReadLine();
                    if(input == null)
                    {
                        continue;
                    }
                    else
                    {
                        setup = new ChessPawnTurnPosition();
                        state = new WhiteTurnState();
                    }
                }
                else
                {
                    setup = new ChessStartingPosition();
                    state = new WhiteTurnState();
                }
                game = new Game(state, setup);
                game.run();
            }
            Console.WriteLine("До следующей сессии!");
        }
    }
}