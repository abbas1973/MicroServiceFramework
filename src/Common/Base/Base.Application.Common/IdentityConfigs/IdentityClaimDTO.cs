namespace Application.IdentityConfigs
{
    public class IdentityClaimDTO
    {
        #region Constructors
        public IdentityClaimDTO() { }

        public IdentityClaimDTO(string claim, string description)
        {
            Claim = claim;
            Description = description;
        }

        public IdentityClaimDTO(string claim, string description, List<IdentityClaimDTO> subClaims)
        {
            Claim = claim;
            Description = description;
            SubClaims = subClaims;
        }
        #endregion

        public string Claim { get; set; }
        public string Description { get; set; }
        public IEnumerable<IdentityClaimDTO> SubClaims { get; set; }
    }
}
