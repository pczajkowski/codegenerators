using System.Text;

namespace CodeGenerators;

public class Property : IGenerator
{
    public string Name { get; }

    private readonly string type;
    public List<IGenerator> Attributes { get; } = new List<IGenerator>();

    public Property(string name, string type)
    {
        Name = name;
        this.type = type;
    }

    public string Build(int indent)
    {
        var sb = new StringBuilder();
        if (Attributes.Any())
        {
            foreach (var attribute in Attributes)
                sb.AppendLine(attribute.Build(indent));
        }
        
        sb.Append($"{(new string('\t', indent))}public {type} {Name} {{ get; set; }}");

        return sb.ToString();
    } 
}