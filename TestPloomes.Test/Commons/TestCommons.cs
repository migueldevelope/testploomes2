using System;
using System.Collections.Generic;
using System.Text;
using TestPloomes.Data;

namespace TestPloomes.Test.Commons
{
  public class TestCommons
  {
    public DataContext context;

    public TestCommons()
    {
      context = new DataContext("Server=testploomes.database.windows.net;Database=testploomes;User Id=test;Password=x14r53p5!a;");
    }
  }
}
