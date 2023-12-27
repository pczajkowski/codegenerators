using System.Text;

namespace CodeGenerators;

public class Property : IGenerator
{
    private readonly string name;
    private readonly string type;
    public List<AttributeElement> Attributes { get; set; } = new List<AttributeElement>();

    public Property(string name, string type)
    {
        this.name = name;
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
        
        sb.Append($"{(new string('\t', indent))}public {type} {name} {{ get; set; }}");

        return sb.ToString();
    } 
}