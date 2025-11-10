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

  [HttpPost("create-vocabularylist")]
  public async Task<IActionResult> Create([FromBody] CreateVocabularyListDto dto)
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
      return Unauthorized();

    var userId = Guid.Parse(userIdClaim.Value);

    var list = await _service.CreateListAsync(userId, dto);
    return Ok(list);
  }

  [HttpGet("get-all-lists-by-user")]
  public async Task<ActionResult<ApiResponse<IEnumerable<VocabularyListDto>>>> GetListsByUser()
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
      return Unauthorized();

    var userId = Guid.Parse(userIdClaim.Value);

    var list = await _service.GetListsByUserAsync(userId);
    return Ok(list);
  }

  [HttpGet("get-list-by-list-id/{listId}")]
  public async Task<ActionResult<ApiResponse<VocabularyListDto>>> GetListById(Guid listId)
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
      return Unauthorized();

    var userId = Guid.Parse(userIdClaim.Value);

    var list = await _service.GetListsByIdAsync(userId, listId);
    return Ok(list);
  }

  [ApiExplorerSettings(IgnoreApi = true)]
  [AllowAnonymous]
  [HttpGet("all")]
  public async Task<ActionResult<ApiResponse<IEnumerable<VocabularyListDto>>>> GetAllLists()
  {
    var lists = await _service.GetAllListsAsync();
    return Ok(lists);
  }

  [HttpPut("update-list-by-id")]
  public async Task<ActionResult<ApiResponse<VocabularyListDto>>> UpdateListById([FromBody] UpdateVocabularyListDto dto)
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
      return Unauthorized();

    var userId = Guid.Parse(userIdClaim.Value);

    var list = await _service.UpdateVocabularyListsAsync(userId, dto);
    return Ok(list);
  }

  [HttpDelete("delete-list-by-id/{listId}")]
  public async Task<ActionResult<ApiResponse<VocabularyListDto>>> DeleteListById(Guid listId)
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
      return Unauthorized();

    var userId = Guid.Parse(userIdClaim.Value);

    var list = await _service.DeleteListByIdAsync(userId, listId);
    return Ok(list);
  }
}