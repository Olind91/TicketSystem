using TicketSystem.Models.Entities;

namespace TicketSystem.Models
{
    internal class Ticket
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string Description { get; set; } = null!;


        public DateTime TicketDateTime { get; set; }
        public TicketStatus Status { get; set; }
        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        public Ticket()
        {
            Status = TicketStatus.NotStarted;

        }
    }
}
