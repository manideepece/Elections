using Elections.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SpecFlowElections.Specs.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;

namespace SpecFlowElections.Specs.Steps
{
    [Binding]
    public class ElectionsSteps
    {
        private IRestResponse _restResponse;
        private Voters _voter;
        private HttpStatusCode _statusCode;
        private List<Voters> _voters;

        [When(@"I need all voters list")]
        public void WhenINeedAllVotersList()
        {
            var request = new HttpRequestWrapper()
                               .SetMethod(Method.GET)
                               .SetResourse("/elections/getvoters");

            _restResponse = new RestResponse();
            _restResponse = request.Execute();
          
            _statusCode = _restResponse.StatusCode;
            _voters = JsonConvert.DeserializeObject<List<Voters>>(_restResponse.Content);
        }
        
        [Then(@"the result should retrieve everything")]
        public void ThenTheResultShouldRetrieveEverything()
        {
            Assert.That(() => _voters.Count > 0);
        }

        [Given(@"a voter Id \((.*)\)")]
        public void GivenAVoterId(long id)
        {
            var request = new HttpRequestWrapper()
                               .SetMethod(Method.GET)
                               .SetResourse("/elections/GetVoter")
                               .AddParameter("id", id);

            _restResponse = new RestResponse();
            _restResponse = request.Execute();

            _statusCode = _restResponse.StatusCode;
            _voter = JsonConvert.DeserializeObject<Voters>(_restResponse.Content);
        }

        [Then(@"the system should output the Voter")]
        public void ThenTheSystemShouldOutputTheVoter()
        {
            Assert.AreEqual(_voter.FirstName, "Michael");
            Assert.AreEqual(_voter.LastName, "Scott");
            Assert.AreEqual(_voter.Age, 30);
        }

        [Given(@"details of Voter \((.*), (.*), (.*)\)")]
        public void GivenDetailsOfVoter(string firstName, string lastName, int age)
        {
            _voter = new Voters()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age
            };
        }


        [When(@"I add a voter")]
        public void WhenIAddAVoter()
        {
            var request = new HttpRequestWrapper()
                               .SetMethod(Method.POST)
                               .SetResourse("/elections/AddVoter")
                               .AddJsonContent(_voter);

            _restResponse = new RestResponse();
            _restResponse = request.Execute();

            _statusCode = _restResponse.StatusCode;
        }

        [Then(@"the result should retrieve fresh list")]
        public void ThenTheResultShouldRetrieveFreshList()
        {
            var latestVoter = _voters.OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.AreEqual("TestFirstName", latestVoter.FirstName);
            Assert.AreEqual("TestLastName", latestVoter.LastName);
            Assert.AreEqual(32, latestVoter.Age);
        }

        [When(@"I delete a voter")]
        public void WhenIDeleteAVoter()
        {
            var request = new HttpRequestWrapper()
                               .SetMethod(Method.POST)
                               .SetResourse("/elections/DeleteVoter")
                               .AddJsonContent(_voter.Id);

            _restResponse = new RestResponse();
            _restResponse = request.Execute();

            _statusCode = _restResponse.StatusCode;
        }

        [Then(@"the result should retrieve list after deletion")]
        public void ThenTheResultShouldRetrieveListAfterDeletion()
        {
            var latestVoter = _voters.OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.AreEqual(15, latestVoter.Id);
        }



    }
}
