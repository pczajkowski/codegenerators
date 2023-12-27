using CodeGenerators;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGeneratorsTests;

public class ClassRecordGeneratorTests
{
    [Fact]
    public void BuildTest()
    {
        var test = new ClassRecordGenerator("TestNamespace", "Test");
        test.Usings.Add("ExcelORM");
        
        var property = new Property("TestProperty", "string");
        property.Attributes.Add(new AttributeElement("Column", "Test Property"));
        test.Properties.Add(property.Name, property);

        var secondProperty = new Property("SecondProperty", "int");
        secondProperty.Attributes.Add(new AttributeElement("Column", "Second Property"));
        test.Properties.Add(secondProperty.Name, secondProperty);

        var result = test.Build();
        Assert.NotEmpty(result);
        
        var tree = CSharpSyntaxTree.ParseText(result);
        var diag = tree.GetDiagnostics();
        Assert.False(diag.Any());
    }
}