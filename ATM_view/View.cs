using ATM_BLL;
using ATM_BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static System.Console;
namespace ATM_view
{
    public class View
    {
        /*
         * USER NAME and Passwrod of both customer and administrator are saved after encryption.
         * One customer from file user name and password are
         * User name = ariz123
         * Password = 12345
         * 
         * Administrator user name and password are
         * User name = Gull
         * Password = 12345
         */
        public void homeScreen()
        {
            int choice = 0;bool l_break = false;
            while(true)
            {
                Console.Clear();
                WriteLine("\n\n\t\t\t\t ATM  SOFTWARE\n\n");
                WriteLine("\t\t\t Please Select the LogIn Type.\n\n\t\t\t Enter 1. For Administrator,\n\t\t\t       2. For Customer" +
                    "\n\t\t\t       3. For exit.");
                try
                {
                    Write("\t\t\t       ");
                    choice = System.Convert.ToInt32(ReadLine());
                    switch (choice)
                    {
                        case 1:
                            string pre_login = "";
                            int login_count = 1;
                            bool loop_break = false;
                            while (true)
                            {
                                Console.Clear();
                                Write("\n\n\t\t\tEnter login:  ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string u_id = ReadLine();
                                Console.ForegroundColor = ConsoleColor.White;
                                Write("\t\t\tEnter Pin code:  ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string pin = pinInput();
                                Console.ForegroundColor = ConsoleColor.White;
                                Administrator_BLL administrator_BLL = new Administrator_BLL();
                                if (administrator_BLL.loginAccessAdmin(u_id, pin) == true)
                                {
                                    Administrator_View administrator_View1 = new Administrator_View();
                                    administrator_View1.administratorMenu();
                                    loop_break = true;
                                }
                                if (administrator_BLL.loginAccessAdmin(u_id, pin) == false)
                                {
                                    WriteLine("\n\n\t\tWrong Credientials Press any Key to continue\t\t");
                                    ReadKey();
                                    Console.Clear();
                                }
                                if (pre_login == u_id)
                                {
                                    login_count++;
                                }
                                pre_login = u_id;
                                if (loop_break == true)
                                {
                                    break;
                                }
                                if (login_count == 3)
                                {
                                    administrator_BLL.disableAccount(u_id);
                                    WriteLine("\n\nYou enter 3 time wrong password your account is disable Please contact to administrator. Press any key to continue");
                                    ReadKey();
                                    Console.Clear();
                                    break;
                                }
                            }
                            break;
                        case 2:
                            bool loop_b = false;
                            int count = 1;
                            string previousLogin = "";
                            while (true)
                            {
                                /*For disable account we suspose that customer always enter correct user name;
                                 * password is incorrect 
                                 Hence, we disable the account for this user name customer*/
                                Console.Clear();
                                WriteLine("\n\n\t\t\tCustomer Service.\n\n");
                                Write("\t\t\tEnter login:  ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string login = ReadLine();
                                Console.ForegroundColor = ConsoleColor.White;
                                Write("\t\t\tEnter Pin code:  ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string pin_code = pinInput();          
                                Console.ForegroundColor = ConsoleColor.White;
                                Customer_BLL customer_BLL = new Customer_BLL();
                                if (customer_BLL.loginAccess(login, pin_code) == true)
                                {
                                    Customer_BO customer = customer_BLL.createCustomer(login, pin_code);
                                    Customer_View customer_View = new Customer_View();
                                    customer_View.customerMenu(customer);
                                    loop_b = true;
                                }
                                else
                                {
                                    WriteLine("\n\n\tWrong Credientials or account is disable Press any Key to continue\t\t\t");
                                    ReadKey();
                                }
                                if(previousLogin == login)
                                {
                                    count++;
                                }
                                previousLogin = login;
                                if (loop_b == true)
                                {
                                    break;
                                }
                                if(count == 3)
                                {
                                    customer_BLL.disableAccount(login);
                                    WriteLine("\n\nYou enter 3 time wrong password your account is disable Please contact to administrator. Press any key to continue");
                                    ReadKey();
                                    Console.Clear();
                                    break;
                                }
                            }
                            break;
                        case 3:
                            l_break = true;
                            break;
                        default:
                            WriteLine("\n\tinvalid input Press any key to continue.\t");
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
                    WriteLine("\n\n\tPlease enter 1 or 2 or 3 Press any key to Continue");
                    ReadKey();
                    Console.Clear();
                }
                catch(Exception e)
                {
                    WriteLine(e.Message + " Invalid Input.");
                }
                if (l_break == true)
                {
                    break;
                }
            }
            
        }
        /*Helper method to hide password in input*/
        private string pinInput()
        {
            string pin_code = String.Empty;
            while (true)
            {
                ConsoleKeyInfo key = ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (pin_code.Length > 0)
                    {
                        pin_code = pin_code.Substring(0, pin_code.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (key.KeyChar != '\u0000')
                {
                    pin_code += key.KeyChar;
                    Console.Write("*");
                }
            }
            return pin_code;
        }
    }
}
