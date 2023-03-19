using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Models.Entities
{
    internal class TicketEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; } = null!;

        public DateTime TicketDateTime { get; set; }


        public int Status { get; set; }

        //FK

        public Guid CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;

        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

        //Implicit

        public static implicit operator Ticket(TicketEntity entity)
        {
            return new Ticket
            {
                Id = entity.Id,
                Description = entity.Description,
                TicketDateTime = entity.TicketDateTime,
                Status = (TicketStatus)entity.Status,
                Comments = entity.Comments,
                FirstName = entity.Customer.CustomerFirstName,
                LastName = entity.Customer.CustomerLastName,
                Email = entity.Customer.CustomerEmail,

            };

        }

        public static implicit operator TicketEntity(Ticket entity)
        {
            return new TicketEntity
            {
                Id = entity.Id,
                Description = entity.Description,
                TicketDateTime = entity.TicketDateTime,
                Status = (int)entity.Status,
                Comments = entity.Comments,

            };
        }
    }
    
}
