using System.Collections.Generic;

namespace Testing.Models
{
    public class ProductReview
    {
        public Product Product { get; set; }
        public IEnumerable<Review> Review { get; set; }

    }
}
