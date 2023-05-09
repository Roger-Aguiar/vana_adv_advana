using Advocacy_Software.Advocacy.Software.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
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
            if(Signature.ImageProfile != null) 
                ImageLogo.Source = new BitmapImage(new Uri(Signature.ImageProfile));
        }
    }
}
