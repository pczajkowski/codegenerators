using System.Text;

namespace CodeGenerators;

public class ClassRecordGenerator : IGenerator
{
    public List<string> Usings { get; set; } = new List<string>();
    public Dictionary<string, Property> Properties { get; set; } = new Dictionary<string, Property>();
    private readonly string namespaceName;
    private readonly string name;
    private readonly bool isRecord;

    public ClassRecordGenerator(string namespaceName, string name, bool isRecord = false)
    {
        this.namespaceName = namespaceName;
        this.name = name;
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

        sb.AppendLine($"public {(isRecord ? "record" : "class")} {name}\n{{");
        indent++;

        if (Properties.Any())
            sb.AppendLine(string.Join("\n\n", Properties.Select(x => x.Value.Build(indent))));

        indent--;
        sb.AppendLine("}");

        return sb.ToString();
    }
}