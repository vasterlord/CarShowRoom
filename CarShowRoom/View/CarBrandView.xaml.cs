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
            e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            if (e.Column.Header.ToString().ToLower() == "cars"
                || e.Column.Header.ToString().ToLower() == "logo"
                || e.Column.Header.ToString().ToLower() == "carbrands"
                || e.Column.Header.ToString().ToLower() == "carbrandid"
                || e.Column.Header.ToString().ToLower() == "clients")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }

}