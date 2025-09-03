using Backend.Models.DTOs.Vocabulary;
using Backend.Models.Wrappers;
using Memora.Models.DTOs.Vocabulary;
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

  [AllowAnonymous]
  [HttpGet("all")]
  public async Task<ActionResult<ApiResponse<IEnumerable<VocabularyListDto>>>> GetAllLists()
  {
    var lists = await _service.GetAllListsAsync();
    return Ok(lists);
  }

  [HttpPut("update")]
  public async Task<ActionResult<ApiResponse<VocabularyListDto>>> UpdateListById([FromBody] UpdateVocabularyListDto dto)
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
      return Unauthorized();

    var userId = Guid.Parse(userIdClaim.Value);

    var list = await _service.UpdateVocabularyListsAsync(userId, dto);
    return Ok(list);
  }
}