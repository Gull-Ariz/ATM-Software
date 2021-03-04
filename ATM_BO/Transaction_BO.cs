using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_BO
{
    public class Transaction_BO
    {
        public string type { set; get; }
        public string user_id { set; get; }
        public string holderName { set; get; }
        public int amount { set; get; }
        public DateTime date { set; get; }
    }
}
