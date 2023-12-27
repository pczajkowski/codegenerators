using CodeGenerators;

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
        test.Properties.Add(property);

        var result = test.Build();
        Assert.NotEmpty(result);
    }
}