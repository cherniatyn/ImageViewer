using ImageViewerApp.Auxiliaries;
using ImageViewerApp.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImageViewerApp.Pages {
    public partial class ListViewPage : Page {
        public ListViewPage() {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            //if (DataContext is ImagesViewModel vm && vm.CheckExistingAndRemoveNonexistentImage(vm.CurrentIndex, vm.CurrentImage))
                FrameNavigator.GoForward(() => { return new DetailItemPage() { DataContext = DataContext }; });
        }
    }
}