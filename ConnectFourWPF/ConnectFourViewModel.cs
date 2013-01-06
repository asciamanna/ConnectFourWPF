using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConnectFourWPF {
  public class ConnectFourViewModel : BaseNotifyPropertyChanged {
    readonly ICommand startGameCommand;
    readonly ICommand playDiscCommand;
    GameBoard gameBoard;
    ObservableCollection<string> boardLocationColors;
    string redPlayersTurn;
    string blackPlayersTurn;
    string redPlayerWins;
    string blackPlayerWins;
    string isBoardEnabled;

    public ConnectFourViewModel() {
      startGameCommand = new Command(this.InitializeGame);
      playDiscCommand = new Command(this.PlayDisc);
      InitializeGame();
    }

    //TODO: Refactor...move to a conversion class
    string ConvertDiscToFillColor(Disc disc) {
      if (disc == Disc.Red) {
        return "Red";
      }
      if (disc == Disc.Black) {
        return "Black";
      }
      return "AliceBlue";
    }

    public void InitializeGame() {
      gameBoard = new GameBoard();
      gameBoard.Initialize();
      BoardLocationColors = new ObservableCollection<string>
        (Enumerable.Repeat<string>("AliceBlue", GameBoard.MaxRow * GameBoard.MaxColumn));
      RedPlayersTurn = "Visible";
      BlackPlayersTurn = "Hidden";
      RedPlayerWins = "Hidden";
      BlackPlayerWins = "Hidden";
      CurrentPlayerDisc = Disc.Red;
      IsBoardEnabled = "True";
    }

    public ICommand StartGameCommand { get { return startGameCommand; } }
    public ICommand PlayDiscCommand { get { return playDiscCommand; } }

    public ObservableCollection<string> BoardLocationColors {
      get { return boardLocationColors; }
      private set {
        boardLocationColors = value;
        FirePropertyChanged("BoardLocationColors");
      }
    }

    public string RedPlayersTurn {
      get { return redPlayersTurn; }
      private set {
        redPlayersTurn = value;
        FirePropertyChanged("RedPlayersTurn");
      }
    }

    public string BlackPlayersTurn {
      get { return blackPlayersTurn; }
      private set {
        blackPlayersTurn = value;
        FirePropertyChanged("BlackPlayersTurn");
      }
    }

    public string RedPlayerWins {
      get { return redPlayerWins; }
      private set {
        redPlayerWins = value;
        FirePropertyChanged("RedPlayerWins");
      }
    }

    public string BlackPlayerWins {
      get { return blackPlayerWins; }
      private set {
        blackPlayerWins = value;
        FirePropertyChanged("BlackPlayerWins");
      }
    }

    public string IsBoardEnabled {
      get { return isBoardEnabled; }
      private set {
        isBoardEnabled = value;
        FirePropertyChanged("IsBoardEnabled");
      }
    }

    void DeclareWinner(Disc disc) {
      RedPlayerWins = disc == Disc.Red ? "Visible" : "Hidden";
      BlackPlayerWins = disc == Disc.Black ? "Visible" : "Hidden";
      RedPlayersTurn = "Hidden";
      BlackPlayersTurn = "Hidden";
      IsBoardEnabled = "False";
    }

    void SwitchTurn(Disc disc) {
      BlackPlayersTurn = disc == Disc.Red ? "Visible" : "Hidden";
      RedPlayersTurn = disc == Disc.Black ? "Visible" : "Hidden";
      CurrentPlayerDisc = disc == Disc.Red ? Disc.Black : Disc.Red;
    }

    Disc CurrentPlayerDisc { get; set; }

    void PlayDisc(object column) {
      //gameboard columns are not zero based
      var discWasPlaced = gameBoard.PlayDisc(CurrentPlayerDisc, (int)column + 1);
      if (discWasPlaced) {
        var index = gameBoard.LastLocationPlayedAsOrderedIndex();
        boardLocationColors[index] = ConvertDiscToFillColor(CurrentPlayerDisc);
        if (!gameBoard.Winner()) {
          SwitchTurn(CurrentPlayerDisc);
        }
        else {
          DeclareWinner(CurrentPlayerDisc);
        }
      }
    }
  }
}
