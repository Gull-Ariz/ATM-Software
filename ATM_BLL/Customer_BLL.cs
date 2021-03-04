using ATM_BO;
using ATM_DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ATM_BLL
{
    public class Customer_BLL
    {
        /*method verify login access by using username and password*/
        public bool loginAccess(string login, string pin_code)
        {
            bool result = false;
            Customer_DAL customer_DAL = new Customer_DAL();
            List<Customer_BO>list = customer_DAL.getAccountList();
            foreach(Customer_BO cus in list)
            {
                if(cus.userName == login && cus.Password == pin_code && cus.Status.ToLower() == "active")
                {
                    result = true;
                    break;
                }    
            }
            return result;
        }
        /*method use to disable account status using login info*/
        public void disableAccount(string login)
        {
            Customer_DAL customer_DAL = new Customer_DAL();
            List<Customer_BO> list = customer_DAL.getAccountList();
            foreach (Customer_BO cus in list)
            {
                if (cus.userName == login)
                {
                    cus.Status = "disable";
                    break;
                }
            }
            customer_DAL.saveAccounList(list);
        }
        /*method return customer object by using login and password information*/
        public Customer_BO createCustomer(string login, string pin_code)
        {
            Customer_DAL customer_DAL = new Customer_DAL();
            List<Customer_BO> list = customer_DAL.getAccountList();
            Customer_BO customer = new Customer_BO();
            foreach (Customer_BO cus in list)
            {
                if (cus.userName == login && cus.Password == pin_code)
                {
                    customer.Account_No = cus.Account_No;
                    customer.userName = cus.userName;
                    customer.Password = cus.Password;
                    customer.holderName = cus.holderName;
                    customer.Type = cus.Type;
                    customer.Balance = cus.Balance;
                    customer.Status = cus.Status;
                    break;
                }
            }
            return customer;
        }
        /*method actullay withdraw money from user account*/
        public (bool, bool, bool) cashWithdraw(Customer_BO customer,int money)
        {
            bool result = false; bool daily_transaction = false; bool low_balance = false;
            int previousTransaction = 0;string transType = "Cash WithDrawal";
            Transaction_BO transaction= new Transaction_BO();
            DateTime dateTime = DateTime.UtcNow.Date;
            Customer_DAL customer_DAL = new Customer_DAL();
            List<Customer_BO> list = customer_DAL.getAccountList();
            List<Transaction_BO> t_list = customer_DAL.getTransactionList();
            foreach (Transaction_BO tr in t_list)
            {
                if(customer.userName == tr.user_id && tr.date == dateTime && tr.type == "Cash WithDrawal")
                {
                    previousTransaction = tr.amount;
                }
            }
            foreach(Customer_BO cus in list)
            {
                if(customer.Account_No == cus.Account_No)
                {
                    int temp = money;
                    int previous = temp + previousTransaction;
                    if(customer.Balance > money && money < Customer_BO.maxWithdraw && previous < Customer_BO
                        .maxWithdraw)
                    {
                        customer.Balance = customer.Balance - money;
                        cus.Balance = cus.Balance - money;
                        result = true;
                        transaction = createTransactionObj(money,transType,customer.userName,customer.holderName,dateTime);
                        customer_DAL.saveTransaction(transaction);
                        break;
                    }
                    if(customer.Balance > money && previous > Customer_BO.maxWithdraw)
                    {
                        daily_transaction = true;
                    }
                    if(customer.Balance < money)
                    {
                        low_balance = true;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
            }
            bool isSaved = customer_DAL.saveAccounList(list);
            if(isSaved != true)
            {
                result = false;
            }
            return (result, daily_transaction, low_balance);
        }
        /*helper method to create transaction object*/
        private Transaction_BO createTransactionObj(int money, string transType, string userName, string holderName, DateTime date)
        {
            Transaction_BO obj = new Transaction_BO();
            obj.amount = money;
            obj.type = transType;
            obj.user_id = userName;
            obj.holderName = holderName;
            obj.date = date;
            return obj;
        }
        /*Method return customer object by using account number*/
        public Customer_BO giveAccount(int accountNO)
        {
            Administrator_DAL administrator = new Administrator_DAL();
            List<Customer_BO> list = administrator.getAccountList();
            Customer_BO customer = null;
            foreach (Customer_BO cus in list)
            {
                if (cus.Account_No == accountNO)
                {
                    customer = cus;
                }
            }
            return customer;
        }
        /*Method transder money from one account and add to other account*/
        public bool Transfer(Customer_BO depositor, Customer_BO customer, int amount)
        {
            bool result = false;
            Transaction_BO transaction = new Transaction_BO();
            Customer_DAL customer_DAL = new Customer_DAL();
            List<Customer_BO> list = customer_DAL.getAccountList();
            foreach(Customer_BO cus in list)
            {
                if(cus.Account_No == customer.Account_No)
                {
                    customer.Balance = customer.Balance + amount;
                    cus.Balance = customer.Balance + amount;
                    depositor.Balance = depositor.Balance - amount;
                    Customer_BO obj = list.FirstOrDefault(obj1 => obj1.Account_No == depositor.Account_No);
                    if(obj != null)
                    {
                        obj.Balance = depositor.Balance - amount;
                    }
                    break;
                }
            }
            bool isSaved = customer_DAL.saveAccounList(list);
            if(isSaved == true)
            {
                result = true;
            }
            string transType = "Cash Transfer";
            DateTime dateTime = DateTime.UtcNow.Date;
            transaction = createTransactionObj(amount, transType, depositor.userName, depositor.holderName, dateTime);
            customer_DAL.saveTransaction(transaction);
            return result;
        }
        /*Method to deposite cash into account
         */
        public bool deposite(Customer_BO customer, int amount)
        {
            bool result = false;
            string transType = "Cash Deposite";
            bool b = addCash(customer, amount, transType);
            if(b == true)
            {
                result = true;
            }
            return result;
        }
        /*Helper method to add balance in account*/
        private bool addCash(Customer_BO customer, int amount,string transType)
        {
            bool result = false;
            Transaction_BO transaction = new Transaction_BO();
            Customer_DAL customer_DAL = new Customer_DAL();
            List<Customer_BO> list = customer_DAL.getAccountList();
            foreach (Customer_BO cus in list)
            {
                if (cus.Account_No == customer.Account_No)
                {
                    customer.Balance = customer.Balance + amount;
                    cus.Balance = cus.Balance + amount;
                    break;
                }
            }
            DateTime dateTime = DateTime.UtcNow;
            dateTime = dateTime.Date;
            transaction = createTransactionObj(amount, transType, customer.userName, customer.holderName, dateTime);
            customer_DAL.saveTransaction(transaction);
            bool isSaved = customer_DAL.saveAccounList(list);
            if(isSaved == true)
            {
                result = true;
            }
            return result;
        }
    }
}
