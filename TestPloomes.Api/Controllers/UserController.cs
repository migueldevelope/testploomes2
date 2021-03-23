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
  /// Controller user
  /// </summary>
  [ApiController]
  [Produces("application/json")]
  [Route("user")]
  public class UserController : Controller
  {
    private readonly IServiceUser service;

    #region Constructor
    /// <summary>
    /// Construtor controller
    /// </summary>
    /// <param name="_service">Service user</param>
    /// <param name="contextAccessor">Token infomartion</param>
    public UserController(IServiceUser _service, IHttpContextAccessor contextAccessor)
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

    #region Crud
    
    /// <summary>
    /// New User
    /// </summary>
    /// <param name="view">View user</param>
    /// <response code="200">Return message: 
    /// USER05 - New user success.</response>
    /// <response code="400">Return error code:<br />
    /// USER02 - Name invalid.<br />
    /// USER03 - Mail invalid.<br />
    /// USER04 - Password invalid.</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult New([FromBody] ViewCrudUser view)
    {
      try
      {
        return Ok(service.New(view));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="view">View user</param>
    /// <response code="200">Return message: 
    /// USER07 - Update user success.</response>
    /// <response code="400">Return error code:<br />
    /// USER02 - Name invalid.<br />
    /// USER03 - Mail invalid.<br />
    /// USER04 - Password invalid.<br />
    /// USER06 - Id not found.</response>
    [Authorize]
    [HttpPut]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] ViewCrudUser view)
    {
      try
      {
        return Ok(service.Update(view));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    /// <summary>
    /// Get User to id
    /// </summary>
    /// <param name="id">Identifier user</param>
    /// <response code="200">Return information.</response>
    /// <response code="400">Return error code.</response>
    [Authorize]
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ViewCrudUser), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Get(int id)
    {
      try
      {
        return Ok(service.Get(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    /// <summary>
    /// Delete User to id
    /// </summary>
    /// <param name="id">Identifier user</param>
    /// <response code="200">Return message:
    /// USER01 - Deleted user success.</response>
    /// <response code="400">Return error code.</response>
    [Authorize]
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
      try
      {
        return Ok(service.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    /// <summary>
    /// List Users
    /// </summary>
    /// <param name="filter">Filter name user (optional)</param>
    /// <param name="page">Page to list (optional - Default: 1)</param>
    /// <param name="pageSize">Page size to list (optional - Default: 10)</param>
    /// <response code="200">Return information.</response>
    /// <response code="400">Return error code.</response>
    [Authorize]
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(typeof(List<ViewCrudUser>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Get(int pageSize = 10, int page = 1, string filter = "")
    {
      try
      {
        return Ok(service.List(pageSize, page, filter));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    #endregion

  }
}
