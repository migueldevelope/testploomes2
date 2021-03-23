using System;
using System.Collections.Generic;
using System.Text;
using TestPloomes.Services.Auth;
using TestPloomes.Test.Commons;
using TestPloomes.Views.BusinessCrud;
using TestPloomes.Views.BusinessList;
using Xunit;

namespace TestPloomes.Test.Services.Auth
{
  public class AutheticationTest: TestCommons
  {
    private readonly ServiceAuthentication service;
    public AutheticationTest()
    {
      service = new ServiceAuthentication(context);
    }

    [Fact]
    public void Auth()
    {
      try
      {
        ViewAuthentication view = new ViewAuthentication();

        Exception ex = Assert.Throws<Exception>(() => service.Auth(view));
        // mail invalid 
        Assert.Equal("AUTHENTICATION01", ex.Message);
        view.Mail = "test@ploomes.com.br";

        // pass invalid
        ex = Assert.Throws<Exception>(() => service.Auth(view));
        Assert.Equal("AUTHENTICATION02", ex.Message);

        view.Password = "123";
        // authentication
        ViewListAuthentication auth = service.Auth(view);
        Assert.True(auth != null);

      }
      catch(Exception e)
      {
        throw e;
      }
    }
  }
}
