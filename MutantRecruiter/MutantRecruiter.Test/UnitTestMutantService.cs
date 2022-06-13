using Microsoft.Extensions.Configuration;
using MutantRecruiter.Services.Services;
using NUnit.Framework;

namespace MutantRecruiter.Test
{
    public class Tests
    {

        [Test]
        public void IsValidDNATest()
        {
            MutantService ms = new MutantService("[^ATCG]");
            string[] validDNA = { "ACGTAC", "ACGTAC", "ACGTAC", "ACGTAC", "ACGTAC", "ACGTAC" };
            if (ms.IsValidDNA(validDNA))
                Assert.Pass();
            else
                Assert.Fail();
        }
        [Test]
        public void IsValidDNAByLetterTest()
        {
            MutantService ms = new MutantService("[^ATCG]");
            string[] validDNA = { "ACGTAX", "ACGTAC", "ACGTAC", "ACGTAC", "ACGTAC", "ACGTAC" };
            if (ms.IsValidDNA(validDNA))
                Assert.Fail();
            else
                Assert.Pass();
        }

        [Test]
        public void IsValidDNAByArrayLenghtTest()
        {
            MutantService ms = new MutantService("[^ATCG]");
            string[] validDNA = { "ACGTAX", "ACGTAC", "ACGTAC", "ACGTAC", "ACGTAC" };
            if (ms.IsValidDNA(validDNA))
                Assert.Fail();
            else
                Assert.Pass();
        }

        [Test]
        public void IsValidDNAByStringLenghtTest()
        {
            MutantService ms = new MutantService("[^ATCG]");
            string[] validDNA = { "ACGTAX", "ACGTAC", "ACGTAC", "ACGTACA", "ACGTAC", "ACGTAC" };
            if (ms.IsValidDNA(validDNA))
                Assert.Fail();
            else
                Assert.Pass();
        }

        [Test]
        public void IsValidDNAByStringIsNulltTest()
        {
            MutantService ms = new MutantService("[^ATCG]");
            string[] validDNA = null;
            if (ms.IsValidDNA(validDNA))
                Assert.Fail();
            else
                Assert.Pass();
        }
    }
}