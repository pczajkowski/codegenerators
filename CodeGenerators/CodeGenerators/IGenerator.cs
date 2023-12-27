namespace CodeGenerators;

public interface IGenerator
{
    string Build(int indent);
}