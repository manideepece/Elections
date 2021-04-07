using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elections.Models;
using Elections.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Elections.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElectionsController : ControllerBase
    {

        private readonly IDataRepository<Voters> _votersRepository;
        private readonly IDataRepository<Candidates> _candidatesRepository;
        private readonly IDataRepository<Category> _categoryRepository;
        private readonly IDataRepository<CandidateCategory> _candidateCategoryRepository;
        private readonly IDataRepository<Vote> _voteRepository;

        public ElectionsController(IDataRepository<Voters> votersRepository, IDataRepository<Candidates> candidatesRepository, IDataRepository<Category> categoryRepository, IDataRepository<CandidateCategory> candidateCategoryRepository, IDataRepository<Vote> voteRepository)
        {
            _votersRepository = votersRepository;
            _candidatesRepository = candidatesRepository;
            _categoryRepository = categoryRepository;
            _candidateCategoryRepository = candidateCategoryRepository;
            _voteRepository = voteRepository;
        }

        [HttpGet]
        [Route("GetVoters")]
        public IEnumerable<Voters> GetVoters()
        {
            return _votersRepository.GetAll();
        }

        [HttpGet]
        [Route("GetVoter")]
        public Voters GetVoter(long id)
        {
            var requestJson = JsonConvert.SerializeObject(id);
            Utility.CreateRequestResponseFilesInBlob(requestJson, "Elections_GetVoter_Request_" + DateTime.Now.ToString());
            return _votersRepository.Get(id);
        }

        [HttpPost]
        [Route("AddVoter")]
        public bool AddVoter(Voters voter)
        {
            try
            {
                _votersRepository.Add(voter);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("AddCandidate")]
        public bool AddCandidate(Candidates candidate)
        {
            try
            {
                _candidatesRepository.Add(candidate);
                var voter = new Voters()
                {
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    Age = candidate.Age
                };
                _votersRepository.Add(voter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("AddCategory")]
        public bool AddCategory(Category category)
        {
            try
            {
                _categoryRepository.Add(category);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("AddCandidateToCategory")]
        public bool AddCandidateToCategory(CandidateCategory candidate)
        {
            try
            {
                _candidateCategoryRepository.Add(candidate);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("AddVote")]
        public bool AddVote(Vote vote)
        {
            try
            {
                var allVotes = _voteRepository.GetAll().Where(x => x.VoterId == vote.VoterId).ToList();
                var candidate = _candidateCategoryRepository.Get(vote.CandidateId);
                var allCandidatesOfCategory = _candidateCategoryRepository.GetAll().Where(x => x.CategoryId == candidate.CategoryId).Select(y => y.Id).ToList();
                bool invalidVote = allVotes.Exists(x => allCandidatesOfCategory.Contains(x.CandidateId));
                if (!invalidVote)
                {
                    vote.CategoryId = candidate.CategoryId;
                    vote.UserId = candidate.UserId;
                    _voteRepository.Add(vote);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("DeleteVoter")]
        public bool DeleteVoter([FromBody]long id)
        {
            try
            {
                var votes = _voteRepository.GetAll().Where(x => x.VoterId == id).ToList();
                foreach(var vote in votes)
                {
                    _voteRepository.Delete(vote.Id);
                }
                var votesForCandidate = _voteRepository.GetAll().Where(x => x.UserId == id).ToList();
                _votersRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("DeleteCandidate")]
        public bool DeleteCandidate([FromBody]long id)
        {
            try
            {
                var votesForCandidate = _voteRepository.GetAll().Where(x => x.UserId == id).ToList();
                foreach (var vote in votesForCandidate)
                {
                    _voteRepository.Delete(vote.Id);
                }
                if (_candidateCategoryRepository.GetAll().Where(x => x.UserId == id).Count() > 0)
                {
                    var candidate = _candidateCategoryRepository.GetAll().Where(x => x.UserId == id).FirstOrDefault();
                    _candidateCategoryRepository.Delete(candidate.Id);
                }
                _candidatesRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("ChangeVoterAge")]
        public bool ChangeVoterAge(Voters voter)
        {
            try
            {
                _votersRepository.Update(voter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("GetVotesOfACandidate")]
        public int GetVotesOfACandidate(long id)
        {
            try
            {
                return _voteRepository.GetAll().Where(x => x.UserId == id).Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
