using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Context;
using TicketSystem.Models.Entities;
using TicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketSystem.Services
{
    internal class DatabaseService
    {
        private readonly DataContext _context;

        public DatabaseService()
        {
            _context = new DataContext();
        }

        #region Save to DB
        public async Task SaveToDatabaseAsync(Ticket ticket)
        {
            var _ticketEntity = new TicketEntity
            {
                Description = ticket.Description,
                TicketDateTime = ticket.TicketDateTime,
                Status = (int)ticket.Status
            };

            var _customerEntity = await _context.Customer.FirstOrDefaultAsync(x =>
            x.CustomerFirstName == ticket.FirstName &&
            x.CustomerLastName == ticket.LastName &&
            x.CustomerEmail == ticket.Email &&
            x.Phone == ticket.PhoneNumber);

            if (_customerEntity != null)
            {
                _ticketEntity.CustomerId = _customerEntity.Id;
            }
            else
            {
                _customerEntity = new CustomerEntity
                {
                    CustomerFirstName = ticket.FirstName,
                    CustomerLastName = ticket.LastName,
                    CustomerEmail = ticket.Email,
                    Phone = ticket.PhoneNumber
                };

                await _context.Customer.AddAsync(_customerEntity);
                await _context.SaveChangesAsync();

                _ticketEntity.CustomerId = _customerEntity.Id;
            }

            _context.Add(_ticketEntity);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Get All
        public async Task<IEnumerable<Ticket>> GetAll()
        {

            var list = new List<Ticket>();

            foreach (var ticketEntity in await _context.Ticket.Include(x => x.Comments).Include(x => x.Customer).ToListAsync())
            {
                list.Add(ticketEntity);
            }

            return list;
        }




        #endregion

        #region Get specific
        public async Task<Ticket> GetAsync(string email)
        {
            var ticketEntity = await _context.Ticket.Include(x => x.Comments).Include(x => x.Customer).FirstOrDefaultAsync(x => x.Customer.CustomerEmail == email);


            if (ticketEntity != null)
                return ticketEntity;

            else
                return null!;
        }
        #endregion

        #region Update
        public async Task UpdateAsync(Ticket entity)
        {
            var ticketEntity = await _context.Ticket.FirstOrDefaultAsync(x => x.Customer.CustomerEmail == entity.Email);
            if (ticketEntity != null)
            {
                if (entity.Status >= TicketStatus.NotStarted && entity.Status <= TicketStatus.Complete)
                {
                    ticketEntity.Status = (int)entity.Status;
                }
                _context.Update(ticketEntity);
                await _context.SaveChangesAsync();

            }

        }
        #endregion

        #region Remove
        public async Task RemoveAsync(string email)
        {
            var ticketEntity = await _context.Ticket.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Customer.CustomerEmail == email);
            if (ticketEntity != null)
            {
                _context.Remove(ticketEntity);
                await _context.SaveChangesAsync();
            }
            //Verkar inte ta bort Customer från table-data men försvinner från konsol.

        }
        #endregion

        #region Add comment
        public async Task AddCommentAsync(int ticketId, string comment)
        {
            var ticketEntity = await _context.Ticket.FindAsync(ticketId);

            if (ticketEntity != null)
            {
                var commentEntity = new CommentEntity
                {
                    CommentText = comment,
                    CommentDateTime = DateTime.Now,
                    Ticket = ticketEntity


                };
                _context.Comment.Add(commentEntity);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
