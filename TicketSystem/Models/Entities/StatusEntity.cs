using System.ComponentModel.DataAnnotations;


namespace TicketSystem.Models.Entities
{
    public enum TicketStatus
    {
        NotStarted = 1,
        InProgress = 2,
        Complete = 3
    }

    public class StatusEntity
    {
        [Key]
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
    }
}
