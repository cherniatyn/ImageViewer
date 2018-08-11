using ImageViewerApp.Auxiliaries;
using System.Windows;
using System.Windows.Controls;

namespace ImageViewerApp.Pages {
    public partial class DetailItemPage : Page {
        public DetailItemPage() {
            InitializeComponent();
        }

        private void _backToImageListButton_Click(object sender, RoutedEventArgs e) 
            => FrameNavigator.GoBack(() => { return new ListViewPage() { DataContext = DataContext }; });
    }
}