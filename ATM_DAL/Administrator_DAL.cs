using ATM_BO;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ATM_DAL
{
    public class Administrator_DAL: Base_DAL
    {
        /*Save account of the new customer in file*/
        public bool saveAccount(Customer_BO customer)
        {
            bool result = false;
            customer.Account_No = getHighestAccountNo("Accounts.txt");
            if(customer.Account_No > 0)
            {
                result = save(customer, "Accounts.txt");
                return result;
            }
            else
            {
                return result;
            }
        }

        /*method give the list of accounts in file so we can perform operation update, delete etc on list easily*/
        public List<Customer_BO> getAccountList()
        {
            return readFile("Accounts.txt");
        }

        /*Method write list of accounts back to file after processing on list e.g update, delete etc*/
        public bool writeFile(List<Customer_BO> list)
        {
            return Write(list,"Accounts.txt");
        }
        /*Method return complete list of transactions*/
        public List<Transaction_BO> getTransactionList()
        {
            return readTransactionFile("Transaction.txt");
        }
        /*Method return compelete list of regiserted Administrators*/
        public List<Administrator_BO> getAdministratorList()
        {
            return readAdministratorFile("Administrator.txt");
        }
        /*Method use to  save admin accounts list. this method is use when admin account is disable and we have to update this status in file*/
        public bool saveAdminAccounList(List<Administrator_BO> list)
        {
            return saveAdminList(list, "Administrator.txt");
        }
    }
}
