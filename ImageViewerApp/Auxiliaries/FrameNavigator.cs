using System;
using System.Windows.Controls;

namespace ImageViewerApp.Auxiliaries {
    public static class FrameNavigator {
        public static Frame MainFrame { get; private set; }
        public static void GoBack(Func<Page> page) {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
            else
                MainFrame.Navigate(page.Invoke());
        }

        public static void GoForward(Func<Page> page) {
            if (MainFrame.CanGoForward)
                MainFrame.GoForward();
            else
                MainFrame.Navigate(page.Invoke());
        }

        internal static void SetMainFrame(Frame frame) => MainFrame = frame;
    }
}