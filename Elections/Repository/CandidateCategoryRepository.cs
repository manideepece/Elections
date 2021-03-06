﻿using Elections.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elections.Repository
{
    public class CandidateCategoryRepository: IDataRepository<CandidateCategory>
    {
        private ElectionsContext _electionContext { get; set; }

        public CandidateCategoryRepository(ElectionsContext electionContext)
        {
            _electionContext = electionContext;
        }

        public IEnumerable<CandidateCategory> GetAll()
        {
            return _electionContext.Candidate.ToList();
        }

        public CandidateCategory Get(long id)
        {
            return _electionContext.Candidate.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(CandidateCategory candidate)
        {
            _electionContext.Candidate.Add(candidate);
            _electionContext.SaveChanges();
        }

        public void Update(CandidateCategory candidate)
        {
            _electionContext.Candidate.Update(candidate);
            _electionContext.SaveChanges();
        }

        public void Delete(CandidateCategory candidate)
        {
            _electionContext.Candidate.Remove(candidate);
            _electionContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var candidate = _electionContext.Candidate.Where(x => x.Id == id).FirstOrDefault();
            _electionContext.Candidate.Remove(candidate);
            _electionContext.SaveChanges();
        }
    }
}
