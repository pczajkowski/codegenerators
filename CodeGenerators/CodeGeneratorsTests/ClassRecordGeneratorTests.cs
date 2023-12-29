using CodeGenerators;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGeneratorsTests;

public class ClassRecordGeneratorTests
{
    [Fact]
    public void BuildTest()
    {
        var test = new ClassRecordGenerator("TestNamespace", "test Class Name");
        test.Usings.Add("ExcelORM");

        const string firstPropertyName = "test Property";
        var property = new Property(firstPropertyName, "string");
        property.Attributes.Add(new AttributeElement("Column", firstPropertyName));
        test.Properties.Add(property.Name, property);

        const string secondPropertyName = "second Property";
        var secondProperty = new Property(secondPropertyName, "int");
        secondProperty.Attributes.Add(new AttributeElement("Column", secondPropertyName));
        test.Properties.Add(secondProperty.Name, secondProperty);

        var result = test.Build();
        Assert.NotEmpty(result);
        
        var tree = CSharpSyntaxTree.ParseText(result);
        var diag = tree.GetDiagnostics();
        Assert.False(diag.Any());
    }
}