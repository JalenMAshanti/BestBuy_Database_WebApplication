using Org.BouncyCastle.Asn1.Mozilla;
using System.Collections;
using System.Collections.Generic;
using Testing.Models;

namespace Testing.Abstractions
{
    public interface IReviewRepository
    {
        public IEnumerable<Review> GetReviewsById(int id);
    }
}
