using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Core.Domains
{
    public class ChangeRequestAttachment
    {
        [Key]
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }

        public Guid ChangeRequestId { get; set; }

        // Navigation property for the many-to-one relationship
        public virtual ChangeRequest ChangeRequest { get; set; }

        public ChangeRequestAttachment() { 
            Id = Guid.NewGuid();        
        }
    }
}
