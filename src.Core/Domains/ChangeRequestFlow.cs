
namespace src.Core.Domains
{
    public class ChangeRequestFlow
    {
        public Guid Id { get; set; }
        public int ChangeRequestId { get; set; }
        public string State { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string NextState { get; set; }
        public int NextStateUserInCharge { get; set; }

        public string? Comment { get; set; }
        public bool? Status { get; set; }
        
        // Navigation property for one-to-one relationship
        public virtual ChangeRequest ChangeRequest { get; set; }

        public virtual User User { get; set; }
        public virtual ChangeRequestState ChangeRequestState { get; set; }
        // Navigation property for one-to-many relationship
        public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();
        public virtual ICollection<ChangeRequestUserUpdate> ChangeRequestUserUpdate { get; set; } = new List<ChangeRequestUserUpdate>();

    }
        
}

