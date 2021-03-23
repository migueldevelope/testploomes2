using TestPloomes.Views.BusinessCrud;
using TestPloomes.Views.BusinessList;

namespace TestPloomes.Core.Interfaces
{
  public interface IServiceAuthentication
  {
    ViewListAuthentication Auth(ViewAuthentication view);
  }
}
