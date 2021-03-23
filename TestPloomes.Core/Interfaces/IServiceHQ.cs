using System.Collections.Generic;
using TestPloomes.Views.BusinessCrud;
using TestPloomes.Views.BusinessList;

namespace TestPloomes.Core.Interfaces
{
  public interface IServiceHQ
  {
    public string New(ViewCrudHQ view);
    public string Update(ViewCrudHQ view);
    public ViewCrudHQ Get(int id);
    public List<ViewListHQ> List(int pageSize, int page, string filter);
    public string Delete(int id);
  }
}
