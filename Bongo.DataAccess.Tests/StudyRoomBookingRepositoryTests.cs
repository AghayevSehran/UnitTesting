using Bongo.DataAccess.Repository;
using Bongo.Models.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.DataAccess.Tests
{
    [TestFixture]
    public class StudyRoomBookingRepositoryTests
    {
        private StudyRoomBooking studyRoomBooking_One;
        private StudyRoomBooking studyRoomBooking_Two;
        private DbContextOptions<ApplicationDbContext> options;

        public StudyRoomBookingRepositoryTests()
        {
            studyRoomBooking_One = new()
            {
                FirstName = "Ben1",
                LastName = "Spark1",
                Date = new DateTime(2023, 1, 1),
                Email = "ben1@gmail.com",
                BookingId = 11,
                StudyRoomId = 1
            };
            studyRoomBooking_Two = new()
            {
                FirstName = "Ben2",
                LastName = "Spark2",
                Date = new DateTime(2023, 2, 2),
                Email = "ben2@gmail.com",
                BookingId = 22,
                StudyRoomId = 2
            };
        }
        [SetUp]
        public void Setop()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>
                   ().UseInMemoryDatabase(databaseName: "temp_Bongo").Options;
        }
        [Test]
        public void SaveBooking_InputBookingOne_CheckTheValuesFromDatabase()
        {
            //arrange

            //act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new StudyRoomBookingRepository(context);
                repository.Book(studyRoomBooking_One);
            }

            //assert

            using (var context = new ApplicationDbContext(options))
            {
                var bookingFromDb = context.StudyRoomBookings.FirstOrDefault
                    (b => b.BookingId == 11);

                Assert.AreEqual(bookingFromDb.BookingId,
                    studyRoomBooking_One.BookingId);
            }
        }

        [Test]
        public void GetAllBooking_InputBookingOneAndTwo_CheckTheValuesFromDatabase()
        {
            //arrange

            var expectedResult = new List<StudyRoomBooking>()
                {
                studyRoomBooking_One,
                studyRoomBooking_Two
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>
                ().UseInMemoryDatabase(databaseName: "temp_Bongo").Options;

            //act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new StudyRoomBookingRepository(context);
                context.Database.EnsureDeleted();
                repository.Book(studyRoomBooking_One);
                repository.Book(studyRoomBooking_Two);
            }

            //assert

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new StudyRoomBookingRepository(context);
                var result = repository.GetAll(null).ToList();
                CollectionAssert.AreEqual(expectedResult, result, new BookingCompare());
            }
        }
        private class BookingCompare : IComparer
        {
            public int Compare(object? x, object? y)
            {
                var booking1 = (StudyRoomBooking)x;
                var booking2 = (StudyRoomBooking)y;
                if (booking1.BookingId != booking2.BookingId)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
