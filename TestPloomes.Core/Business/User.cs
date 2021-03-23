using TestPloomes.Core.Base;
using TestPloomes.Views.BusinessCrud;
using TestPloomes.Views.BusinessList;

namespace TestPloomes.Core.Business
{
  public class User : BaseEntity
  {
    public string Name { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
    public ViewListUser GetViewList()
    {
      return new ViewListUser()
      {
        Id = Id,
        Name = Name
      };
    }

    public ViewCrudUser GetViewCrud()
    {
      return new ViewCrudUser()
      {
        Id = Id,
        Name = Name,
        Mail = Mail
      };
    }

  }
}
