using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestPloomes.Core.Interfaces;
using TestPloomes.Views.BusinessCrud;
using TestPloomes.Views.BusinessList;

namespace TestPlooms.Api.Controllers
{
  /// <summary>
  /// Controller authentication
  /// </summary>
  [ApiController]
  [Produces("application/json")]
  [Route("auth")]
  public class AuthController : Controller
  {
    private readonly IServiceAuthentication service;

    #region Constructor
    /// <summary>
    /// Construtor controller
    /// </summary>
    /// <param name="_service">Service authentication</param>
    /// <param name="contextAccessor">Token infomartion</param>
    public AuthController(IServiceAuthentication _service, IHttpContextAccessor contextAccessor)
    {
      try
      {
        service = _service;
      }
      catch (Exception e)
      {
        throw e;
      }
    }
    #endregion

    #region Auth
    /// <summary>
    /// Authentication User
    /// </summary>
    /// <param name="view">View authentication</param>
    /// <response code="200">Return authentication</response>
    /// <response code="400">Return error code:<br />
    /// AUTHENTICATION01 - Mail invalid.<br />
    /// AUTHENTICATION02 - Password invalid.</response>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(ViewListAuthentication), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Auth([FromBody] ViewAuthentication view)
    {
      try
      {
        return Ok(service.Auth(view));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    #endregion

  }
}
