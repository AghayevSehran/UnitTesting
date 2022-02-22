using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleAddition
{

    public class BankAccount
    {
        private readonly ILogBook _logBook;
        public int Balance { get; set; }
        public BankAccount(ILogBook logBook)
        {
            Balance = 0;
            _logBook = logBook;
        }
        public bool Deposit(int amount)
        {
            _logBook.Message("************Deposit invoked**************");
            _logBook.Message("Test");
            _logBook.LogSeverity = 102;
            _logBook.LogSeverity = 102;
            Balance += amount;
            return true;
        }
        public bool Withdraw(int amount)
        {
            if (amount <= Balance)
            {
                _logBook.LogToDb("Withdrawal Amount: " + amount.ToString());
                Balance -= amount;
                return _logBook.LogBalanceAfterWithdrawal(Balance);
            }
            else return _logBook.LogBalanceAfterWithdrawal(Balance - amount);

        }
        public int GetBalance()
        {
            return Balance;
        }
    }
}
