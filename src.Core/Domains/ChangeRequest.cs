using src.Localization.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Core.Domains
{
    public class ChangeRequest
    {
        public Guid Id { get; set; }
        public string? ApplicationId { get; set; }
        public string? ApplicationOtherName { get; set; }
        public string? ChangeRequestTypeId { get; set; }
        public string? ChangeRequestOtherName { get; set; }
        public string? PriorityId { get; set; }
        public DateTime? PriorityExpectedDate { get; set; }
        [Required(ErrorMessageResourceName = "EnterCurrentFeature", ErrorMessageResourceType = typeof(SharedResource))]
        public string CurrentFeature { get; set; }
        [Required(ErrorMessageResourceName = "EnterFeatureRequiringModification", ErrorMessageResourceType = typeof(SharedResource))]
        public string FeatureRequiringModification { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        [Required(ErrorMessageResourceName = "EnterStudentName", ErrorMessageResourceType = typeof(SharedResource))]
        public string Requester {  get; set; }
        [Required(ErrorMessageResourceName = "EnterClassName", ErrorMessageResourceType = typeof(SharedResource))]
        public string Campus {  get; set; }

        // Navigation property

        public virtual ChangeRequestType ChangeRequestTypes { get; set; }
        public virtual Applications Applications { get; set; }
        public virtual Priority Prioritys { get; set; }
        public virtual User Users { get; set; }
        public virtual ChangeRequestFlow ChangeRequestFlow { get; set; }
        // Navigation property for one-to-many relationship
        public virtual List<ChangeRequestAttachment> ChangeRequestAttachment { get; set; }
        public ChangeRequest()
        {
            Id = Guid.NewGuid();
            ChangeRequestAttachment = new List<ChangeRequestAttachment>();
        }
    }
}
