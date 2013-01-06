using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ConnectFourWPF;

namespace ConnectFourWPFTests {
  public class LocationTest {
    [Test]
    public void Copy() {
      var location = new Location(1, 2);
      var copy = location.Copy();
      Assert.AreEqual(location, copy);
      Assert.AreNotSame(location, copy);
    }

  }
}