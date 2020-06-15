using System;
using System.Collections.Generic;

namespace Elections.Models
{
    public partial class Vote
    {
        public int Id { get; set; }
        public int VoterId { get; set; }
        public int CandidateId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public virtual CandidateCategory Candidate { get; set; }
        public virtual Category Category { get; set; }
        public virtual Candidates User { get; set; }
    }
}
