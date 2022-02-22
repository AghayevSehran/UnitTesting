using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Bongo.Core.Tests
{
    [TestFixture]
    public class StudyRoomBookingServiceTest
    {
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepositoryMock = null;
        private Mock<IStudyRoomRepository> _studyRoomRepositoryMock = null;
        private StudyRoomBookingService _studyRoomBookingService = null;

        private StudyRoomBooking _request;

        private List<StudyRoom> _availableStudyRoom;



        [SetUp]
        public void Setup()
        {
            _request = new StudyRoomBooking
            {
                FirstName = "Ben",
                LastName = "Spark",
                Email = "ben@gmailcom",
                Date = new DateTime(2022, 1, 1)
            };

            _availableStudyRoom = new List<StudyRoom> {
              new StudyRoom{
              Id=10,RoomName="Michigan",RoomNumber = "A202"
              }
            };

            _studyRoomBookingRepositoryMock = new();

            _studyRoomRepositoryMock = new();
            _studyRoomRepositoryMock.Setup(x => x.GetAll()).
                Returns(_availableStudyRoom);

            _studyRoomBookingService = new
                (_studyRoomBookingRepositoryMock.Object,
                _studyRoomRepositoryMock.Object);




        }

        [TestCase]
        public void GetAllBooking_InvokeMethod_CheckIfRepoIsCalled()
        {
            _studyRoomBookingService.GetAllBooking();

            _studyRoomBookingRepositoryMock.Verify(c => c.GetAll(null), Times.Once);



        }


        [TestCase]
        public void BookingException_NullRequest_ThrowsException()
        {
            var exception = Assert.
                Throws<ArgumentNullException>
                (() =>
                {
                    _studyRoomBookingService.BookStudyRoom(null);
                });

            Assert.That(exception.Message, Does.Contain("Value cannot be null"));
        }


        [TestCase]
        public void
        StudyRoomBooking_SaveBookingWithAvailableRoom_ReturnsResultWithAllValues()
        {
            StudyRoomBooking savedStudyRoomBooking = null;

            _studyRoomBookingRepositoryMock
            .Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
            .Callback<StudyRoomBooking>(
                booking =>
                {
                    savedStudyRoomBooking = booking;
                });

            //Act
            _studyRoomBookingService.BookStudyRoom(_request);

            //Assert
            _studyRoomBookingRepositoryMock.Verify
           (x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);

            Assert.NotNull(savedStudyRoomBooking);
            Assert.AreEqual(_request.FirstName, savedStudyRoomBooking.FirstName);

        }

        [TestCase]
        public void StudyRoomBookingResultCheck_InpuntRequest_ValuesMatchInResult()
        {
            StudyRoomBookingResult result = _studyRoomBookingService.BookStudyRoom(_request);

            Assert.NotNull(result);

            Assert.AreEqual(_request.FirstName, result.FirstName);
            Assert.AreEqual(_request.LastName, result.LastName);
            Assert.AreEqual(_request.Date, result.Date);

        }

        [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
        public StudyRoomBookingCode ResultCodeSuccess_RoomAvailablity_ReturnsSuccessResultCode(bool available)
        {
            if (!available)
            {
                _availableStudyRoom.Clear();
            }
            return _studyRoomBookingService.BookStudyRoom(_request).Code;


        }

        [TestCase(0, false)]
        [TestCase(55, true)]
        public void
       StudyRoomBooking_BookRoomWithAvailability_ReturnsBookingId(int expectedBookingId,
            bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableStudyRoom.Clear();
            }



            _studyRoomBookingRepositoryMock
            .Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
            .Callback<StudyRoomBooking>(
                booking =>
                {
                    booking.BookingId = 55;
                });

            //Act
            var result = _studyRoomBookingService.BookStudyRoom(_request);

            Assert.AreEqual(expectedBookingId, result.BookingId);

            //if (!roomAvailability)
            //{
            //    _studyRoomBookingRepositoryMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
            //}

        }
        [Test]
        public void BookNotInvoked_SaveBookingWithoutAvailableRoom_BookMethodNotInvoked()
        {
            _availableStudyRoom.Clear();

            var result = _studyRoomBookingService.BookStudyRoom(_request);

            _studyRoomBookingRepositoryMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);

        }
    }
}
