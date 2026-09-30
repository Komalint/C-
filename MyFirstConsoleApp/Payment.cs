using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstConsoleApp
{
    internal class Payment
    {

        private string account_type;
        private string card_number;
        private decimal account_balnace;
        private string security_token;
        private string payment_type;

        public Payment(string acct , string cardN, decimal accBal, string stoken, string payType)
        {
            this.account_type = acct;
            this.card_number = cardN;
            this.account_balnace = accBal;
            this.security_token = stoken;
            this.payment_type = payType;
        }

        public string AccountType
        {
            get { return account_type; }
            set { account_type = value; }
        }
        public string CardNumbar
        {
            set { card_number = value; }

            get
            {
                return "**********" + card_number.Substring(card_number.Length - 4);
            }
        }
        public string SecurityToken
        {
            set
            {
                security_token = value;
            }
            get
            {
                return security_token;
            }
        }
        public decimal AccountBalance
        {
            set
            {
                account_balnace = value;
            }
            get
            {
                return account_balnace;
            }
        }

        public string PaymentType
        {
            set { payment_type = value; }
            get { return payment_type; }
        }
    }
}
