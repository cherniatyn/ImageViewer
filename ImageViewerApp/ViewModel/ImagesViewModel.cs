using ImageViewerApp.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace ImageViewerApp.ViewModel {
    public class ImagesViewModel : NotifyPropertyChanged {
        private RelayCommand _loadPreviousImageCommand;
        private RelayCommand _loadNextImageCommand;

        public ImagesViewModel() {
            Images = new ObservableCollection<string>() {
                //@"E:\Frontend_Course\Html_css\Lk_7\images\background.jpg",
            };
        }

        public ObservableCollection<string> Images { get; set; }

        private string _currentImage;
        public string CurrentImage {
            get { return _currentImage; }
            set {
                _currentImage = value;
                OnPropertyChanged(nameof(CurrentImage));
            }
        }

        private int _currentIndex;
        public int CurrentIndex {
            get { return _currentIndex; }
            set {
                _currentIndex = value;
                CurrentImage = Images[_currentIndex];
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        //public bool CheckExistingAndRemoveNonexistentImage(int index, string path) {
        //    if (!File.Exists(path)) {
        //        if (Images.Count == 1) {
        //            Images.Clear();
        //            _currentIndex = 0;
        //        }
        //        else {
        //            Images.Remove(Images[index]);
        //            if (index <= _currentIndex)
        //                CurrentIndex--;
        //        }
        //        MessageBox.Show($"Could not found the image {path}", "Warning", MessageBoxButton.OK);
        //        return false;
        //    }
        //    return true;
        //}

        public RelayCommand LoadPreviousImageCommand {
            get {
                return _loadPreviousImageCommand ??
                  (_loadPreviousImageCommand = new RelayCommand((o) => {
                      if (Images.Count == 1)
                          return;

                      int index = _currentIndex;
                      if (_currentIndex == 0)
                          index = Images.Count - 1;
                      else
                          index--;

                      //if (CheckExistingAndRemoveNonexistentImage(index, Images[index]))
                      CurrentIndex = index;
                  }));
            }
        }

        public RelayCommand LoadNextImageCommand {
            get {
                return _loadNextImageCommand ??
                  (_loadNextImageCommand = new RelayCommand((o) => {
                      if (Images.Count == 1)
                          return;

                      int index = _currentIndex;
                      if (_currentIndex == Images.Count - 1)
                          index = 0;
                      else
                          index++;

                      //if (CheckExistingAndRemoveNonexistentImage(index, Images[index]))
                      CurrentIndex = index;
                  }));
            }
        }

        public void UIElement_OnDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] pathes = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string path in pathes) {
                    if (!string.IsNullOrWhiteSpace(Path.GetExtension(path))) {
                        if (IsImage(path.ToUpperInvariant()))
                            AddUniqueImagePath(path);
                    }
                    else
                        DirectoryInfoScrap(path);
                }
            }
        }

        private void DirectoryInfoScrap(string sDir) {
            try {
                foreach (string file in Directory.GetFiles(sDir)) {
                    if (IsImage(file.ToUpperInvariant()))
                        AddUniqueImagePath(file);
                }

                // for recursive directory scan
                if (bool.TryParse(ConfigurationManager.AppSettings["CanRecursiveScanCategories"], out bool check) && check)
                    foreach (string dir in Directory.GetDirectories(sDir))
                        DirectoryInfoScrap(dir);


            } catch (Exception exc) {
                if (exc is UnauthorizedAccessException || exc is InvalidOperationException)
                    Trace.WriteLine(string.Format("{0} cannot be accessible.", sDir));
                else
                    Trace.WriteLine(string.Format("Error while scraping {0}. Message: {1}", sDir, exc.ToString()));
            }
        }

        private bool IsImage(string path) => _imageFormats.Any(x => path.EndsWith(x));

        private void AddUniqueImagePath(string path) {
            if (!Images.Contains(path))
                Images.Add(path);
        }

        private List<string> _imageFormats = new List<string> {
            ".JPG",
            ".JXR",
            ".JPEG",
            ".PNG",
            ".BMP",
            ".GIF",
            ".ICO",
            ".TIFF",
            ".GIF",
            ".BMP"
        };
    }
}