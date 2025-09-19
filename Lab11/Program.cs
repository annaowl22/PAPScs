using System;

namespace TAYAK1
{
    class Check
    {
        public static bool Skobe(string input)
        {
            int count = 0;
            bool wassign = false;
            bool wasclosed = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    count++;
                    if (!wassign && wasclosed)
                    {
                        return false;
                    }
                    wasclosed = false;
                }
                else if (input[i] == ')')
                {
                    count--;
                    wassign = false;
                    wasclosed = true;
                    if (count < 0)
                    {
                        return false;
                    }
                }
                else if (char.IsDigit(input[i]) || input[i] == '.')
                {
                    if (!wassign && wasclosed)
                    {
                        return false;
                    }
                }
                else if (IsSign(input[i]))
                {
                    wassign = true;
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
                if (input[i] == ')')
                    {
                    if (count != 0)
                        {
                            Console.WriteLine("Error at " + i.ToString());
                            return false;
                        }
                    count = 0;
                    }
                else if (input[i] == '(')
                    {
                        count = 1;
                    }
                else if (IsSign(input[i]) || input[i] == ',')
                {
                    if (count == 2 || count == 1 && input[i] != '-')
                    {
                        Console.WriteLine("Error at " + i.ToString());
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
                Console.WriteLine("Error at the end");
                return false;
            }
        }
        public static bool Floats(string input)
        {
            bool prevdot = false;
            bool hasDot = false;
            bool ended = false;
            bool started = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (IsSign(input[i]) || input[i] == ',' || input[i] == ' ')
                {
                    if (prevdot)
                    {
                        Console.WriteLine("Error at " + i.ToString() + " point");
                        return false;
                    }
                    hasDot = false;
                    if (input[i] == ' ')
                    {
                        ended = true;
                    }
                    else
                    {
                        started = false;
                        ended = false;
                    }
                    prevdot = false;
                }
                else
                {
                    if (started && ended)
                    {
                        Console.WriteLine("Error at " + i.ToString() + " space inside");
                        return false;
                    }
                }
                if (input[i] == '.')
                {
                    started = true;
                    ended = false;
                    prevdot = true;
                    if (hasDot)
                    {
                        Console.WriteLine("Error at " + i.ToString());
                        return false;
                    }
                    else
                    {
                        hasDot = true;
                    }
                }
                if (char.IsDigit(input[i]))
                {
                    started = true;
                    prevdot = false;
                    ended = false;
                }                
            }
            return true;
        }
        public static bool Words(string input)
        {
            char prev = ' ';
            for (int i = 0; i < input.Length; i++)
            {
                if (prev == 'g' && !(input[i] == '('))
                {
                    Console.WriteLine("Error function no skobes at " + i.ToString());
                    return false;
                }
                if (!(input[i] == '.' || char.IsDigit(input[i]) || IsSign(input[i]) || input[i] == ' ' || input[i] == ','))
                {
                    if (!(input[i] == 'l' && !(prev == '.' || char.IsDigit(prev)) || input[i] == 'o' && prev == 'l' || input[i] == 'g' && prev == 'o'))
                    {
                        Console.WriteLine("Error letter at " + i.ToString());
                        Console.WriteLine("Previous " + prev + " this " + input[i]);
                        return false;
                    }
                }
                prev = input[i];
            }
            return true;
        }

        public static bool Logs(string input)
        {
            for(int i = 0; i < input.Length; i++)
            {
                if(input[i] == ',')
                {
                    Console.WriteLine("Error comma outside at " + i.ToString());
                    return false;
                }
                if (i < input.Length - 3)
                {
                    if (input[i] == 'l' && input[i + 1] == 'o' && input[i + 2] == 'g' && input[i + 3] == '(')
                    {
                        int countSkobes = 1;
                        int countCommas = 0;
                        int commaIndex = 0;
                        int ind = i + 4;
                        while (countSkobes != 0 && ind < input.Length)
                        {
                            if (input[ind] == ',')
                            {
                                if (countCommas > 0)
                                {

                                    Console.WriteLine("Error many commas at " + ind.ToString());
                                    return false;
                                }
                                countCommas = 1;
                                commaIndex = ind;
                            }
                            else if (input[ind] == '(')
                            {
                                countSkobes++;
                            }
                            else if (input[ind] == ')')
                            {
                                countSkobes--;
                            }
                            ind++;
                        }
                        if (countCommas != 1)
                        {
                            return false;
                        }
                        if (countSkobes != 0)
                            {

                                Console.WriteLine("Error not closed at " + ind.ToString()+' '+countSkobes.ToString());
                                return false;
                            }
                        if (!(Logs(input.Substring(i + 4, commaIndex - i - 4)) && Logs(input.Substring(commaIndex + 1, ind - commaIndex - 2))))
                        {

                            Console.WriteLine("Error inside at " + ind.ToString());
                            return false;
                        }
                        i = ind;
                    }
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
                Console.WriteLine("Enter the sentence or 0 to end");
                input = Console.ReadLine();
            }
            Console.WriteLine("Goodbye");
        }
    }
}

