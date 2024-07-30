using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class PDFViewer : WebView
    {
        public string Uri { get { return (string) GetValue(UriProperty); } set { SetValue(UriProperty, value); } }
        public static readonly BindableProperty UriProperty = BindableProperty.Create(
            nameof(Uri),
            typeof(string),
            typeof(PDFViewer));

        public bool IsPDFUri { get { return (bool)GetValue(IsPDFUriProperty); } set { SetValue(IsPDFUriProperty, value); } }
        public static readonly BindableProperty IsPDFUriProperty = BindableProperty.Create(
            nameof(IsPDFUri),
            typeof(bool),
            typeof(PDFViewer));
    }
}
