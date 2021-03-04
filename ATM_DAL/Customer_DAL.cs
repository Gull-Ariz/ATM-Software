using ATM_BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_DAL
{
    public class Customer_DAL:Base_DAL
    {
        /*method return complete list of accounts*/
        public List<Customer_BO> getAccountList()
        {
            return readFile("Accounts.txt");
        }
        /*method save accounts list back to file*/
        public bool saveAccounList(List<Customer_BO> list)
        {
            return Write(list, "Accounts.txt");
        }
        /*method return transactions list*/
        public List<Transaction_BO> getTransactionList()
        {
            return readTransactionFile("Transaction.txt");
        }
        /*method save transactions list back to file*/
        public bool saveTransaction(Transaction_BO transaction)
        {
            return saveTransactionFile(transaction,"Transaction.txt");
        }
    }
}
