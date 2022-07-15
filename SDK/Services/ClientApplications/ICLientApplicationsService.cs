using Microsoft.Rise.FeedbackService.Contracts.Dto;

namespace SDK.Services.ClientApplications
{
    public interface ICLientApplicationsService
    {
        Task<IEnumerable<ClientApplicationDto>?> GetAllAsync();
    }
}