
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
        return Regex.Replace(text, @"[^\S\n\t]+", " ");
    }
}

public class ExtraBlanksRule : Rule
{
    public override string Apply(string text)
    {
        return Regex.Replace(text, @"(\n){3,}", "\n\n");
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

public class TabulationRule : Rule
{
    public override string Apply(string text)
    {
        // Заменяем 4 пробела в начале строки на \t
        text = Regex.Replace(text, @"\n {4}", "\n\t");
        
        // Удаляем все другие табы/4 пробела в середине текста
        text = Regex.Replace(text, @"[^\n] {4}", " ");
        return text;
    }
}
public class PunctuationSpacesRule : Rule
{
    public override string Apply(string text)
    {
        // Удаляем пробелы ПЕРЕД знаками пунктуации: . , ! ? ) ] } »
        text = Regex.Replace(text, @"\s+([.,!?)\]»])", "$1");
        
        // Удаляем пробелы ПОСЛЕ знаков пунктуации: ( [ { «
        text = Regex.Replace(text, @"([(\[«{])\s+", "$1");
        
        return text;
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
            new ExtraBlanksRule(),
            new TabulationRule(),
            new ExtraSpacesRule(),
            new QuotesRule(),
            new DashRule(),
            new PunctuationSpacesRule()
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
        string inputPath = "/workspaces/PAPScs/Lab11/input.txt";
        string outputPath = "/workspaces/PAPScs/Lab11/output.txt";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Вы не создали текстовый файл. Пожалуйста, создайте файл input.txt и поместите в него Ваш текст.\n");
        }
        else{
        string text = File.ReadAllText(inputPath, Encoding.UTF8);
        Console.WriteLine($"Сырой текст: {text.Replace("\n", "\\n").Replace("\t", "\\t")}");

        var interpreter = new TextInterpreter();
        string result = interpreter.Interpret(text);

        File.WriteAllText(outputPath, result, Encoding.UTF8);
        Console.WriteLine(result.Replace("\n", "\\n").Replace("\t", "\\t"));
        Console.WriteLine($"Результат сохранён в {outputPath}");
        }
    }
}