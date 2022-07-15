namespace SDK.Models
{
    public sealed class SdkContext
    {
        public SdkContext(string[] scope)
        {
            Scope = scope;
        }
        public string? Token { get; set; }

        public string[] Scope { get; }
    }
}
