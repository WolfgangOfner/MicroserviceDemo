namespace WolfgangOfner.MicroserviceDemo.PrimeNumber
{
    public static class PrimeNumber
    {
        /// <summary>
        /// Provide a parameter nThPrimeNumber and find the nThPrimeNumber-th prime number.
        /// </summary>
        /// <param name="nThPrimeNumber">The nThPrimeNumber-th prime number to find.</param>
        /// <returns></returns>
        public static long FindNthPrimeNumber(int nThPrimeNumber)
        {
            var count = 0;
            long a = 2;

            while (count < nThPrimeNumber)
            {
                long b = 2;
                var prime = 1; 

                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;

                        break;
                    }

                    b++;
                }

                if (prime > 0)
                {
                    count++;
                }

                a++;
            }

            return --a;
        }
    }
}