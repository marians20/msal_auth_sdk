using Microsoft.Rise.FeedbackService.Contracts.Dto;
using Microsoft.Rise.FeedbackService.Core.Interfaces.Services;

namespace SDK.Services.QuestionTemplateCollections;

internal class QuestionTemplateCollectionService : IQuestionTemplateCollectionService
{
    private readonly IApiClient _client;

    public QuestionTemplateCollectionService(IApiClient client)
    {
        _client = client;
    }

    public Task<QuestionTemplateCollectionDto> CreateAsync(QuestionTemplateCollectionDto questionTemplateCollection)
    {
        throw new NotImplementedException();
    }

    public Task<QuestionTemplateCollectionDto> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<QuestionTemplateCollectionDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<QuestionTemplateCollectionDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<QuestionTemplateCollectionDto> UpdateAsync(Guid id, QuestionTemplateCollectionDto questionTemplateCollection)
    {
        throw new NotImplementedException();
    }
}
