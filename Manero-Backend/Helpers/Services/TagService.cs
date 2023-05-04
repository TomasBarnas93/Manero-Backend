using Manero_Backend.Contexts;
using Manero_Backend.Helpers.Enums;
using Manero_Backend.Helpers.Factory;
using Manero_Backend.Models.Dtos.Tag;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;

namespace Manero_Backend.Helpers.Services;

public class TagService : BaseService<TagRequest, TagResponse, TagEntity>, ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ManeroDbContext dbContext, ITagRepository baseRepository) : base(dbContext, baseRepository)
    {
        _tagRepository = baseRepository;
    }
    
    
    public async Task<TagEntity> GetOrCreateAsync(TagRequest entityTag)
    {
        var tag = await _tagRepository.SearchSingleAsync(x=>x.Name.ToLower() == entityTag.Name.ToLower());
        
        if (tag is not null)
            return tag;
        
        
        //Only does this if the tag is not null
        var response = await CheckIfValidCategoryAsync(entityTag);
        
        return _tagRepository.SearchSingleAsync(x=>x.Name == response.Name).Result!;
    }

    private async Task<TagResponse> CheckIfValidCategoryAsync(TagRequest entityTag)
    {
        foreach (var tag in Enum.GetNames(typeof(TagEnum)))
        {
            if (tag.ToLower() == entityTag.Name.ToLower())
                return await _tagRepository.CreateAsync(TagFactory.CreateEntity(tag.ToUpper()));
        }
        
        return await _tagRepository.SearchSingleAsync(x=>x.Name == TagEnum.NONE.ToString()) ??
               await _tagRepository.CreateAsync(TagFactory.CreateEntity(TagEnum.NONE.ToString()));
        
    }


}