using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieTicketBooking.Data.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Theatre
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TheatreName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("SeatCount")]
        public int AvailableSeat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Updated { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TheatreDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string TheatreName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
    }
}
