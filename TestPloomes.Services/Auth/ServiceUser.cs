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
  public class ServiceUser : IServiceUser
  {
    private readonly Repository<User> service;

    #region constructor
    public ServiceUser(DataContext context)
    {
      service = new Repository<User>(context);
    }
    #endregion

    #region crud
    public string Delete(int id)
    {
      try
      {
        service.Delete(id);
        return "USER01";
      }catch(Exception e)
      {
        throw e;
      }
    }

    public ViewCrudUser Get(int id)
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

    public List<ViewListUser> List(int pageSize, int page, string filter)
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

    public string New(ViewCrudUser view)
    {
      try
      {
        if (string.IsNullOrEmpty(view.Name))
        {
          throw new Exception("USER02");
        }
        if (string.IsNullOrEmpty(view.Mail))
        {
          throw new Exception("USER03");
        }
        if (string.IsNullOrEmpty(view.Password))
        {
          throw new Exception("USER04");
        }

        service.Insert(new User()
        {
          Name = view.Name,
          Mail = view.Mail,
          Password = Tools.EncryptServices.GetMD5Hash(view.Password)
        });
        return "USER05";
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public string Update(ViewCrudUser view)
    {
      try
      {
        if (string.IsNullOrEmpty(view.Name))
        {
          throw new Exception("USER02");
        }
        if (string.IsNullOrEmpty(view.Mail))
        {
          throw new Exception("USER03");
        }
        if (string.IsNullOrEmpty(view.Password))
        {
          throw new Exception("USER04");
        }
        User model = service.GetByID(view.Id);
        if(model == null)
        {
          throw new Exception("USER06");
        }
        model.Name = view.Name;
        model.Mail = view.Mail;
        string password = EncryptServices.GetMD5Hash(view.Password);
        if (model.Password != password)
        {
          model.Password = password;
        }
        service.Update(model);
        return "USER07";
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    #endregion
  }
}
