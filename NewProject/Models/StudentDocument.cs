using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using NewProject.Controllers;
using QuestPDF.Helpers;
public class StudentDocument : IDocument
{
    private readonly Student _student;

    public StudentDocument(Student student)
    {
        _student = student;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Item().Text($"Student ID: {_student.Id}");
                        column.Item().Text($"Name: {_student.Name}");
                        column.Item().Text($"Address: {_student.Address}");
                        column.Item().Text($"Stage: {_student.Stage?.Name ?? "N/A"}");
                        column.Item().Text($"Grade: {_student.Grade?.Name ?? "N/A"}");
                        column.Item().Text($"Class: {_student.Classes?.Name ?? "N/A"}");
                        
                    });
            });
    }
}
