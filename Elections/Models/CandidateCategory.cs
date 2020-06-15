using System;
using System.Collections.Generic;

namespace Elections.Models
{
    public partial class CandidateCategory
    {
        public CandidateCategory()
        {
            Vote = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Candidates User { get; set; }
        public virtual ICollection<Vote> Vote { get; set; }
    }
}
