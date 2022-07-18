using Microsoft.Rise.FeedbackService.Contracts.Dto;
using Microsoft.Rise.FeedbackService.Core.Interfaces.Services;
using SDK.DataAdapters;

namespace SDK.Services.QuestionTemplateCollections;

internal class QuestionTemplateCollectionService : ServiceBase, IQuestionTemplateCollectionService
{

    public QuestionTemplateCollectionService(IApiClient client) : base(client)
    {
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
