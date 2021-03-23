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
  /// Controller HQ
  /// </summary>
  [ApiController]
  [Produces("application/json")]
  [Route("hq")]
  public class HQController : Controller
  {
    private readonly IServiceHQ service;

    #region Constructor
    /// <summary>
    /// Construtor controller
    /// </summary>
    /// <param name="_service">Service HQ</param>
    /// <param name="contextAccessor">Token infomartion</param>
    public HQController(IServiceHQ _service, IHttpContextAccessor contextAccessor)
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
    /// New HQ
    /// </summary>
    /// <param name="view">View HQ</param>
    /// <response code="200">Return message: 
    /// HQ04 - New HQ success.</response>
    /// <response code="400">Return error code:<br />
    /// HQ02 - Name invalid.<br />
    /// HQ03 - Company invalid.</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult New([FromBody] ViewCrudHQ view)
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
    /// Update HQ
    /// </summary>
    /// <param name="view">View HQ</param>
    /// <response code="200">Return message: 
    /// HQ06 - Update HQ success.</response>
    /// <response code="400">Return error code:<br />
    /// HQ02 - Name invalid.<br />
    /// HQ03 - Company invalid. <br />
    /// HQ05 - Id not found.</response>
    [Authorize]
    [HttpPut]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] ViewCrudHQ view)
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
    /// Get HQ to id
    /// </summary>
    /// <param name="id">Identifier HQ</param>
    /// <response code="200">Return information.</response>
    /// <response code="400">Return error code.</response>
    [Authorize]
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ViewCrudHQ), StatusCodes.Status200OK)]
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
    /// Delete HQ to id
    /// </summary>
    /// <param name="id">Identifier HQ</param>
    /// <response code="200">Return message:
    /// HQ01 - Deleted HQ success.</response>
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
    /// List HQs
    /// </summary>
    /// <param name="filter">Filter name HQ (optional)</param>
    /// <param name="page">Page to list (optional - Default: 1)</param>
    /// <param name="pageSize">Page size to list (optional - Default: 10)</param>
    /// <response code="200">Return information.</response>
    /// <response code="400">Return error code.</response>
    [Authorize]
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(typeof(List<ViewCrudHQ>), StatusCodes.Status200OK)]
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
