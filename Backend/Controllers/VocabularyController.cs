using Backend.Models.DTOs.Vocabulary;
using Backend.Models.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class VocabularyController : ControllerBase
{
  private readonly VocabularyService _service;

  public VocabularyController(VocabularyService service)
  {
    _service = service;
  }

  [HttpGet("all")]
  public async Task<ActionResult<ApiResponse<VocabularyWrapper>>> GetLists()
  {
    var list = await _service.GetAllVocabulariesAsync();
    return Ok(list);
  }
}