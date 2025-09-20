using System;
using System.Runtime.CompilerServices;

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
                        Console.WriteLine("Opened without sign between skobes");
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
                        Console.WriteLine("Closing without opening");
                        return false;
                    }
                }
                else if (char.IsDigit(input[i]) || input[i] == '.')
                {
                    if (!wassign && wasclosed)
                    {
                        Console.WriteLine("Number without sign after skobes "+i.ToString());
                        return false;
                    }
                }
                else if (IsSign(input[i])||input[i]==',')
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
                            if (input[ind] == ',' && countSkobes == 1)
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
            return Floats(input) && Signs(input) && Skobe(input) && Words(input) && Logs(input);
        }
    }

    class Calculator
    {
        public static bool hasError = false;
        public static int seekPlusMinus(string input)
        {
            int countSkobes = 0;
            for (int i = input.Length - 1; i > 0; i--)
            {
                if (input[i] == ')')
                {
                    countSkobes++;
                }
                else if (input[i] == '(')
                {
                    countSkobes--;
                }
                else if (input[i] == '+')
                {
                    if (countSkobes == 0)
                    {
                        return i;
                    }
                }
                else if (input[i] == '-')
                {
                    if (countSkobes == 0 && (!Check.IsSign(input[i - 1]) || input[i - 1] == '('))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int seekMulDiv(string input)
        {
            int countSkobes = 0;
            for (int i = input.Length - 1; i > 0; i--)
            {
                if (input[i] == ')')
                {
                    countSkobes++;
                }
                else if (input[i] == '(')
                {
                    countSkobes--;
                }
                else if (input[i] == '*' || input[i] == '/')
                {
                    if (countSkobes == 0)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        static int seekComma(string input)
        {
            int countSkobes = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    countSkobes++;
                }
                else if (input[i] == ')')
                {
                    countSkobes--;
                }
                else if (input[i] == ',')
                {
                    if (countSkobes == 0)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        static float makeDivision(float left, float right)
        {
            if (right == 0)
            {
                hasError = true;
                Console.WriteLine("Division by zero");
                return 0;
            }
            else
            {
                return left / right;
            }
        }
        static float getLog(float num, float osn)
        {
            if (num > 0 && osn > 0 && osn != 1)
            {
                return (float)Math.Log(num) / (float)Math.Log(osn);
            }
            else
            {
                Console.WriteLine("Incorrect argument in logarythm");
                hasError = true;
                return 0;
            }
        }
        static float makeNumber(string input)
        {
            int negative;
            bool hasDot = false;
            int afterDot = 1;
            float result = 0;
            if (input[0] == '-')
            {
                negative = 1;
            }
            else
            {
                negative = 0;
            }
            for (int i = negative; i < input.Length; i++)
            {
                if (input[i] == '.')
                {
                    hasDot = true;
                }
                else if (hasDot)
                {
                    result += (input[i] - '0') * (float)Math.Pow(0.1, afterDot);
                }
                else
                {
                    result *= 10;
                    result += input[i] - '0';
                }
            }
            if (negative == 1)
            {
                result *= -1;
            }
            return result;

        }
        public static float solve(string input)
        {
            int index = -1;
            string left;
            string right;
            float leftResult;
            float rightResult;
            Console.WriteLine("Solving for " + input);
            index = seekPlusMinus(input);
            if (index != -1)
            {
                left = input.Substring(0, index);
                leftResult = solve(left);
                right = input.Substring(index + 1, input.Length - index - 1);
                rightResult = solve(right);
                if (hasError)
                {
                    return 0;
                }
                if (input[index] == '+')
                {
                    return leftResult + rightResult;
                }
                else
                {
                    return leftResult - rightResult;
                }
            }
            index = seekMulDiv(input);
            if (index != -1)
            {
                left = input.Substring(0, index);
                leftResult = solve(left);
                right = input.Substring(index + 1, input.Length - index - 1);
                rightResult = solve(right);
                if (hasError)
                {
                    return 0;
                }
                if (input[index] == '*')
                {
                    return leftResult * rightResult;
                }
                else
                {
                    return makeDivision(leftResult, rightResult);
                }
            }
            if (input[0] == '(')
            {
                return solve(input.Substring(1, input.Length - 2));
            }
            if (input[0] == 'l')
            {
                index = seekComma(input.Substring(4, input.Length - 5));
                Console.WriteLine("Index of comma is " + index.ToString());
                left = input.Substring(4, index);
                right = input.Substring(index + 5, input.Length - index - 6);
                leftResult = solve(left);
                rightResult = solve(right);
                if (hasError)
                {
                    return 0;
                }
                return getLog(leftResult, rightResult);
            }
            return makeNumber(input);
        }
        public static float StartSolve(string input)
        {
            hasError = false;
            if (input == "")
            {
                Console.WriteLine("Empty");
                return 0;
            }
            if (!Check.AllChecks(input))
            {
                Console.WriteLine("Incorrect");
                return 0;
            }
            float result = solve(input.Replace(" ",""));
            if (hasError)
            {
                Console.WriteLine("Error in process");
                return 0;
            }
            Console.WriteLine("The answer is " + result.ToString());
            return 1;
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
                if (Calculator.StartSolve(input)==0)
                {
                    Console.WriteLine("The sentence is incorrect. Try again");
                }
                Console.WriteLine("Enter the sentence or 0 to end");
                input = Console.ReadLine();
            }
            Console.WriteLine("Goodbye");
        }
    }
}

