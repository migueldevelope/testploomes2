using System;
using System.Collections.Generic;
using System.Text;
using TestPloomes.Core.Business;
using TestPloomes.Core.Interfaces;
using TestPloomes.Data;
using TestPloomes.Views.BusinessCrud;
using System.Linq;
using TestPloomes.Views.BusinessList;
using Tools;

namespace TestPloomes.Services.Auth
{
  public class ServiceHQ : IServiceHQ
  {
    private readonly Repository<HQ> service;

    #region constructor
    public ServiceHQ(DataContext context)
    {
      service = new Repository<HQ>(context);
    }
    #endregion

    #region crud
    public string Delete(int id)
    {
      try
      {
        service.Delete(id);
        return "HQ01";
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public ViewCrudHQ Get(int id)
    {
      try
      {
        return service.GetByID(id).GetViewCrud();
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public List<ViewListHQ> List(int pageSize, int page, string filter)
    {
      try
      {
        return service.Get(p => p.Name.ToUpper().Contains(filter.ToUpper())).Select(p => p.GetViewList())
          .OrderBy(o => o.Name).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public string New(ViewCrudHQ view)
    {
      try
      {
        if (string.IsNullOrEmpty(view.Name))
        {
          throw new Exception("HQ02");
        }
        if (string.IsNullOrEmpty(view.Company))
        {
          throw new Exception("HQ03");
        }
        
        service.Insert( new HQ()
        {
          Name = view.Name,
          Company = view.Company,
          Edition = view.Edition,
          Date = view.Date
        });
        return "HQ04";
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public string Update(ViewCrudHQ view)
    {
      try
      {
        if (string.IsNullOrEmpty(view.Name))
        {
          throw new Exception("HQ02");
        }
        if (string.IsNullOrEmpty(view.Company))
        {
          throw new Exception("HQ03");
        }
        HQ model = service.GetByID(view.Id);
        if (model == null)
        {
          throw new Exception("HQ05");
        }
        service.Update(model);
        return "HQ06";
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    #endregion
  }
}
