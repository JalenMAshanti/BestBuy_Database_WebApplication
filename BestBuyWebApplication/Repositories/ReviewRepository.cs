using Dapper;
using System.Collections.Generic;
using System.Data;
using Testing.Abstractions;
using Testing.Models;

namespace Testing.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IDbConnection _conn;
        public ReviewRepository(IDbConnection conn) 
        {
            _conn = conn;
        }

        public IEnumerable<Review> GetReviewsById(int id)
        {
            string sql = "SELECT Reviewer, Rating, Comment FROM reviews WHERE ProductID = @Id;";
            var reviews = _conn.Query<Review>(sql, new {Id = id});
            return reviews;
        }
    }
}
