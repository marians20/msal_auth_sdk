namespace SDK.Services.ClientApplications;

public interface IUrlFactoryBase
{
    string BaseUrl { get; }

    string GetUrlForWithIdParameter(Guid id);
}