using System.Windows;
using CarShowRoom.ViewModel;
using CarShowRoom.DataSource;
using System.Windows.Threading;
using System;

namespace CarShowRoom.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

    }
}