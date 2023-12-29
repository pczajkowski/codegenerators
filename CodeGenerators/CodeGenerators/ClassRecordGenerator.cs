using System.Text;

namespace CodeGenerators;

public class ClassRecordGenerator : IGenerator
{
    public List<string> Usings { get; } = new();
    public Dictionary<string, IGenerator> Properties { get; set; } = new();
    private readonly string namespaceName;
    public string Name { get; }
    private readonly bool isRecord;

    public ClassRecordGenerator(string namespaceName, string name, bool isRecord = false)
    {
        this.namespaceName = Helpers.SanitizeName(namespaceName);
        Name = Helpers.SanitizeName(name);
        this.isRecord = isRecord;
    }

    public string Build(int indent = 0)
    {
        var sb = new StringBuilder();
        if (Usings.Any())
        {
            foreach (var usingItem in Usings.Distinct())
                sb.AppendLine($"using {usingItem};");

            sb.AppendLine();
        }

        sb.AppendLine($"namespace {namespaceName};");
        sb.AppendLine();

        sb.AppendLine($"public {(isRecord ? "record" : "class")} {Name}\n{{");

        if (Properties.Any())
            sb.AppendLine(string.Join("\n\n", Properties.Select(x => x.Value.Build(indent+1))));

        sb.AppendLine("}");

        return sb.ToString();
    }
}