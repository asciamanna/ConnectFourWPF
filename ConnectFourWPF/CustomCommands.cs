using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConnectFourWPF {
  public static class CustomCommands {
    static RoutedCommand exitCommand;

    static CustomCommands() {
      exitCommand = new RoutedCommand("Exit", typeof(CustomCommands));
    }

    public static RoutedCommand Exit {
      get { return exitCommand; }
    }
  }
}
