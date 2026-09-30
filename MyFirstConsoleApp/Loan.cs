using System;

namespace MyFirstConsoleApp
{
    public abstract class Loan
    {
        public void VerifyDocuments()
        {
            Console.WriteLine("Documents Verified");
        }

        public void SanctionAmount()
        {
            Console.WriteLine("Loan Amount Sanctioned");
        }

        public abstract double CalculateInterestRate();

        public abstract bool CheckEligibility();
    }

    public class HomeLoan : Loan
    {
        public override double CalculateInterestRate()
        {
            return 8.5;
        }

        public override bool CheckEligibility()
        {
            return true;
        }
    }

    public class CarLoan : Loan
    {
        public override double CalculateInterestRate()
        {
            return 9.0;
        }

        public override bool CheckEligibility()
        {
            return true;
        }
    }

    public class EducationLoan : Loan
    {
        public override double CalculateInterestRate()
        {
            return 7.5;
        }

        public override bool CheckEligibility()
        {
            return true;
        }
    }
}