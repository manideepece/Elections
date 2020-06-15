using Elections.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elections.Repository
{
    public class VoteRepository: IDataRepository<Vote>
    {
        private ElectionsContext _electionContext { get; set; }

        public VoteRepository(ElectionsContext electionContext)
        {
            _electionContext = electionContext;
        }

        public IEnumerable<Vote> GetAll()
        {
            return _electionContext.Vote.ToList();
        }

        public Vote Get(long id)
        {
            return _electionContext.Vote.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(Vote vote)
        {
            _electionContext.Vote.Add(vote);
            _electionContext.SaveChanges();
        }

        public void Update(Vote vote)
        {
            _electionContext.Vote.Update(vote);
            _electionContext.SaveChanges();
        }

        public void Delete(Vote vote)
        {
            _electionContext.Vote.Remove(vote);
            _electionContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var vote = _electionContext.Vote.Where(x => x.Id == id).FirstOrDefault();
            _electionContext.Vote.Remove(vote);
            _electionContext.SaveChanges();
        }
    }
}
