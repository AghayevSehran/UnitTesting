namespace ExampleAddition
{
    public interface ILogBook
    {
        public int LogSeverity { get; set; }
        public string LogType { get; set; }
        void Message(string message);
        bool LogToDb(string message);
        bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal);
        string MessageWithReturnString(string message);
        bool LogWithOutputResult(string str, out string outputStr);
        bool LogWithRefObject(ref Customer customer);
    }
}