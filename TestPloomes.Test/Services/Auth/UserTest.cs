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
  public class UserTest : TestCommons
  {
    private readonly ServiceUser service;
    public UserTest()
    {
      service = new ServiceUser(context);
    }

    [Fact]
    public void CrudUser()
    {
      try
      {
        ViewCrudUser view = new ViewCrudUser();

        // name invalid 
        Exception ex = Assert.Throws<Exception>(() => service.New(view));
        Assert.Equal("USER02", ex.Message);

        view.Name = "test1";
        // mail invalid
        ex = Assert.Throws<Exception>(() => service.New(view));
        Assert.Equal("USER03", ex.Message);

        view.Mail = "test1@ploomes.com.br";
        // pass invalid
        ex = Assert.Throws<Exception>(() => service.New(view));
        Assert.Equal("USER04", ex.Message);
        
        view.Password = "123";
        // new user
        Assert.Equal("USER05", service.New(view));

        view = new ViewCrudUser();
        // name invalid
        ex = Assert.Throws<Exception>(() => service.Update(view));
        Assert.Equal("USER02", ex.Message);

        view.Name = "test1";
        // mail invalid
        ex = Assert.Throws<Exception>(() => service.Update(view));
        Assert.Equal("USER03", ex.Message);

        view.Mail = "test1@ploomes.com.br";
        // pass invalid
        ex = Assert.Throws<Exception>(() => service.Update(view));
        Assert.Equal("USER04", ex.Message);

        view.Password = "123";
        // id invalid
        ex = Assert.Throws<Exception>(() => service.Update(view));
        Assert.Equal("USER06", ex.Message);

        view.Id = service.List(999,1,"").LastOrDefault().Id;
        // update user
        Assert.Equal("USER07", service.Update(view));

        // get id
        view = service.Get(view.Id);
        Assert.True(view != null);

        //list
        List<ViewListUser> list = service.List(10, 1, "");
        Assert.True(list.Count > 0);

        //delete
        Assert.Equal("USER01", service.Delete(view.Id));

      }
      catch (Exception e)
      {
        throw e;
      }
    }
  }
}

