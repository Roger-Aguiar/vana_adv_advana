using Advocacy_Software.Advocacy.Software.Entities;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Advocacy_Software.Advocacy.Software.Pages
{
    /// <summary>
    /// Interaction logic for PageImageLogo.xaml
    /// </summary>
    public partial class PageImageLogo : Page
    {
        public Signatures Signature { get; set; }
        public PageImageLogo(Signatures signature)
        {
            Signature = signature;
            InitializeComponent();
        }

        private void PageImageLogo1_Loaded(object sender, RoutedEventArgs e)
        {
            using (var stream = new MemoryStream(Signature.ImageProfile))
            {
                ImageLogo.Stretch = Stretch.Fill;
                ImageLogo.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            };
        }
    }
}
