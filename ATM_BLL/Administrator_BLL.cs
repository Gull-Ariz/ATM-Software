using ATM_BO;
using ATM_DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ATM_BLL
{
    
    public class Administrator_BLL
    {
        /*method use to verify username and password to login system*/
        public bool loginAccessAdmin(string u_id, string pin)
        {
            bool result = false;
            Administrator_DAL administrator = new Administrator_DAL();
            List<Administrator_BO> list = administrator.getAdministratorList();
            if(list != null)
            {
                foreach (Administrator_BO admin in list)
                {
                    if (admin.userName == u_id && admin.Password == pin && admin.Status == "active")
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
        /*method use to disable the account on 3 time wrong credientials*/
        public void disableAccount(string login)
        {
            Administrator_DAL administrator_DAL = new Administrator_DAL();
            List<Administrator_BO> list = administrator_DAL.getAdministratorList();
            foreach (Administrator_BO admin in list)
            {
                if (admin.userName == login)
                {
                    admin.Status = "disable";
                    break;
                }
            }
            administrator_DAL.saveAdminAccounList(list);
        }
        /*Method get customer object from Presentation layer and
         * call saveAccount method of DAL for save account in file.*/
        public int createAccount(Customer_BO customer)
        {
            Administrator_DAL administrator = new Administrator_DAL();
            bool result = administrator.saveAccount(customer);
            if (result == true)
            {
                return customer.Account_No;
            }
            else
            {
                return -1;//account number is never negative so it is a check to validate whether acoount is created or not
            }
        }

        /*Method give specific account info according to specify accountNO*/
        public Customer_BO giveAccount(int accountNO)
        {
            Customer_BO customer = null;
            Administrator_DAL administrator = new Administrator_DAL();
            List<Customer_BO> list = administrator.getAccountList();
            if(list!=null)
            {
                foreach (Customer_BO cus in list)
                {
                    if (cus.Account_No == accountNO)
                    {
                        customer = cus;
                    }
                }
            }
            return customer;
        }
        /*method Delete Account of a customer specify by the account number*/
        public bool deleteAccount(int accountNO)
        {
            bool result = false;
            Administrator_DAL administrator = new Administrator_DAL();
            List<Customer_BO> list = administrator.getAccountList();
            if(list != null)
            {
                foreach (Customer_BO cus in list)
                {
                    if (cus.Account_No == accountNO)
                    {
                        result = list.Remove(cus);
                        break;
                    }
                }
                administrator.writeFile(list);
            }
            return result;
        }
        /*method update account of a specific customer*/
        public bool update_BLL(Customer_BO customerPrevious, Customer_BO customerNew)
        {
            bool result = false;
            Administrator_DAL administrator = new Administrator_DAL();
            List<Customer_BO> list = administrator.getAccountList();
            foreach(Customer_BO cus in list)
            {
                if(cus.Account_No == customerPrevious.Account_No)
                {
                    if(customerNew.Account_No > 0)
                    {
                        cus.Account_No = customerNew.Account_No;
                    }
                    if(customerNew.userName !="")
                    {
                        cus.userName = customerNew.userName;
                    }
                    if (customerNew.Password != "")
                    {
                        cus.Password = customerNew.Password;
                    }
                    if (customerNew.holderName != "")
                    {
                        cus.holderName = customerNew.holderName;
                    }
                    if (customerNew.Type != "")
                    {
                        cus.Type = customerNew.Type;
                    }
                    if (customerNew.Balance >= 0)
                    {
                        cus.Balance = customerNew.Balance;
                    }
                    if(customerNew.Status != "")
                    {
                        cus.Status = customerNew.Status;
                    }
                    result = true;
                    break;
                }
            }
            administrator.writeFile(list);
            return result;
        }
        /*Method use to search accounts according to inputs of Search Menu*/
        public List<Customer_BO> search_BLL(Customer_BO customer)
        {
            Administrator_DAL administrator = new Administrator_DAL();
            List<Customer_BO> completeList = administrator.getAccountList();
            List<Customer_BO> listMatched = administrator.getAccountList();
            if (customer.Account_No > 0)
            {
                foreach(Customer_BO cus in listMatched.ToList())
                {
                    if(cus.Account_No != customer.Account_No)
                    {
                        listMatched.Remove(cus);
                    }
                }
            }
            if (customer.userName != "")
            {
                foreach (Customer_BO cus in listMatched.ToList())
                {
                    if (cus.userName != customer.userName)
                    {
                        listMatched.Remove(cus);
                    }
                }
            }
            if (customer.Password != "")
            {
                foreach (Customer_BO cus in listMatched.ToList())
                {
                    if (cus.Password != customer.Password)
                    {
                        listMatched.Remove(cus);
                    }
                }
            }
            if (customer.holderName != "")
            {
                foreach (Customer_BO cus in listMatched.ToList())
                {
                    if (cus.holderName != customer.holderName)
                    {
                        listMatched.Remove(cus);
                    }
                }
            }
            if (customer.Balance > 0)
            {
                foreach (Customer_BO cus in listMatched.ToList())
                {
                    if (cus.Balance != customer.Balance)
                    {
                        listMatched.Remove(cus);
                    }
                }
            }
            if (customer.Status != "")
            {
                foreach (Customer_BO cus in listMatched.ToList())
                {
                    if (cus.Status != customer.Status)
                    {
                        listMatched.Remove(cus);
                    }
                }
            }
            if (customer.Type != "")
            {
                foreach (Customer_BO cus in listMatched.ToList())
                {
                    if (cus.Type != customer.Type)
                    {
                        listMatched.Remove(cus);
                    }
                }
            }
            if(customer.Account_No < 0 && customer.userName == "" && customer.Password == "" && customer.holderName == ""
                && customer.Balance < 0 && customer.Status == "" && customer.Type == "")
            {
                listMatched.Clear();
            }
            return listMatched;
        }
        /*Method return the list of customer accounts according to mini and max amount range*/
        public List<Customer_BO> byAmounts(int min,int max)
        {
            Administrator_DAL administrator_DAL = new Administrator_DAL();
            List<Customer_BO> list = administrator_DAL.getAccountList();
            List<Customer_BO> matchedList = new List<Customer_BO>();
            if(list != null)
            {
                foreach (Customer_BO cus in list)
                {
                    if (cus.Balance >= min && cus.Balance <= max)
                    {
                        matchedList.Add(cus);
                    }
                }
            }
            if(list == null)
            {
                matchedList = null;
            }
            return matchedList;
        }
        /*method return the list of customer accounts according to starting and ending date of transactions*/
        public List<Transaction_BO> byDate(DateTime s_date, DateTime e_date)
        {
            s_date = s_date.Date;
            e_date = e_date.Date;
            Administrator_DAL administrator_DAL = new Administrator_DAL();
            List<Transaction_BO> list = administrator_DAL.getTransactionList();
            List<Transaction_BO> listmatched = new List<Transaction_BO>();
            if(list != null)
            {
                foreach (Transaction_BO obj in list)
                {
                    var date1 = obj.date.Date;
                    if (date1 >= s_date &&  date1 <= e_date)
                    {
                        listmatched.Add(obj);
                    }
                }
            }
            if (list == null)
            {
                listmatched = null;
            }
            return listmatched;
        }
    }
}
