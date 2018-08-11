using ImageViewerApp.Auxiliaries;
using ImageViewerApp.Pages;
using System.Windows;
using System.Windows.Input;

namespace ImageViewerApp {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            FrameNavigator.SetMainFrame(mainContentFrm);
            FrameNavigator.GoForward(() => { return new ListViewPage() { DataContext = DataContext }; });
        }

        public static RoutedCommand GoBackCommand = new RoutedCommand();
        private void ExecutedGoBackCommand(object sender, ExecutedRoutedEventArgs e)
            => FrameNavigator.GoBack(() => { return new ListViewPage() { DataContext = DataContext }; });

        private void CanExecuteGoBackCommand(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = mainContentFrm.Content is DetailItemPage;
    }
}