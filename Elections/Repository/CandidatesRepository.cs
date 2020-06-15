using Elections.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elections.Repository
{
    public class CandidatesRepository: IDataRepository<Candidates>
    {
        private ElectionsContext _electionContext { get; set; }

        public CandidatesRepository(ElectionsContext electionContext)
        {
            _electionContext = electionContext;
        }

        public IEnumerable<Candidates> GetAll()
        {
            return _electionContext.Candidates.ToList();
        }

        public Candidates Get(long id)
        {
            return _electionContext.Candidates.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(Candidates candidate)
        {
            _electionContext.Candidates.Add(candidate);
            _electionContext.SaveChanges();
        }

        public void Update(Candidates candidate)
        {
            var actualVoter = _electionContext.Candidates.Where(x => x.Id == candidate.Id).FirstOrDefault();
            actualVoter.Age = candidate.Age;
            _electionContext.SaveChanges();
        }

        public void Delete(Candidates candidate)
        {
            _electionContext.Candidates.Remove(candidate);
            _electionContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var voter = _electionContext.Candidates.Where(x => x.Id == id).FirstOrDefault();
            _electionContext.Candidates.Remove(voter);
            _electionContext.SaveChanges();
        }
    }
}
