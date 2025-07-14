using ReparaStoreApp.WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReparaStoreApp.WPF.Views.Reparaciones
{
    /// <summary>
    /// Lógica de interacción para ReparacionesView.xaml
    /// </summary>
    public partial class ReparacionesView : UserControl
    {
        public ReparacionesView()
        {
            InitializeComponent();
            // Establecer la distribución a mitad y mitad al cargar
            Loaded += ReparacionesView_Loaded;
        }

        private void ReparacionesView_Loaded(object sender, RoutedEventArgs e)
        {
            ResetSplitter(); // Llama al método al cargar
        }

        private void ResetSplitter_Click(object sender, RoutedEventArgs e)
        {
            ResetSplitter(); // Llama también desde el botón
        }

        private void ResetSplitter()
        {
            // Asegura que las RowDefinitions estén definidas correctamente
            if (this.Content is Grid grid && grid.RowDefinitions.Count >= 3)
            {
                var totalHeight = ActualHeight - 5; // Splitter = 5
                if (totalHeight <= 0) return;

                double half = totalHeight / 2;

                // Aplica nuevas alturas relativas
                grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            }
        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            var hitTest = VisualTreeHelper.HitTest(dataGrid, e.GetPosition(dataGrid));
            var cell = hitTest.VisualHit.GetParentOfType<DataGridCell>();

            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                dataGrid.CurrentCell = new DataGridCellInfo(cell);
                dataGrid.BeginEdit();
                e.Handled = true;
            }
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.IsReadOnly)
            {
                e.Cancel = true;
            }
        }
    }
}
