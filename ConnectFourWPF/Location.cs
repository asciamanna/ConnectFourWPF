using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectFourWPF{
  public class Location {
    public Location(int row, int column) {
      this.Row = row;
      this.Column = column;
    }
    public int Column { get; set; }
    public int Row { get; set; }

    public override string ToString() {
      return "Column: " + Column + " Row: " + Row;
    }

    public override bool Equals(object obj) {
      var that = obj as Location;
      if (that == null) return false;
      return this.Column == that.Column && this.Row == that.Row;
    }

    public override int GetHashCode() {
      return Column.GetHashCode() + Row.GetHashCode();
    }
  }
}