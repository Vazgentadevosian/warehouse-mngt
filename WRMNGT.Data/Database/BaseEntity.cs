using System.ComponentModel.DataAnnotations;

namespace WRMNGT.Infrastructure.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
