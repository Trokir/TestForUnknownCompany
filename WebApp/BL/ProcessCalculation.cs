using WebApp.Models;

namespace WebApp.BL
{
    public interface IProcessCalculation
    {
        string GetResult(int value);
    }

    public class ProcessCalculation : IProcessCalculation
    {
        private readonly Calculator _calculator;
        public ProcessCalculation()
        {
            _calculator = new Calculator();
        }

        public string GetResult(int number)
        {
            _calculator.Factor = number;
            if (number < 0 || number ==0 || number>=11) return _calculator.Result = $"Result is {number}";
            if (number >= 1 && number <= 10) return _calculator.Result = $"Factorial is {GetFactorial(number)}";
            else return _calculator.Result = "Result is Error";
        }

        private static int GetFactorial(int value)
        {
            if (value == 0)
                return 1;
            return value * GetFactorial(value- 1);
        }

    }
}