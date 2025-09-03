using Backend.Models.DTOs.Vocabulary;
using Backend.Models.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VocabularyListsController : ControllerBase
{
  private readonly VocabularyListService _service;

  public VocabularyListsController(VocabularyListService service)
  {
    _service = service;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateVocabularyListDto dto)
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
      return Unauthorized();

    var userId = Guid.Parse(userIdClaim.Value);

    var list = await _service.CreateListAsync(userId, dto);
    return Ok(list);
  }

  [HttpGet("user")]
  public async Task<ActionResult<ApiResponse<IEnumerable<VocabularyListDto>>>> GetListsByUser()
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
      return Unauthorized();

    var userId = Guid.Parse(userIdClaim.Value);

    var list = await _service.GetListsByUserAsync(userId);
    return Ok(list);
  }

  [HttpGet("all")]
  public async Task<ActionResult<ApiResponse<IEnumerable<VocabularyListDto>>>> GetAllLists()
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
      return Unauthorized();

    var lists = await _service.GetAllListsAsync();
    return Ok(lists);
  }
}
