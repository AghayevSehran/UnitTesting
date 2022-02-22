using ExampleAddition;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleAddition
{
    [TestFixture]
    public class BankAccountNUnitTest
    {
        private BankAccount bankAccount;
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void Deposit_InputAdd100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(""));

            bankAccount = new(logMock.Object);

            var result = bankAccount.Deposit(100);
            Assert.IsTrue(result);
            Assert.That(bankAccount.GetBalance, Is.EqualTo(100));

        }

        [TestCase(200, 100)]
        public void Withdraw_InputWithdraw100With200Balance_ReturnsTrue(int balance, int
            withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(It.IsAny<string>()));

            logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true);

            logMock.Setup(x => x.LogBalanceAfterWithdrawal
            (It.Is<int>(x => x > 0))).Returns(true);

            bankAccount = new(logMock.Object);

            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);

            Assert.IsTrue(result);
            Assert.That(bankAccount.GetBalance, Is.EqualTo(balance - withdraw));
        }

        [Test]
        public void BankWithdraw__InputWithdraw300With200Balance_ReturnsFalse()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(It.IsAny<string>()));

            logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true);

            logMock.Setup(x => x.LogBalanceAfterWithdrawal
            (It.Is<int>(x => x < 0))).Returns(false);

            logMock.Setup(x => x.LogBalanceAfterWithdrawal
       (It.Is<int>(x => x > 0))).Returns(true);

            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, 0, Moq.Range.Inclusive))).Returns(false);

            bankAccount = new(logMock.Object);

            bankAccount.Deposit(200);
            var result = bankAccount.Withdraw(300);

            Assert.IsFalse(result);

        }

        //MOQ evalute a return value

        [Test]
        public void BankLogDummy__InputMockString_Returnstrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello";
            logMock.Setup(x => x.MessageWithReturnString(It.IsAny<string>())).
                Returns((string s) => s.ToLower());


            Assert.That(logMock.Object.MessageWithReturnString("Hello"), Is.EqualTo(desiredOutput));

        }

        [Test]
        public void BankLogDummy__InputMockStringOutputString_Returnstrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "Hello";
            logMock.Setup(x => x.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).
                Returns(true);

            string result = "dfdfdfdfdfdf";
            Assert.IsTrue(logMock.Object.LogWithOutputResult("Ben", out result));
            Assert.That(result, Is.EqualTo(desiredOutput));

        }

        [Test]
        public void BankLogDummy__InputMockStringRefObjecting_Returnstrue()
        {
            var logMock = new Mock<ILogBook>();
            var custmoer = new Customer();
            logMock.Setup(x => x.LogWithRefObject(ref custmoer)).
                Returns(true);

            var custmoer2 = new Customer();
            Assert.IsTrue(logMock.Object.LogWithRefObject(ref custmoer));
            //Assert.That(custmoer2, Is.EqualTo(custmoer));

        }
        //Properties
        [Test]
        public void BankLogDummy__InputMockLogTypeAndLogSeveirity_Returnstrue()
        {
            var logMock = new Mock<ILogBook>();

            logMock.SetupAllProperties();

            logMock.Setup(x => x.LogSeverity).
                Returns(10);
            logMock.Setup(x => x.LogType).
            Returns("Error");

            logMock.Object.LogSeverity = 100;

            Assert.That(logMock.Object.LogSeverity, Is.EqualTo(100));
            Assert.That(logMock.Object.LogType, Is.EqualTo("Error"));

            //Callbacks
            string lognTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true)
                .Callback((string str) => lognTemp += str);
            logMock.Object.LogToDb("Ben");

            Assert.That(lognTemp, Is.EqualTo("Hello, Ben"));


            int counter = 5;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true)
                .Callback(() => counter++);
            logMock.Object.LogToDb("Ben");

            Assert.That(counter, Is.EqualTo(6));
        }

        [Test]
        public void BankLogDummy_VerifyExample()
        {
            var logMock = new Mock<ILogBook>();
            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(100);

            Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));


            //verifcation

            logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(u => u.Message("Test"), Times.Exactly(1));
            logMock.VerifySet(u => u.LogSeverity = 102, Times.Exactly(2));

        }
    }
}
