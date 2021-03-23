using System.Collections.Generic;
using TestPloomes.Views.BusinessCrud;
using TestPloomes.Views.BusinessList;

namespace TestPloomes.Core.Interfaces
{
  public interface IServiceUser
  {
    public string New(ViewCrudUser view);
    public string Update(ViewCrudUser view);
    public ViewCrudUser Get(int id);
    public List<ViewListUser> List(int pageSize, int page, string filter);
    public string Delete(int id);
  }
}
