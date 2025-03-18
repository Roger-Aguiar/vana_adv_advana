namespace Advocacy_Software.Advocacy.Software.Shared.Utils
{
    public class DocumentFormat
    {
        public Paragraph SetTitle(string title)
        {
            var _title = new Paragraph();
            _title.SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));

            if (title == "PROCURAÇÃO")
            {
                _title.SetFontSize(22);
            }
            else if (title == "(Assinatura)")
            {
                _title.SetFontSize(10);
            }
            else
            {
                _title.SetFontSize(11);
            }

            _title.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            _title.SetUnderline();
            _title.Add(title);

            return _title;
        }

        
        public Paragraph SetBody(string body)
        {
            var _body = new Paragraph();
            _body.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            _body.Add(body);
            _body.SetFontSize(10);
            return _body;
        }

        public Paragraph SetBodyAsJustified(string body)
        {
            var _body = new Paragraph();
            _body.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED);
            _body.SetFontSize(10);
            _body.Add(body);

            return _body;
        }

        public Paragraph SetBodyAsJustifiedInline(string body)
        {
            var _body = new Paragraph();
            _body.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED);
            _body.SetUnderline();
            _body.SetFontSize(10);
            _body.Add(body);

            return _body;
        }
    }
}
