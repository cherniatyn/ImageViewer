﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ImageViewerApp.Auxiliaries {
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}