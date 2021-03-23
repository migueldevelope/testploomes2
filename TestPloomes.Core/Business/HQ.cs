using System;
using TestPloomes.Core.Base;
using TestPloomes.Views.BusinessCrud;
using TestPloomes.Views.BusinessList;

namespace TestPloomes.Core.Business
{
  public class HQ: BaseEntity
  {
    public string Name { get; set; }
    public string Company { get; set; }
    public int Edition { get; set; }
    public DateTime Date { get; set; } 

    public ViewListHQ GetViewList()
    {
      return new ViewListHQ
      {
        Id = Id,
        Name = Name
      };
    }
    public ViewCrudHQ GetViewCrud()
    {
      return new ViewCrudHQ
      {
        Id = Id,
        Name = Name,
        Date = Date,
        Edition = Edition,
        Company = Company
      };
    }
  }
}
