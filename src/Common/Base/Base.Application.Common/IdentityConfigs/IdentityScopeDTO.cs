namespace Application.IdentityConfigs
{
    public class IdentityScopeDTO
    {
        #region Constructors
        public IdentityScopeDTO() { }

        public IdentityScopeDTO(string scope, string description)
        {
            Scope = scope;
            Description = description;
        }

        public IdentityScopeDTO(string scope, string description, IEnumerable<string> claims)
        {
            Scope = scope;
            Description = description;
            Claims = claims;
        }
        #endregion


        public string Scope { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}
