using System;
using System.Collections.Generic;

namespace Elections.Models
{
    public partial class Candidates
    {
        public Candidates()
        {
            Vote = new HashSet<Vote>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public virtual CandidateCategory CandidateCategory { get; set; }
        public virtual ICollection<Vote> Vote { get; set; }
    }
}
