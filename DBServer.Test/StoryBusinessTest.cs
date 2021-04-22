using DBServer.Project.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DBServer.Project.Data;
using Moq;
using DBServer.Project.Controllers;
using Microsoft.AspNetCore.Mvc;
using DBServer.Project.Models;
using System.Collections.Generic;

namespace DBServer.Test
{
    [TestClass]
    public class StoryBusinessTest
    {
        private Mock<IVotationData> _mockVotationData;
        private StoryBusiness _storyBusiness;

        [TestInitialize]
        public void Setup()
        {
            _mockVotationData = new Mock<IVotationData>();
            _storyBusiness = new StoryBusiness(_mockVotationData.Object);
        }

        [TestMethod]
        public void Test_User_NoVotes_ReturnTrue()
        {
            _mockVotationData.Setup(x => x.GetVotesByDate(DateTime.Today))
                .Returns(new List<VoteModel>());

            bool result = _storyBusiness.CheckUser(1, DateTime.Today);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_User_WithVotes_ReturnFalse()
        {
            var votes = new List<VoteModel>()
            {
                new VoteModel()
                {
                    IdUser = 1,
                    IdRestaurant = 2,
                    DateVote = DateTime.Today
                },
                new VoteModel()
                {
                    IdUser = 2,
                    IdRestaurant = 2,
                    DateVote = DateTime.Today
                }
            };

            _mockVotationData.Setup(x => x.GetVotesByDate(DateTime.Today))
                .Returns(votes);

            bool result = _storyBusiness.CheckUser(1, DateTime.Today);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_User_WithVotesOtherUsers_ReturnTrue()
        {
            var votes = new List<VoteModel>()
            {
                new VoteModel()
                {
                    IdUser = 3,
                    IdRestaurant = 2,
                    DateVote = DateTime.Today
                },
                new VoteModel()
                {
                    IdUser = 2,
                    IdRestaurant = 3,
                    DateVote = DateTime.Today
                }
            };

            _mockVotationData.Setup(x => x.GetVotesByDate(DateTime.Today))
                .Returns(votes);

            bool result = _storyBusiness.CheckUser(1, DateTime.Today);

            Assert.IsTrue(result);
        }


    }
}
