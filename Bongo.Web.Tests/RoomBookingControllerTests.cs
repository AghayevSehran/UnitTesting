using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Bongo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Web.Tests
{
    [NUnit.Framework.TestFixture]
    public class RoomBookingControllerTests
    {
        private Mock<IStudyRoomBookingService> _studyRoomBookingService;
        private RoomBookingController _bookingController;

        [SetUp]
        public void SetUp()
        {
            _studyRoomBookingService = new Mock<IStudyRoomBookingService>();
            _bookingController = new RoomBookingController(_studyRoomBookingService.Object);
        }

        [Test]
        public void IndexPage_CallRequest_VerifyGetAllInvokedOnce()
        {
            _bookingController.Index();

            _studyRoomBookingService.Verify(x => x.GetAllBooking(), Times.Once);
        }

        [Test]
        public void BookRoomCheck_ModelStateInvalid_ReturnView()
        {
            _bookingController.ModelState.AddModelError("test", "test");

            var result = _bookingController.Book(new StudyRoomBooking());

            ViewResult view = result as ViewResult;

            Assert.AreEqual("Book", view.ViewName);
        }

        [Test]
        public void BookRoomCheck_NotSuccessful_NoRoomCode()
        {
            _studyRoomBookingService.Setup
            (x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
            .Returns(new StudyRoomBookingResult()
            {
                Code = StudyRoomBookingCode.NoRoomAvailable
            });


            var result = _bookingController.Book(new StudyRoomBooking());

            ViewResult view = result as ViewResult;

            Assert.IsInstanceOf<ViewResult>(result);

            Assert.AreEqual("No Study Room available for selected date",
                view.ViewData["Error"]);


        }

        [Test]
        public void BookRoomCheck_Successful_SuccessCodeAndRedirect()
        {
            //Arrange
            _studyRoomBookingService.Setup
            (x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
            .Returns((StudyRoomBooking booking) => new StudyRoomBookingResult()
            {
                Code = StudyRoomBookingCode.Success,
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                BookingId = booking.BookingId,
                Date = booking.Date,
                Email = booking.Email
            });

            //Act
            var result = _bookingController.Book(
                new StudyRoomBooking()
                {
                    Date = DateTime.Now,
                    Email = "hello@master.com",
                    FirstName = "Hello",
                    LastName = "Master",
                    StudyRoomId = 1
                });

            //Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            RedirectToActionResult redirectToActionResult = result as RedirectToActionResult;

            Assert.AreEqual("Hello",
                redirectToActionResult.RouteValues["FirstName"]);

            Assert.AreEqual(StudyRoomBookingCode.Success,
              redirectToActionResult.RouteValues["Code"]);

        }

    }
}
