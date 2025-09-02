using Backend.DTOs.Vocabulary;
using Backend.Repositories;
using Backend.Repositories.Vocabularies;

public class VocabularyListService
{
    private readonly IVocabularyListRepository _listRepo;
    private readonly IUserRepository _userRepo;

    public VocabularyListService(IVocabularyListRepository listRepo, IUserRepository userRepo)
    {
        _listRepo = listRepo;
        _userRepo = userRepo;
    }

    // public async Task<VocabularyListDto> CreateListAsync(Guid userId, string title, string language)
    // {
    //     var user = await _userRepo.GetByIdAsync(userId);
    //     if (user == null)
    //         throw new KeyNotFoundException("User not found");

    //     var list = new VocabularyList
    //     {
    //         Id = Guid.NewGuid(),
    //         Title = title,
    //         Language = language,
    //         UserId = userId
    //     };

    //     await _listRepo.AddAsync(list);

    //     return new VocabularyListDto
    //     {
    //         Id = list.Id,
    //         Language = list.Language,
    //         Vocabularies = []
    //     };
    // }

    public async Task<IEnumerable<VocabularyListDto>> GetListsByUserAsync(Guid userId)
    {
        var lists = await _listRepo.GetListsByUser(userId);

        return lists.Select(list => new VocabularyListDto
        {
            Id = list.Id,
            Language = list.Language,
            Vocabularies = [.. list.Vocabularies.Select(v => new VocabularyDto
            {
                Id = v.Id,
                Word = v.Word,
                Translation = v.Translation
            })]
        });
    }
    public async Task<IEnumerable<VocabularyListDto>> GetAllLists()
    {
        var lists = await _listRepo.GetAllLists();

        return lists.Select(list => new VocabularyListDto
        {
            Id = list.Id,
            Language = list.Language,
            Vocabularies = [.. list.Vocabularies.Select(v => new VocabularyDto
            {
                Id = v.Id,
                Word = v.Word,
                Translation = v.Translation
            })]
        });
    }

    // public async Task DeleteListAsync(Guid userId, Guid listId)
    // {
    //     var list = await _listRepo.GetByIdAsync(listId);

    //     if (list == null || list.UserId != userId)
    //         throw new UnauthorizedAccessException("You cannot delete this list.");

    //     await _listRepo.DeleteAsync(list);
    // }
}
