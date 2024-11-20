using System;

namespace ConversionTest
{
    class Program
    {
        #region Public Dictionary
        // Dictionary for letter to number association
        public static Dictionary<char, int> numbers = new Dictionary<char, int>()
        {
            {'0' , 0},
            {'1' , 1},
            {'2' , 2},
            {'3' , 3},
            {'4' , 4},
            {'5' , 5},
            {'6' , 6},
            {'7' , 7},
            {'8' , 8},
            {'9' , 9},
            {'A' , 10},
            {'B' , 11},
            {'C' , 12},
            {'D' , 13},
            {'E' , 14},
            {'F' , 15},
        };
        #endregion

        #region Main
        static void Main(string[] args)
        {
            // User input variables
            string numberToConvert = null;
            string baseFrom = null;
            string baseTo = null;

            // Helping variables
            uint baseFromNum = 0;
            uint baseToNum = 0;
            string convertedNumber = null;
            bool isValid = false;

            // Program title
            Console.WriteLine("CONVERSION PROGRAM\n");

            // Checking cycle for valid inputs
            do
            {
                numberToConvert = UserInput("number you want to convert (Integer and positive)");
                numberToConvert.ToUpper();

                baseFrom = UserInput("base of the given number (From 2 to 16)");
                baseTo = UserInput("base of the converted number (From 2 to 16)");

                isValid = UintBaseChecking(baseFrom, out baseFromNum) &&
                          UintBaseChecking(baseTo, out baseToNum) &&
                          RightBaseFromChecking(numberToConvert, baseFromNum);

            } while (!isValid);

            // Conversion if inputs are valid
            if (isValid)
            {
                convertedNumber = Conversion(numberToConvert, baseFromNum, baseToNum);
                Console.WriteLine($"{numberToConvert} in base {baseFrom} is {convertedNumber} in base {baseTo}");
            }

        }
        #endregion

        #region Public Methods
        // User input request
        public static string UserInput(string request)
        {
            Console.WriteLine($"Insert the {request}: ");
            return Console.ReadLine();
        }

        // Check if the base is compatible with the number
        public static bool RightBaseFromChecking(string inputToCheck, uint baseFrom)
        {
            // Temporary list for clamping the range numbers depending on the base
            List <char> digits = new List <char>();

            bool isRight = false;
            digits.Clear();

            // check loops for every digit
            for (int i = 0; i < baseFrom; i++)
                digits.Add(numbers.Keys.ElementAt(i));
            for (int i = 0; i < inputToCheck.Length; i++) {
                if (digits.Contains(inputToCheck[i])) isRight = true;
                else
                {
                    isRight = false;
                    Console.WriteLine($"{inputToCheck} is not a number in base {baseFrom}! Retry!");
                    break;
                }
            }
            return isRight;
        }
        // Check if the base is valid
        public static bool UintBaseChecking(string input, out uint uintBase)
        {
            bool isValid = uint.TryParse(input, out uintBase);

            if (isValid && uintBase >= 2 && uintBase <= 16) return true;
            else
            {
                Console.WriteLine($"{input} is not a valid base! Retry!");
                return false;
            }
        }
        // Conversion from a base to another
        public static string Conversion(string numberToConvert, uint baseFromNum, uint baseToNum)
        {
            // Useful variables
            string convertedNum = null;
            int digit = 0;
            int numLength = numberToConvert.Length;
            int pow = 0;
            int baseTenNumber = 0;
            int exp = 0;
            int rest = 0;
            int module = 0;

            if (baseFromNum == baseToNum)
                return numberToConvert;
            
            // Conversion in base 10
            for (int i = numLength - 1; i >= 0; i-- )
            {
                digit = numbers.GetValueOrDefault(numberToConvert[i]);
                pow = (int)baseFromNum;

                for (int j = 1; j < exp; j++)
                    pow *= (int)baseFromNum;

                if (exp == 0) 
                    pow = 1;

                digit *= pow;
                baseTenNumber += digit;

                exp++;
            }

            if (baseToNum == 10)
                return baseTenNumber.ToString();

            // Conversion in another base
            rest = baseTenNumber;

            do
            {
                module = rest % (int)baseToNum;
                rest /= (int)baseToNum;

                foreach (KeyValuePair<char, int> number in numbers)
                {
                    if (number.Value == module)
                        convertedNum = number.Key + convertedNum;
                }
            } while (rest != 0);

            return convertedNum;
        }

        #endregion
    }
}
