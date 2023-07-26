using MovieTicketBooking.Data.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTicketBooking.Data;
using MongoDB.Driver;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MovieTicketBooking.Business.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDatabaseConnection _settings;
        private readonly IMongoCollection<Tickets> _ticket;
        private readonly IMongoCollection<Theatre> _theatre;
        private readonly IMongoCollection<User> _user;
        private readonly IMongoCollection<Movie> _movie;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public BookingRepository(IDatabaseConnection settings)
        {
            _settings = settings;
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            _ticket = database.GetCollection<Tickets>("Ticket");
            _theatre = database.GetCollection<Theatre>("Theatre");
            _user = database.GetCollection<User>("User");
            _movie = database.GetCollection<Movie>("Movie");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Tickets>> ReteriveTicktes(string userId)
        {
            try
            {
                List<Tickets> tickets = await _ticket.Find(t => t.UserId == userId).Sort("{Created:-1}").ToListAsync();


                foreach (var ticket in tickets)
                {
                    ticket.User = await _user.Find(u => u.Id == userId).FirstOrDefaultAsync();
                    ticket.Theatre = await _theatre.Find(u => u.Id == ticket.TheatreId).FirstOrDefaultAsync();
                    ticket.Movie = await _movie.Find(u => u.Id == ticket.MovieId).FirstOrDefaultAsync();
                }

                return tickets;
            }
            catch (Exception ex)
            {
                return new List<Tickets>();
            }
        }

        public async Task<CreateResponse> TicketBook(Tickets ticket)
        {
            CreateResponse response = new CreateResponse();
            try
            {
                Theatre theatre = await _theatre.Find(t => t.Id == ticket.TheatreId).FirstOrDefaultAsync();
                if (ticket.TotalCount <= theatre.AvailableSeat)
                {
                    await _ticket.InsertOneAsync(ticket);

                    theatre.AvailableSeat -= ticket.TotalCount;
                    theatre.Updated = DateTime.Now;

                    await _theatre.ReplaceOneAsync(t => t.Id == ticket.TheatreId, theatre);

                    response.IsSuccess = true;
                    response.Message = $"{ticket.TotalCount} Ticket Booked";
                    return response;
                }

                response.IsSuccess = false;
                response.Message = $"Only {theatre.AvailableSeat} seats are available";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
