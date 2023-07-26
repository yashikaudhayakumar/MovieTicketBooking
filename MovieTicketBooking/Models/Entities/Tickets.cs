using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Data.Models.Entities
{
    public class Tickets
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TicketId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("TicketsCount")]
        public int TotalCount { get; set; }

        // <summary>
        /// 
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string TheatreId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonIgnore]
        public Theatre Theatre { get; set; }


        // <summary>
        /// 
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string MovieId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonIgnore]
        public Movie Movie { get; set; }

        // <summary>
        /// 
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonIgnore]
        public User User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Updated { get; set; }
    }
}
