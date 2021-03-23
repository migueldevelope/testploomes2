using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TestPloomes.Core.Business;
using TestPloomes.Core.Interfaces;
using TestPloomes.Data;
using TestPloomes.Views.BusinessCrud;
using TestPloomes.Views.BusinessList;
using Tools;

namespace TestPloomes.Services.Auth
{
  public class ServiceAuthentication : IServiceAuthentication
  {
    private readonly Repository<User> service;
    private const string Secret = "db3OIsj+BXE9NZDyjg8f3TcNekrF+2d/1sFnWG4HnV8TZY30r2k0tVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

    #region contructor
    public ServiceAuthentication(DataContext context)
    {
      service = new Repository<User>(context);
    }
    #endregion

    #region auth
    public ViewListAuthentication Auth(ViewAuthentication view)
    {
      try
      {
        User user = service.Get(p => p.Mail == view.Mail).FirstOrDefault();
        if(user == null)
        {
          throw new Exception("AUTHENTICATION01");
        }
        if (string.IsNullOrEmpty(view.Password))
        {
          throw new Exception("AUTHENTICATION02");
        }
        if(EncryptServices.GetMD5Hash(view.Password) != user.Password)
        {
          throw new Exception("AUTHENTICATION02");
        }

        // Geração do Token de Segurança
        Claim[] claims = new[]
        {
          new Claim(ClaimTypes.Name, user.Name),
          new Claim(ClaimTypes.Email, user.Mail),
          new Claim(ClaimTypes.UserData, user.Id.ToString())
        };
        JwtSecurityToken token;
        DateTime expire = DateTime.Now.AddDays(2);
        token = new JwtSecurityToken(
          issuer: "localhost",
          audience: "localhost",
          claims: claims,
          expires: expire,
          signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)), SecurityAlgorithms.HmacSha256)
        );

        return new ViewListAuthentication
        {
          Id = user.Id,
          Name = user.Name,
          Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
      }
      catch (Exception e)
      {
        throw e;
      }
    }
    #endregion
  }
}
