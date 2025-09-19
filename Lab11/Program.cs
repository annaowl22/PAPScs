using System;

namespace TAYAK1
{
    class Check
    {
        public static bool Skobe(string input)
        {
            int count = 0;
            for(int i = 0; i < input.Length; i++)
            {
                if(input[i] == '(')
                {
                    count++;
                }else if(input[i] == ')')
                {
                    count--;
                    if(count < 0)
                    {
                        return false;
                    }
                }
            }
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsSign(char letter)
        {
            char[] signs = { '-', '+', '/', '*', '(', ')' };
            for(int i = 0; i < signs.Length; i++)
            {
                if(letter == signs[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool Signs(string input)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if(IsSign(input[i]) || input[i] == ',')
                {
                    if (count == 2 || count == 1 && input[i] != '-')
                    {
                        return false;
                    }
                    else
                    {
                        count++;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Floats(string input)
        {
            bool prevdot = false;
            bool hasDot = false;
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i] == '.')
                {
                    prevdot = true;
                    if (hasDot)
                    {
                        return false;
                    }
                    else
                    {
                        hasDot = true;
                    }
                }
                else
                {
                    if (IsSign(input[i]) || input[i] == ',')
                    {
                        if (prevdot)
                        {
                            return false;
                        }
                        hasDot = false;
                    }
                    prevdot = false;
                }
            }
            return true;
        }
        public static bool Words(string input)
        {
            char prev = ' ';
            for(int i = 0; i < input.Length; i++)
            {
                if(prev == 'g' && !(input[i] == '('))
                {
                    return false;
                }
                if(!(input[i] == '.' || char.IsDigit(input[i])) || IsSign(input[i]) || input[i] == ' ' || input[i] == ',')
                {
                    if(!(input[i] == 'l' && !(prev == '.' || char.IsDigit(prev)) || input[i] == 'o' && prev == 'l' || input[i] == 'g' && prev == 'o'))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool Logs(string input)
        {
            for(int i = 0; i < input.Length - 3; i++)
            {
                if(input[i] == ',')
                {
                    return false;
                }
                if(input[i] == 'l' && input[i+1] == 'o' && input [i+2] == 'g' && input[i+3] == '(')
                {
                    int countSkobes = 1;
                    int countCommas = 0;
                    int commaIndex = 0;
                    int ind = i + 1;
                    while(countSkobes != 0 && ind < input.Length)
                    {
                        if(input[ind] == ',')
                        {
                            if(countCommas > 0)
                            {
                                return false;
                            }
                            commaIndex = ind;
                        }
                        else if(input[ind] == '(')
                        {
                            countSkobes++;
                        }
                        else if(input[ind] == ')')
                        {
                            countSkobes--;
                        }
                        ind++;
                    }
                    if(countSkobes != 0)
                    {
                        return false;
                    }
                    if(!(Logs(input.Substring(i+4, commaIndex - i - 4)) && Logs(input.Substring(commaIndex + 1, ind - commaIndex - 2))))
                    {
                        return false;
                    }
                    i = ind;
                }
            }
            return true;
        }
        public static bool AllChecks(string input)
        {
            bool result = true;
            if (!Floats(input))
            {
                Console.WriteLine("Incorrect float numbers");
                result = false;
            }
            if (!Signs(input.Replace(" ", "")))
            {
                Console.WriteLine("Incorrect order of signs");
                result = false;
            }
            if (!Skobe(input))
            {
                Console.WriteLine("Incorrect order of scobes");
                result = false;
            }
            if (!Words(input))
            {
                Console.WriteLine("Using unregistered functions");
                result = false;
            }
            if (!Logs(input.Replace(" ", "")))
            {
                Console.WriteLine("Incorrect usage of log function");
                result = false;
            }
            return result;
        }
        public static bool AllChecksSilent(string input)
        {
            return (Floats(input) && Signs(input) && Skobe(input) && Words(input) && Logs(input));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello User!");
            Console.WriteLine("Enter the sentence or 0 to end");
            string input = Console.ReadLine();
            while (input != "0")
            {
                if (Check.AllChecks(input))
                {
                    Console.WriteLine("The sentence is correct");
                }
                else
                {
                    Console.WriteLine("You wrote some bullshit");
                }
            }
            Console.WriteLine("Goodbye");
        }
    }
}

