using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConnectFourWPF {
  public class Command : ICommand {
    public Command(Action action, bool canExecute = true) {
      this.action = action;
      this.canExecute = canExecute;
    }

    public Command(Action<object> parameterizedAction, bool canExecute = true) {
      this.parameterizedAction = parameterizedAction;
      this.canExecute = canExecute;
    }
    protected Action action = null;
    protected Action<object> parameterizedAction = null;
    private bool canExecute = false;

    public bool CanExecute {
      get { return canExecute; }
      set {
        if (canExecute != value) {
          canExecute = value;
          EventHandler canExecuteChanged = CanExecuteChanged;
          if (canExecuteChanged != null)
            canExecuteChanged(this, EventArgs.Empty);
        }
      }
    }

    bool ICommand.CanExecute(object parameter) {
      return canExecute;
    }

    void ICommand.Execute(object parameter) {
      this.DoExecute(parameter);
    }
    
    public event EventHandler CanExecuteChanged;
   
    protected void InvokeAction(object param) {
      Action theAction = action;
      Action<object> theParameterizedAction = parameterizedAction;
      if (theAction != null)
        theAction();
      else if (theParameterizedAction != null)
        theParameterizedAction(param);
    }
    public virtual void DoExecute(object param) {
      //  Call the action or the parameterized action, whichever has been set.
      InvokeAction(param);
    }
  }


}
