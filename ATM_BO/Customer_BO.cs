using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_BO
{
    public class Customer_BO
    {
        public string userName { set; get; }
        public string Password { set; get; }
        public string holderName { set; get; }
        public string Type { set; get; }
        public double Balance { set; get; }
        public string Status { set; get; }

        public int Account_No { set; get; }
        public const int maxWithdraw = 20001;
        public int transcationAmount { set; get; }
    }
}
