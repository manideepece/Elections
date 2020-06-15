using Elections.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elections.Repository
{
    public class VotersRepository: IDataRepository<Voters>
    {
        private ElectionsContext _electionContext { get; set; }

        public VotersRepository(ElectionsContext electionContext)
        {
            _electionContext = electionContext;
        }

        public IEnumerable<Voters> GetAll()
        {
            return _electionContext.Voters.ToList();
        }

        public Voters Get(long id)
        {
            return _electionContext.Voters.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(Voters voter)
        {
            _electionContext.Voters.Add(voter);
            _electionContext.SaveChanges();
        }

        public void Update(Voters voter)
        {
            var actualVoter = _electionContext.Voters.Where(x => x.Id == voter.Id).FirstOrDefault();
            actualVoter.Age = voter.Age;
            _electionContext.SaveChanges();
        }

        public void Delete(Voters voter)
        {
            _electionContext.Voters.Remove(voter);
            _electionContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var voter = _electionContext.Voters.Where(x => x.Id == id).FirstOrDefault();
            _electionContext.Voters.Remove(voter);
            _electionContext.SaveChanges();
        }
    }
}
