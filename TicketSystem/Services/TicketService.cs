using TicketSystem.Models.Entities;
using TicketSystem.Models;

namespace TicketSystem.Services
{
    public class TicketService
    {
        #region Create
        public static async Task CreateTicketAsync()
        {

            var databaseService = new DatabaseService();
            var ticket = new Ticket();

            Console.Write("Firstname: ");
            ticket.FirstName = Console.ReadLine() ?? "";

            Console.Write("Lastname: ");
            ticket.LastName = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            ticket.Email = Console.ReadLine() ?? "";

            Console.Write("Phonenumber: ");
            ticket.PhoneNumber = Console.ReadLine() ?? "";

            Console.Write("Error Description: ");
            ticket.Description = Console.ReadLine() ?? "";


            ticket.TicketDateTime = DateTime.Now;


            Console.Clear();
            Console.WriteLine("Ticket has been created!");

            //Save to DB
            await databaseService.SaveToDatabaseAsync(ticket);

        }

        #endregion

        #region Show All
        public static async Task ShowAllTicketsAsync()
        {
            var databaseService = new DatabaseService();
            var tickets = await databaseService.GetAll();

            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine($"Customer ID: {ticket.Id}");
                Console.WriteLine($"Name: {ticket.FirstName} {ticket.LastName}");
                Console.WriteLine($"Email: {ticket.Email}");
                Console.WriteLine($"Phonenumber: {ticket.PhoneNumber}");
                Console.WriteLine($"Error Description: {ticket.Description}");
                Console.WriteLine($"Date and time registered: {ticket.TicketDateTime}");
                Console.WriteLine($"Status: {ticket.Status} ");
                if (ticket.Comments.Any())
                {
                    Console.WriteLine("Comments:"); foreach (CommentEntity comment in ticket.Comments)
                    {
                        Console.WriteLine($"\t{comment.CommentText} \n \t{comment.CommentDateTime} \n");
                    }
                }
            }

        }
        #endregion

        #region Show Specific
        public static async Task ShowSpecificTicketAsync()
        {
            var databaseService = new DatabaseService();
            Console.WriteLine("Input the email of the customerticket you wish to find");
            var email = Console.ReadLine();


            if (!string.IsNullOrEmpty(email))
            {
                var ticket = await databaseService.GetAsync(email);

                if (ticket != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Customer ID: {ticket.Id}");
                    Console.WriteLine($"Name: {ticket.FirstName} {ticket.LastName}");
                    Console.WriteLine($"Email: {ticket.Email}");
                    Console.WriteLine($"Error Description: {ticket.Description}");
                    Console.WriteLine($"Date and time registered: {ticket.TicketDateTime}");
                    Console.WriteLine($"Status: {ticket.Status}");
                    if (ticket.Comments.Any())
                    {
                        Console.WriteLine("Comments:"); foreach (CommentEntity comment in ticket.Comments)
                        {
                            Console.WriteLine($"\t{comment.CommentText} \n \t{comment.CommentDateTime} \n");
                        }
                    }
                    Console.WriteLine("");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No ticket registered with  {email}  as an email");
                }
            }
        }
        #endregion

        #region Update Status
        public static async Task UpdateTicketAsync()
        {

            Console.WriteLine("Input the email of the customer you wish to update");
            var email = Console.ReadLine();



            if (!string.IsNullOrEmpty(email))
            {
                var databaseService = new DatabaseService();
                var ticket = await databaseService.GetAsync(email);
                if (ticket != null)
                {
                    Console.Clear();
                    Console.WriteLine("Ticket found! Please choose a new status");
                    Console.Write("1 = Not Started. 2 = In progress. 3 = Complete: ");

                    TicketStatus status;
                    if (Enum.TryParse(Console.ReadLine(), out status) && Enum.IsDefined(typeof(TicketStatus), status))
                    {
                        ticket.Status = status;
                        await databaseService.UpdateAsync(ticket);
                        Console.Clear();
                        Console.WriteLine($"Status has been updated to {ticket.Status}!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Input...");
                    }

                }

                else
                {
                    Console.Clear();
                    Console.WriteLine($"No ticket registered with  {email}  as an email");
                }
            }


        }

        #endregion

        #region Remove
        public static async Task RemoveTicketAsync()
        {
            var databaseService = new DatabaseService();
            Console.WriteLine("Input the email of the customerticket you wish to remove");
            var email = Console.ReadLine();


            if (!string.IsNullOrEmpty(email))
            {
                var ticket = await databaseService.GetAsync(email);

                if (ticket != null)
                {
                    await databaseService.RemoveAsync(email);
                    Console.Clear();
                    Console.WriteLine($"Ticket registered with the email {email} has been removed!");
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine($"No ticket registered with {email} as an email");
                }
            }
        }
        #endregion


        #region Add Comment
        public static async Task AddCommentToTicketAsync()
        {
            var databaseService = new DatabaseService();
            Console.WriteLine("Input the email of the customerticket you wish to find");
            var email = Console.ReadLine();


            if (!string.IsNullOrEmpty(email))
            {
                var ticket = await databaseService.GetAsync(email);

                if (ticket != null)
                {
                    Console.Clear();
                    Console.WriteLine("Ticket found! Add a comment below.");
                    string comment = Console.ReadLine() ?? "";
                    Console.Clear();
                    Console.WriteLine("Comment added!");

                    await databaseService.AddCommentAsync(ticket.Id, comment);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No ticket registered with {email} as an email");

                }
            }

        }
        #endregion

    }
}
