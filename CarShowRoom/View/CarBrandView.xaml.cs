using CarShowRoom.DataSource;
using CarShowRoom.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CarShowRoom.View
{
    /// <summary>
    /// Description for CarBrandView.
    /// </summary>
    public partial class CarBrandView : Window
    {
        /// <summary>
        /// Initializes a new instance of the CarBrandView class.
        /// </summary>
        public CarBrandView()
        {
            InitializeComponent();
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString().ToLower() == "id"||  
                e.Column.Header.ToString().ToLower() == "logo"||
                e.Column.Header.ToString().ToLower() == "carbrands")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }

}