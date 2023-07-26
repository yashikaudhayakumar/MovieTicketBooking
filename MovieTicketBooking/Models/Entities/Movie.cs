using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MovieTicketBooking.Data.Models.Entities
{
    public class Movie
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
        public string MovieName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MoviePoster { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Languages { get; set; }

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
