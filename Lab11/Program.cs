
using System.Text;
using System.Text.RegularExpressions;

public abstract class Rule
{
    public abstract string Apply(string text);
}

// Удаление лишних пробелов
public class ExtraSpacesRule : Rule
{
    public override string Apply(string text)
    {
        return Regex.Replace(text, @"\s+", " ");
    }
}

public class ExtraBlanksRule : Rule
{
    public override string Apply(string text)
    {
        return Regex.Replace(text, @"\n+", "\n");
    }
}

// Замена кавычек
public class QuotesRule : Rule
{
    public override string Apply(string text)
    {
        // Замена открывающей кавычки (перед словом)
        text = Regex.Replace(text, @"(\W|^)""(\w)", "$1«$2");
        // Замена закрывающей кавычки (после слова)
        text = Regex.Replace(text, @"(\w)""(\W|$)", "$1»$2");
        return text;
    }
}


// Дефис → тире
public class DashRule : Rule
{
    public override string Apply(string text)
    {
        return Regex.Replace(text, " - ", " — ");
    }
}

// Интерпретатор текста
public class TextInterpreter
{
    private readonly Rule[] _rules;

    public TextInterpreter()
    {
        _rules = new Rule[]
        {
            new ExtraSpacesRule(),
            new QuotesRule(),
            new DashRule()
        };
    }

    public string Interpret(string text)
    {
        foreach (var rule in _rules)
        {
            text = rule.Apply(text);
        }
        return text;
    }
}


class Program
{
    static void Main()
    {
        // Чтение из файла
        string inputPath = "input.txt";
        string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Вы не создали текстовый файл. Пожалуйста, создайте файл input.txt и поместите в него Ваш текст.\n");
        }
        else{
        string text = File.ReadAllText(inputPath, Encoding.UTF8);

        var interpreter = new TextInterpreter();
        string result = interpreter.Interpret(text);

        File.WriteAllText(outputPath, result, Encoding.UTF8);
        Console.WriteLine(result);
        Console.WriteLine($"Результат сохранён в {outputPath}");
        }
    }
}