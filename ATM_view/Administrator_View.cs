using ATM_BLL;
using ATM_BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static System.Console;
namespace ATM_view
{
    public class Administrator_View
    {
        public void administratorMenu()
        {
            Console.Clear();
            bool l_break = false;
            while(true)
            {
                Console.Clear();
                int choice = 0;
                WriteLine("1----Create New Account.");
                WriteLine("2----Delete Existing Account.");
                WriteLine("3----Update Account Information.");
                WriteLine("4----Search For Account.");
                WriteLine("5----View Reports.");
                WriteLine("6----Exit.");
                try
                {
                    choice = System.Convert.ToInt32(ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            createAccount();
                            ReadKey();
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            deleteAccount();
                            ReadKey();
                            Console.Clear();
                            break;
                        case 3:
                            Console.Clear();
                            update();
                            ReadKey();
                            Console.Clear();
                            break;
                        case 4:
                            Console.Clear();
                            search();
                            ReadKey();
                            Console.Clear();
                            break;
                        case 5:
                            Console.Clear();
                            viewReports();
                            ReadKey();
                            Console.Clear();
                            break;
                        case 6:
                            Console.Clear();
                            l_break = true;
                            break;
                        default:
                            Console.Clear();
                            break;
                    }
                    if (l_break)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    WriteLine("invalid input");
                }
            }
        }
        
        /*method get input from administrator and create account*/
        public void createAccount()
        {
            Administrator_BLL administrator_BLL = new Administrator_BLL();

            int balance = 0; string type = ""; string status = "";
            string login = inputString("Login");
            string password = "";
            while (true)
            {
                password = inputString("5 Digit Pin Code");
                if(password.Length == 5)
                {
                    break;
                }
                WriteLine("Pin code should be 5 digit");
            }
            
            string name = inputString("Holder Name");
            while (true)
            {
                bool l_b = false;
                try
                {
                    Write("Type (Saving, Current):   ");
                    type = ReadLine();
                    if (type.ToLower() != "saving" && type.ToLower() != "savings" && type.ToLower() != "current" )
                    {
                        throw new Exception();
                    }
                    else
                    {
                        l_b = true;
                    }
                }
                catch (Exception)
                {
                    WriteLine("Please enter correct type saving or current. Press any key to continue");
                    ReadKey();
                    WriteLine("");
                }
                if (l_b == true)
                {
                    break;
                }
            }
            while(true)
            {
                balance = inputInt("Enter Starting Balance > 100:   ");
                if (balance < 100)
                {
                    WriteLine("Balance should b greater than 1000. Enter Again press any key to continue.");
                    ReadKey();
                    WriteLine("");
                }
                if(balance >=100)
                {
                    break;
                }
            }
            while (true)
            {
                bool l_break = false;
                try
                {
                    Write("Status(active or disable):   ");
                    status = ReadLine();
                    if (status.ToLower() != "active" && status.ToLower() != "disable")
                    {
                        throw new Exception();
                    }
                    else
                    {
                        l_break = true;
                    }
                }
                catch (Exception)
                {
                    WriteLine("Please enter valid status e.g(active,disable). press any key to continue.");
                    ReadKey();
                    WriteLine("");
                }
                if (l_break == true)
                {
                    break;
                }
            }
            Customer_BO customer = new Customer_BO { userName = login, Password = password, holderName = name, Balance = balance, Status = status, Type = type};
            int accountNumber = administrator_BLL.createAccount(customer);
            if (accountNumber > 0)
            {
                WriteLine("\nAccount Successfully created  account number assigned is: " + accountNumber);
                WriteLine("Press any key to continue");
            }
            else
            {
                WriteLine("\nProblem in File Account is not created");
                WriteLine("Press any key to continue");
            }
        }
        /*Helper method to get input in Integer format in createAccount, deleteAccount and updateAccount method*/
        private int inputInt(string str)
        {
            bool l_break = false; int result = 0;
            while(true)
            {
                try
                {
                    Write(str);
                    result = System.Convert.ToInt32(ReadLine());
                    l_break = true;
                }
                catch(FormatException)
                {
                    WriteLine("Please enter only integer value. Press any key to continue.");
                    ReadKey();
                    WriteLine("");
                }
                catch(OverflowException)
                {
                    WriteLine("Please enter valid range of input. press any key to continue");
                    ReadKey();
                    WriteLine("");
                }
                catch(Exception)
                {
                    WriteLine("Invalid input. Press any key to continue");
                    ReadKey();
                    WriteLine("");
                }
                if(l_break == true)
                {
                    break;
                }
            }
            return result;
        }
        /*Helper method to get input in string format in createAccount, deleteAccount and updateAccount method*/
        private string inputString(string str)
        {
            string line = "";
            while (true)
            {
                bool l_break = false;
                try
                {
                    Write(str + ":   ");
                    line = ReadLine();
                    if(line.ToLower() == "")
                    {
                        throw new Exception();
                    }
                    l_break = true;
                }
                catch(Exception)
                {
                    WriteLine("input is not correct press any key to continue.");
                    ReadKey();
                    WriteLine("");
                }
                if(l_break == true)
                {
                    break;
                }
            }
            return line;
        }
        /*method get account number from adminstrator and delete this specific account*/
        public void deleteAccount()
        {
            Administrator_BLL administrator = new Administrator_BLL();
            bool l_break = false;
            while(true)
            {
                try
                {
                    Console.Clear();
                    int accountNo;
                    accountNo = inputInt("Enter the account number to which you want to delete:  ");

                    Customer_BO customer = administrator.giveAccount(accountNo);
                    if (customer != null)
                    {
                        WriteLine("\n\nYou wish to delete the account held by Mr " + customer.holderName + "  If this information is correct please re - enter the account number:  ");
                        int input_2nd = 0;
                        try
                        {
                            input_2nd = System.Convert.ToInt32(ReadLine());
                        }
                        catch(Exception)
                        {
                            
                        }
                        if (input_2nd == accountNo)
                        {
                            bool result = administrator.deleteAccount(accountNo);
                            if (result == true)
                            {
                                WriteLine("\n\nAccount Deleted Successfully");
                                l_break = true;
                            }
                            else
                            {
                                WriteLine("Error in deleting account.");
                                l_break = true;
                            }
                        }
                        else
                        {
                            WriteLine("You enter wrong account number.");
                            l_break = true;
                        }
                    }
                    else
                    {
                        WriteLine("Account is not Exist.");
                        l_break = true;
                    }
                    if(l_break == true)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    WriteLine("Input is not correct");
                }
            }
        }

        /*method get account no from administrator and then take input to update account info*/
        public void update()
        {
            bool l_break = false;
            while(true)
            {
                Console.Clear();
                try
                {
                    int accountNO;
                    accountNO = inputInt("Enter the Account Number:  ");
                    Administrator_BLL administrator = new Administrator_BLL();
                    Customer_BO customerPrevious = administrator.giveAccount(accountNO);
                    if (customerPrevious != null)
                    {
                        Console.Clear();
                        WriteLine("\n\nPrevious Details of Account.");
                        showAccount(customerPrevious);// method display info of account

                        WriteLine("\nPlease enter in the fields you wish to update (leave blank otherwise):\n");
                        Customer_BO customerNew = new Customer_BO();
                        customerNew = getInput();
                        while(true)
                        {
                            bool l_b = false;
                            try
                            {
                                Write("Account Number:  ");
                                string input = ReadLine();
                                if (input == "")
                                {
                                    customerNew.Account_No = -100;/*since acoountNO is not -ve we store -ve value to show administrator does not update accountNO*/
                                    l_b = true;
                                }
                                else
                                {
                                    customerNew.Account_No = System.Convert.ToInt32(input);
                                    l_b = true;
                                }
                            }
                            catch(Exception)
                            {
                                WriteLine("please enter valid account number. Press any key to continue");
                                ReadKey();
                            }
                            if (l_b == true)
                            {
                                break;
                            }
                        }
                        
                        bool result = administrator.update_BLL(customerPrevious, customerNew);
                        if (result == true)
                        {
                            WriteLine("Your account has been successfully been updated.");
                            l_break = true;
                        }
                        else
                        {
                            WriteLine("Your account is not updated.");
                            l_break = true;
                        }
                    }
                    else
                    {
                        WriteLine("Account is not Eixsts.");
                        l_break = true;
                    }
                }
                catch (Exception)
                {
                    WriteLine("Input is not correct. Press any key to continue.");
                }
                if(l_break == true)
                {
                    break;
                }
            }
        }

        /*Search method show search menu and show input matching account*/
        public void search()
        {
            Console.Clear();
            WriteLine("Search Menu:\n");
            Customer_BO customer = getInput();
            Write("Account Number:  ");
            string input = ReadLine();
            if (input == "")
            {
                customer.Account_No = -100;/*since acoountNO is not -ve we store -ve value to show administrator does not update accountNO*/
            }
            else
            {
                customer.Account_No = System.Convert.ToInt32(input);
            }
            Administrator_BLL administrator_BLL = new Administrator_BLL();
            List<Customer_BO> list = administrator_BLL.search_BLL(customer);
            if (list != null)
            {
                WriteLine("==== SEARCH RESULTS ======");
                Write(
                    format: "{0,-13} {1,-13} {2,-13}",
                    arg0: "Account ID",
                    arg1: "User ID",
                    arg2: "Holder Name");
                WriteLine(
                    format: "{0,-13} {1,-13} {2,-13}",
                    arg0: "Type",
                    arg1: "Balance",
                    arg2: "Status"
                    );
                foreach (Customer_BO cus in list)
                {
                    Write(
                        format: "{0,-13} {1,-13} {2,-13}",
                        arg0: cus.Account_No,
                        arg1: cus.userName,
                        arg2: cus.holderName);
                    WriteLine("{0,-13} {1,-13} {2,-13}",
                        arg0: cus.Type,
                        arg1: cus.Balance,
                        arg2: cus.Status);
                }
                WriteLine("Press any key to continue");
            }
            else
            {
                WriteLine("Not a single Account exists with these attributes.");
            }
        }
    
        /*helper method to show account details*/
        private void showAccount(Customer_BO customer)
        {
            WriteLine($"AccountNO: {customer.Account_No}\nusername: {customer.userName}\nPassword: {customer.Password}\nHolderName: {customer.holderName}\n" +
                $"Type: {customer.Type}\nBalance: {customer.Balance}\nStatus: {customer.Status}");
        }
        /*helper method to take account attributes input*/
        private Customer_BO getInput()
        {
            Customer_BO customer = new Customer_BO();
            int balance = 0; string type = "";string status = "";
            while (true)
            {
                try
                {
                    Write("LogIn:   ");
                    string login = ReadLine();
                    Write("Pin Code:   ");
                    string password = ReadLine();
                    Write("Holder Name:   ");
                    string name = ReadLine();
                    while(true)
                    {
                        bool l_b = false;
                        try
                        {
                            Write("Type (Saving, Current):   ");
                            type = ReadLine();
                            if (type.ToLower() != "saving" && type.ToLower() != "current" && type.ToLower() != "")
                            {
                                throw new FormatException();
                            }
                            l_b = true;
                        }
                        catch(FormatException)
                        {
                            WriteLine("Please enter correct type saving or current or leave blank. press aany key to continue");
                        }
                        if(l_b == true)
                        {
                            break;
                        }
                    }
                    while(true)
                    {
                        bool l_break = false;
                        try
                        {
                            Write("Balance:   ");
                            string bal = ReadLine();
                            if (bal == "")
                            {
                                balance = -100;/*since balance is not -ve we store -ve value to show administrator does not update balance*/
                                l_break = true;
                            }
                            else
                            {
                                balance = System.Convert.ToInt32(bal);
                                if(balance > 0)
                                {
                                    l_break = true;
                                }
                                else
                                {
                                    WriteLine("Balance can't be negative please enter correct balance or leave it blank.");
                                }
                            }
                        }
                        catch(Exception)
                        {
                            WriteLine("Please enter correct balance. Press any key to continue");
                            ReadKey();
                        }
                        if(l_break == true)
                        {
                            break;
                        }
                    }
                    while(true)
                    {
                        bool l_break = false;
                        try
                        {
                            Write("Status:   ");
                            status = ReadLine();
                            if (status.ToLower() != "active" && status.ToLower() != "disable" && status.ToLower() != "")
                            {
                                throw new Exception();
                            }
                            else
                            {
                                l_break = true;
                            }
                        }
                        catch(Exception)
                        {
                            WriteLine("Please enter valid status e.g(active,disable or blank). press any key to continue.");
                            ReadKey();
                        }
                        if (l_break == true)
                        {
                            break;
                        }
                    }
                    customer.userName = login; customer.Password = password; customer.holderName = name; customer.Type = type; customer.Balance = balance; customer.Status = status;
                }
                catch (Exception)
                {
                    WriteLine("Invalid input press any key to enter again account information.");
                    customer = null;
                }
                if(customer != null)
                {
                    break;
                }
            }
            return customer;
        }
        /*method to view two different reports type*/
        public void viewReports()
        {
            bool l_break = false;
            while(true)
            {
                Console.Clear();
                try
                {
                    int choice;
                    WriteLine("1---Accounts By Amount");
                    WriteLine("2---Accounts By Date");
                    Write("Press 1 or 2 according to your choice.");
                    choice = System.Convert.ToInt32(ReadLine());
                    switch (choice)
                    {
                        case 1:
                            showByAmount();
                            l_break = true;
                            break;
                        case 2:
                            showByDate();
                            l_break = true;
                            break;
                    }
                }
                catch (Exception)
                {
                    WriteLine("Input is not in correct. Press any key to continue.");
                    ReadKey();
                }
                if(l_break == true)
                {
                    break;
                }
            }
        }
        /*method take mini. and max. amounts and show reports by this amount*/
        private void showByAmount()
        {
            bool l_break = false;
            while(true)
            {
                Console.Clear();
                try
                {
                    int mini, max;
                    Write("Enter the minimum amount:   ");
                    mini = System.Convert.ToInt32(ReadLine());
                    Write("Enter the maximum amount:   ");
                    max = System.Convert.ToInt32(ReadLine());
                    Administrator_BLL administrator_BLL = new Administrator_BLL();
                    List<Customer_BO> list = administrator_BLL.byAmounts(mini, max);
                    if (list != null)
                    {
                        WriteLine("==== SEARCH RESULTS ======");
                        Write(
                            format: "{0,-13} {1,-13} {2,-13}",
                            arg0: "Account ID",
                            arg1: "User ID",
                            arg2: "Holder Name");
                        WriteLine(
                            format: "{0,-13} {1,-13} {2,-13}",
                            arg0: "Type",
                            arg1: "Balance",
                            arg2: "Status"
                            );
                        foreach (Customer_BO cus in list)
                        {
                            Write(
                                format: "{0,-13} {1,-13} {2,-13}",
                                arg0: cus.Account_No,
                                arg1: cus.userName,
                                arg2: cus.holderName);
                            WriteLine("{0,-13} {1,-13} {2,-13}",
                                arg0: cus.Type,
                                arg1: cus.Balance,
                                arg2: cus.Status);
                        }
                        l_break = true;
                    }
                    else
                    {
                        WriteLine("Record is not found for this Search Attributes");
                        l_break = true;
                    }
                }
                catch (Exception)
                {
                    WriteLine("Input is not correct. Press any key to continue");
                    ReadKey();
                }
                if(l_break == true)
                {
                    break;
                }
            }
        }
        /*method take starting date and ending date and show reports according to these dates*/
        private void showByDate()
        {
            bool l_break = false;
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.Clear();
                    DateTime s_date, e_date;
                    Administrator_BLL administrator_BLL = new Administrator_BLL();
                    WriteLine("Date Formate dd/MM/yyyy\n");
                    WriteLine("Enter the starting date:  ");
                    s_date = DateTime.ParseExact(ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    WriteLine("Enter the ending date:   ");
                    e_date = DateTime.ParseExact(ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    List<Transaction_BO> list = administrator_BLL.byDate(s_date, e_date);
                    if (list != null)
                    {
                        WriteLine("==== SEARCH RESULTS ======");
                        Write(
                            format: "{0,-16} {1,-16} {2,-16}",
                            arg0: "Transaction Type",
                            arg1: "User ID",
                            arg2: "Holder Name");
                        WriteLine(
                            format: "{0,-16} {1,-16}",
                            arg0: "Amount",
                            arg1: "Date"
                            );
                        foreach (Transaction_BO tr in list)
                        {
                            Write(
                                format: "{0,-16} {1,-16} {2,-16}",
                                arg0: tr.type,
                                arg1: tr.user_id,
                                arg2: tr.holderName);
                            WriteLine("{0,-13} {1,-13}",
                                arg0: tr.amount,
                                arg1: tr.date.ToString("dd/MM/yyyy"));
                        }
                        l_break = true;
                    }
                    else
                    {
                        WriteLine("Record is not found for this Search Attributes");
                        l_break = true;
                    }
                }
                catch (Exception)
                {
                    WriteLine("Invalid input. Press any key to continue");
                    ReadKey();
                }
                if(l_break == true)
                {
                    break;
                }
            }
        }
    }
}
