using ATM_BLL;
using ATM_BO;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace ATM_view
{
    public class Customer_View
    {
        public void customerMenu(Customer_BO customer)
        {
            Console.Clear();
            int choice = 0; bool l_break = false;
            while(true)
            {
                Console.Clear();
                WriteLine("1----Withdraw Cash");
                WriteLine("2----Cash Transfer");
                WriteLine("3----Deposit Cash");
                WriteLine("4----Display Balance");
                WriteLine("5----Exit.\n");
                WriteLine("Please select one of the above options:   ");
                try
                {
                    choice = System.Convert.ToInt32(ReadLine());
                    switch (choice)
                    {
                        case 1:
                            bool m_loop = false;
                            while(true)
                            {
                                Console.Clear();
                                char mode;
                                WriteLine("a) Fast Cash");
                                WriteLine("b) Normal Cash");
                                WriteLine("Please select a mode of withdrawal:");
                                try
                                {
                                    mode = System.Convert.ToChar(ReadLine());
                                    switch (mode)
                                    {
                                        case 'a':
                                            Console.Clear();
                                            fastCash(customer);
                                            Write("Press any key to continue.");
                                            ReadKey();
                                            Console.Clear();
                                            m_loop = true;
                                            break;
                                        case 'b':
                                            Console.Clear();
                                            normalCash(customer);
                                            Write("Press any key to continue.");
                                            ReadKey();
                                            Console.Clear();
                                            m_loop = true;
                                            break;
                                        default:
                                            WriteLine("Please enter a or b Press any key to Continue");
                                            break;
                                    }
                                }
                                catch (Exception)
                                {
                                    WriteLine("Please enter a or b Press any key to Continue");
                                    ReadKey();
                                    WriteLine("");
                                }
                                if(m_loop == true)
                                {
                                    break;
                                }
                            }
                            break;
                        case 2:
                            Console.Clear();
                            cashTransfer(customer);
                            Write("\nPress any key to continue.");
                            ReadKey();
                            Console.Clear();
                            break;
                        case 3:
                            Console.Clear();
                            depositCash(customer);
                            Console.Clear();
                            break;
                        case 4:
                            Console.Clear();
                            displayBalance(customer);
                            WriteLine("Press ay key to continue");
                            ReadKey();
                            break;
                        case 5:
                            Console.Clear();
                            l_break = true;
                            break;
                        default:
                            WriteLine("Invalid input. Press any key to continue.");
                            ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                catch (System.FormatException)
                {
                    WriteLine("Invalid input please enter integer value Press any key to continue");
                    ReadKey();
                    Console.Clear();
                }
                catch (OverflowException)
                {
                    WriteLine("Please select from above options. Press any key to Continue");
                    ReadKey();
                    Console.Clear();
                }
                catch(Exception)
                {
                    WriteLine("Invalid input. Press any key to continue.");
                }
                if(l_break == true)
                {
                    break;
                }
            }
        }
        /*Method for fast cash withdraw*/
        public void fastCash(Customer_BO customer)
        {
            bool m_loop = false;
            while(true)
            {
                Console.Clear();
                Customer_BLL customer_BLL = new Customer_BLL();
                int denomination = 0; int money = 0;
                char confirmation = ' '; bool i_loop1 = false;
                WriteLine("1----500");
                WriteLine("2----1000");
                WriteLine("3----2000");
                WriteLine("4----5000");
                WriteLine("5----10000");
                WriteLine("6----15000");
                WriteLine("7----20000");
                WriteLine("Select one of the denominations of money:  ");
                try
                {
                    denomination = System.Convert.ToInt32(ReadLine());
                    switch (denomination)
                    {
                        case 1:
                            money = 500;
                            m_loop = true;
                            break;
                        case 2:
                            money = 1000;
                            m_loop = true;
                            break;
                        case 3:
                            money = 2000;
                            m_loop = true;
                            break;
                        case 4:
                            money = 5000;
                            m_loop = true;
                            break;
                        case 5:
                            money = 10000;
                            m_loop = true;
                            break;
                        case 6:
                            money = 15000;
                            m_loop = true;
                            break;
                        case 7:
                            money = 20000;
                            m_loop = true;
                            break;
                    }
                    if (denomination > 0 && denomination < 7)
                    {
                        while (i_loop1 != true)
                        {
                            try
                            {
                                WriteLine($"Are you sure you want to withdraw Rs.{money}(Y / N) ?  ");
                                confirmation = System.Convert.ToChar(ReadLine());
                                if (confirmation == 'Y' || confirmation == 'y')
                                {
                                    (bool, bool, bool) data = customer_BLL.cashWithdraw(customer, money);
                                    bool result = data.Item1;
                                    bool daily_tr = data.Item2;
                                    bool low_balance = data.Item3;
                                    if (result == true)
                                    {
                                        showTransactionResult(result, customer, money);
                                    }
                                    if(daily_tr == true)
                                    {
                                        WriteLine("Daily transaction limit is 20000. you can't go beyond this limit.");
                                    }
                                    if(money > 20000)
                                    {
                                        WriteLine("Transaction amount should be less than 20000.");
                                    }
                                    if(low_balance == true)
                                    {
                                        WriteLine("Your account balance is low for this transaction");
                                    }
                                    i_loop1 = true;
                                }
                                else if (confirmation == 'n' || confirmation == 'N')
                                {
                                    WriteLine("Cash is not withdrawal");
                                    i_loop1 = true;
                                }
                                else
                                {
                                    WriteLine("Input is not correct select Y|N ");
                                }
                            }
                            catch (Exception)
                            {
                                WriteLine("Input is not correct Please select Y|N ");
                            }
                        }
                    }
                    else
                    {
                        WriteLine("Invalid input Please select above denomination. Press any key to continue.");
                        ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception)
                {
                    WriteLine("Input is not correct Select Above denominations. Press any key to continue.");
                    ReadKey();
                }
                if(m_loop == true)
                {
                    break;
                }
            }
        }
        /*Method for noraml cash withdraw*/
        public void normalCash(Customer_BO customer)
        {
            while(true)
            {
                Console.Clear();
                try
                {
                    WriteLine("Enter the withdrawal amount: ");
                    int money = System.Convert.ToInt32(ReadLine());
                    if(money < 0)
                    {
                        throw new Exception();
                    }
                    Customer_BLL customer_BLL = new Customer_BLL();
                    (bool, bool, bool) data = customer_BLL.cashWithdraw(customer, money);
                    bool result = data.Item1;
                    bool daily_tr = data.Item2;
                    bool low_balance = data.Item3;
                    if (result == true)
                    {
                        showTransactionResult(result, customer, money);//function show result of transcation
                    }
                    if(daily_tr == true)
                    {
                        WriteLine("Daily transaction limit is 20000. you can't go beyond this limit.");
                    }
                    if (money > 20000)
                    {
                        WriteLine("Transaction amount should be less than 20000.");
                    }
                    if(low_balance == true)
                    {
                        WriteLine("Your account balance is low for this transaction");
                    }
                    break;
                }
                catch (Exception)
                {
                    WriteLine("Input is not correct Select corect amount. Press any key to continue.");
                    ReadKey();
                    WriteLine("");
                }
            }
        }
        /*helper method to show transcation is completed or not*/
        private void showTransactionResult(bool result, Customer_BO customer,int money)
        {
            bool l_break = false;
            if (result == true)
            {
                WriteLine("Cash Successfully Withdrawn!");
                while(true)
                {
                    try
                    {
                         WriteLine("Do you wish to print a receipt(Y/ N)?  ");
                         char answer = System.Convert.ToChar(ReadLine());
                         if (answer == 'y' || answer == 'Y')
                         {
                            Console.Clear();
                            showRecepit(customer, "Cash WithDrawal", money);
                            l_break = true;
                         }
                         else if(answer == 'n' || answer == 'N')
                         {
                            WriteLine("");
                            l_break = true;
                         }
                         else
                         {
                            WriteLine("Invalid input Please select Y|N. ");
                         }
                         if(l_break == true)
                         {
                            break;
                         }
                    }
                    catch (Exception)
                    {
                        WriteLine("Invalid input Please select Y|N. ");
                    }
                }
            }
            else
            {
                WriteLine("You Balance low for this transaction.");
            }
        }
        /*helper method to take input in integer format*/
        private int inputInt(string str)
        {
            bool l_break = false; int result = 0;
            while (true)
            {
                try
                {
                    Write(str);
                    result = System.Convert.ToInt32(ReadLine());
                    l_break = true;
                }
                catch (FormatException)
                {
                    WriteLine("Please enter only integer value. Press any key to continue.");
                    ReadKey();
                    WriteLine("");
                }
                catch (OverflowException)
                {
                    WriteLine("Please enter valid range of input. press any key to continue");
                    ReadKey();
                    WriteLine("");
                }
                catch (Exception)
                {
                    WriteLine("Invalid input. Press any key to continue");
                    ReadKey();
                    WriteLine("");
                }
                if (l_break == true)
                {
                    break;
                }
            }
            return result;
        }
        /*Method for cash transfer from one account to another*/
        public void cashTransfer(Customer_BO depositor)
        {
            bool m_loop = false;
            bool l_break = false;
            Customer_BO customer = new Customer_BO();
            Customer_BLL customer_BLL = new Customer_BLL();
            while(true)
            {
                Console.Clear();
                try
                {
                    int amount = inputInt("Enter amount in multiples of 500:   ");
                    if(amount % 500 != 0 || amount < 0)
                    {
                        Write("Enter correct amount ");
                        throw new Exception();
                    }
                    int accountNO = inputInt("\nEnter the account number to which you want to transfer:   ");
                    if(accountNO < 1)
                    {
                        Write("Please enter valid AccountNo ");
                        throw new Exception();
                    }

                    customer = customer_BLL.giveAccount(accountNO);
                    int accountNO2 = -1;//beacuse account numbr is never -ve
                    try
                    {
                        Write($"\nYou wish to deposit Rs {amount} in account held by Mr. {customer.holderName};" +
                        $" If this information is correct please re - enter the account number:   ");
                        accountNO2 = System.Convert.ToInt32(ReadLine());
                    }
                    catch(Exception)
                    {

                    }

                    if (accountNO == accountNO2)
                    {
                        if(customer_BLL.Transfer(depositor, customer, amount) == true)
                        {
                            WriteLine("Transaction confirmed.");
                        }
                    }
                    else
                    {
                        WriteLine("Account Number is wrong");
                        l_break = true;
                        m_loop = true;
                    }
                    while (l_break != true)
                    {
                        try
                        {
                            Write("Do you wish to print a receipt(Y / N)?   ");
                            char answer = System.Convert.ToChar(ReadLine());

                            if (answer == 'y' || answer == 'Y')
                            {
                                Console.Clear();
                                showRecepit(depositor, "Cash Transfer", amount);
                                l_break = true;
                                m_loop = true;
                            }
                            else
                            {
                                WriteLine("");
                                l_break = true;
                                m_loop = true;
                            }
                            if (l_break == true)
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            WriteLine("Input is not correct Select corect amount.");
                        }
                    }
                }
                catch (Exception)
                {
                    WriteLine("Input is not correct Press any key to continue");
                    ReadKey();
                }
                if(m_loop == true)
                {
                    break;
                }
            }
            
        }
        /*method to deposite cash in account*/
        public void depositCash(Customer_BO customer)
        {
            bool m_l_break = false;
            while(true)
            {
                Console.Clear();
                int amount; bool l_break = false;
                try
                {
                    amount = inputInt("Enter the cash amount to deposit:   ");
                    if(amount < 1)
                    {
                        Write("Enter correct amount ");
                        throw new Exception();
                    }
                    Customer_BLL customer_BLL = new Customer_BLL();
                    bool result = customer_BLL.deposite(customer, amount);
                    if (result == true)
                    {
                        WriteLine("Cash Deposited Successfully.");
                        while (true)
                        {
                            try
                            {
                                Write("Do you wish to print a receipt(Y / N) ?   ");
                                char answer = System.Convert.ToChar(ReadLine());
                                if (answer == 'y' || answer == 'Y')
                                {
                                    Console.Clear();
                                    showRecepit(customer, "Cash Deposite", amount);
                                    WriteLine("Press any key to continue");
                                    ReadKey();
                                    l_break = true;
                                    m_l_break = true;
                                }
                                else if (answer == 'n' || answer == 'Y')
                                {
                                    WriteLine("");
                                    l_break = true;
                                    m_l_break = true;
                                }
                                else
                                {
                                    throw new Exception();
                                }
                                if (l_break == true)
                                {
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                WriteLine("Input is not correct. Please select Y|N");
                            }
                        }
                    }
                    else
                    {
                        WriteLine("Transaction cannot completed");
                    }
                }
                catch (Exception)
                {
                    WriteLine("Input is not correct Press any key to continue");
                    ReadKey();
                    WriteLine("");
                }
                if(m_l_break == true)
                {
                    break;
                }
            }
            
        }
        /*Helper method show Recepit*/
        private void showRecepit(Customer_BO customer, string transType,int amount)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string date = dateTime.ToString("dd/MM/yyyy");
            WriteLine($"Account #{customer.Account_No}");
            WriteLine("Date " + date);
            WriteLine($"Amount {transType}: {amount}");
            WriteLine($"Balance: {customer.Balance}");
        }
        /*Method display plance of customer*/
        public void displayBalance(Customer_BO customer)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string date = dateTime.ToString("dd/MM/yyyy");
            WriteLine($"Account #{customer.Account_No}");
            WriteLine("Date " + date);
            WriteLine($"Balance: {customer.Balance}");
        }
    }
}
