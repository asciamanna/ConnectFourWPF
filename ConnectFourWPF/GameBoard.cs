using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectFourWPF {
  public class GameBoard {
    readonly Dictionary<Location, Disc> board;
    Location lastLocationPlayed;

    public GameBoard() {
      board = new Dictionary<Location, Disc>(MaxRow * MaxColumn);
    }

    public static int MaxRow { get { return 6; } }
    public static int MaxColumn { get { return 7; } }

    public static bool Exists(Location location) {
      return location.Row > 0 && location.Row <= MaxRow &&
        location.Column > 0 && location.Column <= MaxColumn;
    }

    public void Initialize() {
      int x, y;
      x = y = 1;
      for (int i = 1; i <= MaxRow * MaxColumn; i++) {
        board.Add(new Location(y, x), Disc.Empty);
        x++;
        if (x > MaxColumn) {
          y++;
          x = 1;
        }
      }
    }

    public bool PlayDisc(Disc disc, int column) {
      if (column > MaxColumn || column < 1) throw new ArgumentException(String.Format("Column must be between 1 and {0}", MaxColumn));
      int row = MaxRow;
      while (row > 0 && board[new Location(row, column)] != Disc.Empty) {
        row--;
      }

      if (row > 0) {
        board[new Location(row, column)] = disc;
        lastLocationPlayed = new Location(row, column);
        return true;
      }
      return false;
    }

    public Location LastLocationPlayed {
      get {
        return lastLocationPlayed;
      }
    }

    public bool IsEmpty {
      get { return board.Values.All(d => d == Disc.Empty); }
    }

    public int LastLocationPlayedAsOrderedIndex() {
      return board.Keys.OrderBy(k => k.Row).ThenBy(k => k.Column).ToList().IndexOf(LastLocationPlayed);
    }

    public bool Winner() {
      if (HorizontalWins(lastLocationPlayed, board[lastLocationPlayed])) return true;
      if (NortWestSouthEastDiagonalWins(lastLocationPlayed, board[lastLocationPlayed])) return true;
      if (VerticalWins(lastLocationPlayed, board[lastLocationPlayed])) return true;
      if (NorthEastSouthWestDiagonalWins(lastLocationPlayed, board[lastLocationPlayed])) return true;
      return false;
    }

    bool NorthEastSouthWestDiagonalWins(Location lastLocationPlayed, Disc disc) {
      var contiguousDiscs = 0;

      var row = lastLocationPlayed.Row - 1; ;
      var column = lastLocationPlayed.Column + 1;

      //check north east.
      while (row >= 1 && column <= MaxColumn) {
        var loc = new Location(row, column);
        if (board[loc] != disc) break;
        if (board[loc] == disc) {
          contiguousDiscs++;
          column++;
          row--;
        }
        else {
          break;
        }
      }

      row = lastLocationPlayed.Row + 1;
      column = lastLocationPlayed.Column - 1;
      //check south west
      while (row <= MaxRow && column >= 1) {
        var loc = new Location(row, column);
        if (board[loc] == disc) {
          contiguousDiscs++;
          column--;
          row++;
        }
        else {
          break;
        }
      }
      return contiguousDiscs >= 3;
    }

    bool NortWestSouthEastDiagonalWins(Location lastLocationPlayed, Disc disc) {
      var contiguousDiscs = 0;
      var row = lastLocationPlayed.Row - 1;
      var column = lastLocationPlayed.Column - 1;

      //check north west.
      while (row >= 1 && column >= 1) {
        var loc = new Location(row, column);
        if (board[loc] == disc) {
          contiguousDiscs++;
          column--;
          row--;
        }
        else {
          break;
        }
      }

      row = lastLocationPlayed.Row + 1;
      column = lastLocationPlayed.Column + 1;
      //check south east
      while (row <= MaxRow && column <= MaxColumn) {
        var loc = new Location(row, column);
        if (board[loc] == disc) {
          contiguousDiscs++;
          column++;
          row++;
        }
        else {
          break;
        }
      }
      return contiguousDiscs >= 3;
    }

    bool HorizontalWins(Location location, Disc disc) {
      var contiguousDiscs = 0;
      var column = location.Column + 1;

      //check right.
      while (column <= MaxColumn) {
        var loc = new Location(location.Row, column);
        if (board[loc] == disc) {
          contiguousDiscs++;
          column++;
        }
        else {
          break;
        }
      }

      //check left.
      column = location.Column - 1;
      while (column >= 1) {
        var loc = new Location(location.Row, column);
        if (board[loc] == disc) {
          contiguousDiscs++;
          column--;
        }
        else {
          break;
        }
      }
      return contiguousDiscs >= 3;
    }

    bool VerticalWins(Location location, Disc disc) {
      var contiguousDiscs = 0;
      var row = location.Row + 1;

      //check down.
      while (row <= MaxRow) {
        var loc = new Location(row, location.Column);
        if (board[loc] == disc) {
          contiguousDiscs++;
          row++;
        }
        else {
          break;
        }
      }
      return contiguousDiscs >= 3;
    }
  }
}