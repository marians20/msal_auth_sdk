using SDK.Models;

namespace SDK.Services.ClientApplications;

internal sealed class QuestionTemplateCollectionUrlsFactory : UrlFactoryBase, IClientApplicationUrlsFactory
{
    public QuestionTemplateCollectionUrlsFactory(SdkSettings sdkSettings) : base(sdkSettings)
    {
    }

    public override string BaseUrl => $"{SdkSettings.BaseUrl}/QuestionTemplateCollections";
}
