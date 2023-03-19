using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace TicketSystem.Models.Entities
{
    [Index(nameof(CustomerEmail), IsUnique = true)]
    internal class CustomerEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(50)]
        public string CustomerFirstName { get; set; } = null!;
        public string CustomerLastName { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string CustomerEmail { get; set; } = null!;

        [Column(TypeName = "char(13)")]
        public string? Phone { get; set; }

        public ICollection<TicketEntity> Tickets = new HashSet<TicketEntity>();

    }
}
