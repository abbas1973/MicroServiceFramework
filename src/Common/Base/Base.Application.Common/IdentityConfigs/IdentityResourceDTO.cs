namespace Application.IdentityConfigs
{
    public class IdentityResourceDTO
    {
        #region Constructors
        public IdentityResourceDTO() { }

        public IdentityResourceDTO(string resource, string description, IEnumerable<string> scopes, IEnumerable<string> claims)
        {
            Resource = resource;
            Description = description;
            Scopes = scopes;
            Claims = claims;
        }
        #endregion


        public string Resource { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Scopes { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}
