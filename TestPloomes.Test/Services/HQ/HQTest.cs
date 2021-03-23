using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TestPloomes.Services.Auth;
using TestPloomes.Test.Commons;
using TestPloomes.Views.BusinessCrud;
using Xunit;
using TestPloomes.Views.BusinessList;

namespace TestPloomes.Test.Services.Auth
{
  public class HQTest : TestCommons
  {
    private readonly ServiceHQ service;
    public HQTest()
    {
      service = new ServiceHQ(context);
    }

    [Fact]
    public void CrudHQ()
    {
      try
      {
        ViewCrudHQ view = new ViewCrudHQ();
        view.Date = DateTime.Now;
        view.Edition = 1;

        // name invalid 
        Exception ex = Assert.Throws<Exception>(() => service.New(view));
        Assert.Equal("HQ02", ex.Message);

        view.Name = "test1";
        // company invalid
        ex = Assert.Throws<Exception>(() => service.New(view));
        Assert.Equal("HQ03", ex.Message);

        view.Company = "test1";
        // new hq
        Assert.Equal("HQ04", service.New(view));

        view = new ViewCrudHQ();
        // name invalid
        ex = Assert.Throws<Exception>(() => service.Update(view));
        Assert.Equal("HQ02", ex.Message);

        view.Name = "test1";
        // company invalid
        ex = Assert.Throws<Exception>(() => service.Update(view));
        Assert.Equal("HQ03", ex.Message);

        view.Company = "test1";
        // id invalid
        ex = Assert.Throws<Exception>(() => service.Update(view));
        Assert.Equal("HQ05", ex.Message);

        view.Date = DateTime.Now;
        view.Edition = 1;
        view.Id = service.List(999, 1, "").LastOrDefault().Id;
        // update hq
        Assert.Equal("HQ06", service.Update(view));

        // get id
        view = service.Get(view.Id);
        Assert.True(view != null);

        //list
        List<ViewListHQ> list = service.List(10, 1, "");
        Assert.True(list.Count > 0);

        //delete
        Assert.Equal("HQ01", service.Delete(view.Id));

      }
      catch (Exception e)
      {
        throw e;
      }
    }
  }
}

