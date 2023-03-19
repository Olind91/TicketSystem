using TicketSystem.Services;

while (true)
{
    Console.Clear();
    Console.WriteLine("What do you want to do?");
    Console.WriteLine("1. Create new ticket");
    Console.WriteLine("2. Show all tickets");
    Console.WriteLine("3. Show a specific ticket");
    Console.WriteLine("4. Update status on a ticket");
    Console.WriteLine("5. Remove a ticket");
    Console.WriteLine("6. Add a comment");
    Console.WriteLine("x. Exit program");


    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            await TicketService.CreateTicketAsync();
            break;


        case "2":
            Console.Clear();
            await TicketService.ShowAllTicketsAsync();

            break;


        case "3":
            Console.Clear();
            await TicketService.ShowSpecificTicketAsync();
            break;


        case "4":
            Console.Clear();
            await TicketService.UpdateTicketAsync();
            break;


        case "5":
            Console.Clear();
            await TicketService.RemoveTicketAsync();
            break;

        case "6":
            Console.Clear();
            await TicketService.AddCommentToTicketAsync();
            break;

        case "x":
            return;

    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}