namespace ExampleAddition
{
    public class Calculator
    {
        public List<int> NumbersList = new();
        public int Add(int a, int b)
        {
            return a + b;
        }
        public double AddDoubles(double a, double b)
        {
            return a + b;
        }
        public bool IsOddNumber(int a)
        {
            return (a % 2 != 0);
        }
        public List<int> GetOddRange(int min, int max)
        {
            NumbersList.Clear();
            for (int i = min; i < max; i++)
            {
                if (i % 2 != 0)
                {
                    NumbersList.Add(i);
                }
            }
            return NumbersList;
        }
    }
}