using System;
using System.Collections.Generic;

namespace CustomerApi.Test.Validator.v1
{
    public class BirthdayTestData
    {
        public static IEnumerable<object[]> InvalidTestData
        {
            get
            {
                yield return new object[]
                {
                    DateTime.Now.AddYears(-150).AddDays(-1)
                };
                yield return new object[]
                {
                    DateTime.Now.AddDays(1)
                };
            }
        }

        public static IEnumerable<object[]> ValidTestData
        {
            get
            {
                yield return new object[]
                {
                    DateTime.Now.AddYears(-150)
                };
                yield return new object[]
                {
                    DateTime.Now
                };
            }
        }
    }
}