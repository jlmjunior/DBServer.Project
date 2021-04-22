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
        private StoryBusiness _sut;

        [TestInitialize]
        public void Setup()
        {
            _mockVotationData = new Mock<IVotationData>();
            _sut = new StoryBusiness(_mockVotationData.Object);
        }

        #region USER_CHECK
        [TestMethod]
        public void UserCheck_WithoutVotes_ReturnTrue()
        {
            _mockVotationData.Setup(x => x.GetVotesByDate(DateTime.Today))
                .Returns(new List<VoteModel>());

            bool result = _sut.CheckUser(1, DateTime.Today);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserCheck_WithVotes_ReturnFalse()
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

            bool result = _sut.CheckUser(1, DateTime.Today);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UserCheck_WithVotesOtherUsers_ReturnTrue()
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

            bool result = _sut.CheckUser(1, DateTime.Today);

            Assert.IsTrue(result);
        }
        #endregion

        #region RESTAURANT_CHECK
        [TestMethod]
        public void RestaurantCheck_WithoutVotes_ReturnTrue()
        {
            var votes = new List<VoteModel>();

            _mockVotationData.Setup(x => x.GetVotes())
                .Returns(votes);
            
            bool result = _sut.CheckRestaurant(1, DateTime.Today);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RestaurantCheck_AlreadySelectedInTheWeek_ReturnFalse()
        {
            var monday = DateTime.Today.AddDays((int)DayOfWeek.Monday - (int)DateTime.Today.DayOfWeek);

            var votes = new List<VoteModel>()
            {
                new VoteModel()
                {
                    IdUser = 1,
                    IdRestaurant = 2,
                    DateVote = monday                    
                },
                new VoteModel()
                {
                    IdUser = 2,
                    IdRestaurant = 2,
                    DateVote = monday
                },
                new VoteModel()
                {
                    IdUser = 3,
                    IdRestaurant = 1,
                    DateVote = monday
                }
            };

            _mockVotationData.Setup(x => x.GetVotes())
                .Returns(votes);

            bool result = _sut.CheckRestaurant(2, monday.AddDays(1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RestaurantCheck_NotSelectedInTheWeek_ReturnTrue()
        {
            var thursday = DateTime.Today.AddDays((int)DayOfWeek.Thursday - (int)DateTime.Today.DayOfWeek);

            var votes = new List<VoteModel>()
            {
                new VoteModel()
                {
                    IdUser = 1,
                    IdRestaurant = 2,
                    DateVote = thursday
                },
                new VoteModel()
                {
                    IdUser = 2,
                    IdRestaurant = 2,
                    DateVote = thursday
                },
                new VoteModel()
                {
                    IdUser = 3,
                    IdRestaurant = 1,
                    DateVote = thursday
                }
            };

            _mockVotationData.Setup(x => x.GetVotes())
                .Returns(votes);

            bool result = _sut.CheckRestaurant(1, thursday.AddDays(2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RestaurantCheck_NotSelectedInTheWeek_MultipleDays_ReturnTrue()
        {
            var thursday = DateTime.Today.AddDays((int)DayOfWeek.Thursday - (int)DateTime.Today.DayOfWeek);

            var votes = new List<VoteModel>()
            {
                new VoteModel()
                {
                    IdUser = 1,
                    IdRestaurant = 2,
                    DateVote = thursday
                },
                new VoteModel()
                {
                    IdUser = 2,
                    IdRestaurant = 2,
                    DateVote = thursday
                },
                new VoteModel()
                {
                    IdUser = 3,
                    IdRestaurant = 1,
                    DateVote = thursday
                },
                new VoteModel()
                {
                    IdUser = 1,
                    IdRestaurant = 4,
                    DateVote = thursday.AddDays(2)
                },
                new VoteModel()
                {
                    IdUser = 2,
                    IdRestaurant = 4,
                    DateVote = thursday.AddDays(2)
                },
                new VoteModel()
                {
                    IdUser = 3,
                    IdRestaurant = 1,
                    DateVote = thursday.AddDays(2)
                }
            };

            _mockVotationData.Setup(x => x.GetVotes())
                .Returns(votes);

            bool result = _sut.CheckRestaurant(1, thursday.AddDays(1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void RestaurantCheck_AlreadySelectedInTheWeek_MultipleSelected_ReturnFalse(int idRestaurant)
        {
            var thursday = DateTime.Today.AddDays((int)DayOfWeek.Thursday - (int)DateTime.Today.DayOfWeek);

            var votes = new List<VoteModel>()
            {
                new VoteModel()
                {
                    IdUser = 1,
                    IdRestaurant = 1,
                    DateVote = thursday
                },
                new VoteModel()
                {
                    IdUser = 2,
                    IdRestaurant = 1,
                    DateVote = thursday
                },
                new VoteModel()
                {
                    IdUser = 3,
                    IdRestaurant = 1,
                    DateVote = thursday
                },
                new VoteModel()
                {
                    IdUser = 1,
                    IdRestaurant = 2,
                    DateVote = thursday.AddDays(1)
                },
                new VoteModel()
                {
                    IdUser = 2,
                    IdRestaurant = 4,
                    DateVote = thursday.AddDays(1)
                },
                new VoteModel()
                {
                    IdUser = 3,
                    IdRestaurant = 2,
                    DateVote = thursday.AddDays(1)
                }
            };

            _mockVotationData.Setup(x => x.GetVotes())
                .Returns(votes);

            bool result = _sut.CheckRestaurant(idRestaurant, thursday.AddDays(2));

            Assert.IsFalse(result);
        }
        #endregion

    }
}
