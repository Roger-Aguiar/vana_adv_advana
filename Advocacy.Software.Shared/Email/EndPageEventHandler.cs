using iText.IO.Font.Constants;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using System.Windows;
using System.IO;
using Advocacy_Software.Advocacy.Software.Entities;

public class EndPageEventHandler : IEventHandler
{
    protected Document document;
    protected PdfDocument pdfDocument;
    protected Lawyer lawyer;

    public EndPageEventHandler(Document document, PdfDocument pdfDocument, Lawyer lawyer)
    {
        this.document = document;
        this.pdfDocument = pdfDocument;
        this.lawyer = lawyer;
    }

    public void HandleEvent(Event currentEvent)
    {
        PdfDocumentEvent pdfDocumentEvent = (PdfDocumentEvent)currentEvent;
        PdfPage page = pdfDocumentEvent.GetPage();
        iText.Kernel.Geom.Rectangle pageSize = page.GetPageSize();
        PdfFont? font;

        try
        {
            font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
        }
        catch (IOException exception)
        {
            MessageBox.Show($"{exception.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }

        float leftX = pageSize.GetLeft() + document.GetLeftMargin();
        float centerX = (pageSize.GetLeft() + document.GetLeftMargin() + (pageSize.GetRight() - document.GetRightMargin())) / 2;
        float rightX = pageSize.GetRight() - document.GetRightMargin();

        float headerY = pageSize.GetTop() - document.GetTopMargin() + 25;
        float footerY = document.GetBottomMargin() - 15;

        Canvas canva = new Canvas(pdfDocumentEvent.GetPage(), pageSize);
        
        //Header                
        canva.SetFont(font).SetFontSize(18).ShowTextAligned($"Dr(a). {lawyer.Name}\n{lawyer.Profession?.ToUpper()}", centerX, headerY, iText.Layout.Properties.TextAlignment.CENTER);

        //Footer
        canva.SetFont(font).SetFontSize(10).ShowTextAligned($"Dr(a). {lawyer.Name} - OAB-{lawyer.UfOab} nº{lawyer.OabNumber}", leftX, footerY, iText.Layout.Properties.TextAlignment.LEFT);
    }    
}
