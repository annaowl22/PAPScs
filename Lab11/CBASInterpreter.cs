using System;
using System.Collections.Generic;
using System.Linq;

public class CBASInterpreter
{
    private List<string> tokens = new List<string>();
    private int currentTokenIndex = 0;
    private Dictionary<string, int> variables = new Dictionary<string, int>();
    private List<string> errors = new List<string>();
    private HashSet<string> keywordTable = new HashSet<string>
    {
        "print", "scan", "for", "if", "else", "to"
    };

    public List<string> Errors => errors;

    private void ReportError(string message)
    {
        errors.Add($"Error: {message}");
    }

    private string GetCurrentToken()
    {
        if (currentTokenIndex < tokens.Count)
            return tokens[currentTokenIndex];
        return "";
    }

    private void AdvanceToken()
    {
        if (currentTokenIndex < tokens.Count)
            currentTokenIndex++;
    }

    private bool Match(string expected)
    {
        if (GetCurrentToken() == expected)
        {
            AdvanceToken();
            return true;
        }
        return false;
    }

    private bool IsIdentifier(string token)
    {
        if (string.IsNullOrEmpty(token))
            return false;
        if (!char.IsLetter(token[0]) && token[0] != '_')
            return false;

        return token.All(c => char.IsLetterOrDigit(c) || c == '_') &&
               !keywordTable.Contains(token);
    }

    private bool IsNumber(string token)
    {
        return !string.IsNullOrEmpty(token) &&
               token.All(char.IsDigit);
    }

    public void Interpret(List<string> programTokens)
    {
        tokens = programTokens;
        currentTokenIndex = 0;
        errors.Clear();
        variables.Clear();

        try
        {
            ParseProgram();
        }
        catch (Exception e)
        {
            ReportError($"Runtime error: {e.Message}");
        }
    }

    private int ParseFactor()
    {
        string token = GetCurrentToken();

        if (Match("("))
        {
            int result = ParseExpression();
            if (!Match(")"))
            {
                ReportError("Expected ')'");
            }
            return result;
        }
        else if (IsIdentifier(token))
        {
            AdvanceToken();
            if (!variables.ContainsKey(token))
            {
                variables[token] = 0;
            }
            return variables[token];
        }
        else if (IsNumber(token))
        {
            AdvanceToken();
            return int.Parse(token);
        }
        else
        {
            ReportError("Expected identifier, number, or expression");
            return 0;
        }
    }

    private int ParseTerm()
    {
        int result = ParseFactor();

        while (true)
        {
            if (Match("*"))
            {
                result *= ParseFactor();
            }
            else if (Match("/"))
            {
                int divisor = ParseFactor();
                if (divisor == 0)
                {
                    ReportError("Division by zero");
                    divisor = 1;
                }
                result /= divisor;
            }
            else
            {
                break;
            }
        }

        return result;
    }

    private int ParseExpression()
    {
        int result = ParseTerm();

        while (true)
        {
            if (Match("+"))
            {
                result += ParseTerm();
            }
            else if (Match("-"))
            {
                result -= ParseTerm();
            }
            else
            {
                break;
            }
        }

        return result;
    }

    private bool ParseBoolExpression()
    {
        int left = ParseExpression();

        string op = GetCurrentToken();
        if (op == "<" || op == ">" || op == "==" || op == "!=")
        {
            AdvanceToken();
        }
        else
        {
            ReportError($"Expected relational operator, got '{op}'");
            return false;
        }

        int right = ParseExpression();

        if (op == "<") return left < right;
        if (op == ">") return left > right;
        if (op == "==") return left == right;
        if (op == "!=") return left != right;

        return false;
    }

    private void ParsePrint()
    {
        if (!Match("print"))
        {
            ReportError("Expected 'print'");
            return;
        }

        while (true)
        {
            string token = GetCurrentToken();

            if (token.Length >= 2 && token[0] == '"' && token[^1] == '"')
            {
                string str = token.Substring(1, token.Length - 2);
                Console.Write(str);
                AdvanceToken();
            }
            else
            {
                int value = ParseExpression();
                Console.Write(value);
            }

            if (!Match(",")) break;
        }

        if (!Match(";"))
        {
            ReportError("Expected ';' after print");
        }
        else
        {
            Console.WriteLine();
        }
    }

    private void ParseScan()
    {
        if (!Match("scan"))
        {
            ReportError("Expected 'scan'");
            return;
        }

        string varName = GetCurrentToken();
        if (!IsIdentifier(varName))
        {
            ReportError("Expected identifier after scan");
            return;
        }
        AdvanceToken();

        Console.Write($"Enter value for {varName}: ");
        if (int.TryParse(Console.ReadLine(), out int value))
        {
            variables[varName] = value;
        }
        else
        {
            ReportError($"Invalid integer input for {varName}");
            variables[varName] = 0;
        }

        if (!Match(";"))
        {
            ReportError("Expected ';' after scan");
        }
    }

    private void ParseFor()
    {
        if (!Match("for"))
        {
            ReportError("Expected 'for'");
            return;
        }

        string varName = GetCurrentToken();
        if (!IsIdentifier(varName))
        {
            ReportError("Expected identifier in for loop");
            return;
        }
        AdvanceToken();

        if (!Match("="))
        {
            ReportError("Expected '=' in for loop");
            return;
        }

        int startValue = ParseExpression();

        if (!Match("to"))
        {
            ReportError("Expected 'to' in for loop");
            return;
        }

        int endValue = ParseExpression();

        if (!Match("{"))
        {
            ReportError("Expected '{' after for");
            return;
        }

        int loopBodyStart = currentTokenIndex;
        int braceCount = 1;

        // Находим конец тела цикла
        while (currentTokenIndex < tokens.Count && braceCount > 0)
        {
            if (GetCurrentToken() == "{") braceCount++;
            else if (GetCurrentToken() == "}") braceCount--;
            AdvanceToken();
        }

        int loopBodyEnd = currentTokenIndex;

        // Выполняем цикл
        variables[varName] = startValue;
        while (variables[varName] <= endValue)
        {
            currentTokenIndex = loopBodyStart;

            // Выполняем тело цикла
            int tempIndex = currentTokenIndex;
            int tempBraceCount = 1;
            while (tempIndex < loopBodyEnd - 1 && tempBraceCount > 0)
            {
                currentTokenIndex = tempIndex;
                ParseStatement();

                tempIndex = currentTokenIndex;
                if (tempIndex < tokens.Count)
                {
                    if (tokens[tempIndex] == "{") tempBraceCount++;
                    else if (tokens[tempIndex] == "}") tempBraceCount--;
                }
            }

            variables[varName]++;
        }

        currentTokenIndex = loopBodyEnd;
    }

    private void ParseIf()
    {
        if (!Match("if"))
        {
            ReportError("Expected 'if'");
            return;
        }

        bool condition = ParseBoolExpression();

        if (!Match("{"))
        {
            ReportError("Expected '{' after if condition");
            return;
        }

        if (condition)
        {
            // Выполняем блок if
            while (currentTokenIndex < tokens.Count && GetCurrentToken() != "}")
            {
                ParseStatement();
            }

            if (!Match("}"))
            {
                ReportError("Expected '}' after if block");
                return;
            }

            // Пропускаем блок else, если он есть
            if (GetCurrentToken() == "else")
            {
                AdvanceToken(); // Пропускаем 'else'

                if (Match("{"))
                {
                    // Пропускаем весь блок else
                    int braceCount = 1;
                    while (currentTokenIndex < tokens.Count && braceCount > 0)
                    {
                        if (GetCurrentToken() == "{") braceCount++;
                        else if (GetCurrentToken() == "}") braceCount--;
                        AdvanceToken();
                    }
                }
            }
        }
        else
        {
            // Пропускаем блок if
            int braceCount = 1;
            while (currentTokenIndex < tokens.Count && braceCount > 0)
            {
                if (GetCurrentToken() == "{") braceCount++;
                else if (GetCurrentToken() == "}") braceCount--;
                AdvanceToken();
            }

            // Проверяем наличие else
            if (Match("else"))
            {
                if (!Match("{"))
                {
                    ReportError("Expected '{' after else");
                    return;
                }

                // Выполняем блок else
                while (currentTokenIndex < tokens.Count && GetCurrentToken() != "}")
                {
                    ParseStatement();
                }

                if (!Match("}"))
                {
                    ReportError("Expected '}' after else block");
                }
            }
        }
    }

    private void ParseStatement()
    {
        string token = GetCurrentToken();

        if (token == "print")
        {
            ParsePrint();
        }
        else if (token == "scan")
        {
            ParseScan();
        }
        else if (token == "for")
        {
            ParseFor();
        }
        else if (token == "if")
        {
            ParseIf();
        }
        else if (IsIdentifier(token))
        {
            string varName = token;
            AdvanceToken();

            if (!Match("="))
            {
                ReportError("Expected '=' in assignment");
                return;
            }

            int value = ParseExpression();
            variables[varName] = value;

            if (!Match(";"))
            {
                ReportError("Expected ';' after assignment");
            }
        }
        else if (token == "}" || token == "{")
        {
            AdvanceToken();
        }
        else if (!string.IsNullOrEmpty(token))
        {
            ReportError($"Unexpected token: {token}");
            AdvanceToken();
        }
    }

    private void ParseProgram()
    {
        while (currentTokenIndex < tokens.Count)
        {
            ParseStatement();
        }
    }
}