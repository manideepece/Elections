using System;
using System.Collections.Generic;

namespace Elections
{
    public partial class Category
    {
        public Category()
        {
            CandidateCategory = new HashSet<CandidateCategory>();
            Vote = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CandidateCategory> CandidateCategory { get; set; }
        public virtual ICollection<Vote> Vote { get; set; }
    }
}
