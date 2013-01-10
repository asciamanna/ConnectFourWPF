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

namespace ConnectFourWPF {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
      this.DataContext = new ConnectFourViewModel();
    }

    void ButtonClick(object sender, RoutedEventArgs e) {
      var button = sender as Button;
      if (button != null) {
        int column = (int)button.GetValue(Grid.ColumnProperty);
        (DataContext as ConnectFourViewModel).PlayDiscCommand.Execute(column);
       
       //var ellipse = board.FindName("Cell" + cell.Row + cell.Column) as Ellipse;
       //ellipse.Fill = cell.Disc == Disc.Red ? Brushes.Red : Brushes.Black;
      }
    }

    void OnExit(object sender, ExecutedRoutedEventArgs e) {
      Application.Current.Shutdown();
    }
  }
}
