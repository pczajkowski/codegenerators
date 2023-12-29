using System.ComponentModel;
using ClosedXML.Excel;
using CodeGenerators;
using ExcelInfo;
using Spectre.Cli;

namespace GeneratorForExcelORM;

internal sealed class GeneratorCmd : Command<GeneratorCmd.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("Path to XLSX file.")]
        [CommandArgument(0, "[inputPath]")]
        public string? InputPath { get; init; }

        [Description("Start from n row. Default 1.")]
        [DefaultValue((uint)1)]
        [CommandOption("-s|--startFrom")]
        public uint StartFrom { get; init; }
    }

    private static string GetType(XLDataType type) =>
        type switch
        {
            XLDataType.Number => "double?",
            XLDataType.Text => "string?",
            XLDataType.DateTime => "DateTime?",
            XLDataType.TimeSpan => "TimeSpan?",
            _ => throw new Exception($"Can't match {type}!")
        };

    private static void GenerateRecord(WorksheetRecord worksheet, string outputFolder, string filename)
    {
        var recordObject = new ClassRecordGenerator(filename, worksheet.Name, true);
        var outputPath = Path.Combine(outputFolder, $"{recordObject.Name}.cs");
        recordObject.Usings.Add("ExcelORM");

        foreach (var column in worksheet.Columns)
        {
            if (string.IsNullOrWhiteSpace(column.Name)) continue;
            
            var propertyObject = new Property(column.Name, GetType(column.Type));
            propertyObject.Attributes.Add(new AttributeElement("Column", column.Name));
            
            if (!recordObject.Properties.TryAdd(propertyObject.Name, propertyObject))
                Console.Error.WriteLine($"Duplicated property {propertyObject.Name}!");
        }
        
        File.WriteAllText(outputPath, recordObject.Build());
    }

    private static void Generate(Settings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.InputPath)) return;
        
        var worksheetsInfo = WorkbookInfo.GetInfoOnWorksheets(settings.InputPath, settings.StartFrom).ToArray();
        if (worksheetsInfo.Length == 0) throw new Exception("Nothing to process!");

        var outputFolder = Path.GetDirectoryName(settings.InputPath);
        if (string.IsNullOrWhiteSpace(outputFolder))
            throw new Exception($"Can't establish folder from {settings.InputPath}!");
        
        var filename = Path.GetFileNameWithoutExtension(settings.InputPath);
        foreach(var worksheet in worksheetsInfo)
            GenerateRecord(worksheet, outputFolder, filename);
    }
    
    public override int Execute(CommandContext context, Settings settings)
    {
        if (!File.Exists(settings.InputPath)) throw new FileNotFoundException(settings.InputPath);

        Generate(settings);
        return 0;
    } 
}