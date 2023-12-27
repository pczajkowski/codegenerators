namespace CodeGenerators;

public class AttributeElement : IGenerator
{
    private readonly string name;
    private readonly string value;
    
    public AttributeElement(string name, string value)
    {
        this.name = name;
        this.value = value;
    }

    public string Build(int indent) => $"{new string('\t', indent)}[{name}(\"{value}\")]";
}