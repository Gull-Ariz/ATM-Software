using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ATM_BO;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace ATM_DAL
{
    public class Base_DAL
    {
        /*method serialize the customer object and write in file*/
        public bool save(Customer_BO customer, string fileName)
        {
            bool result = false;
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                if (!File.Exists(filePath))
                {
                    FileStream fs = File.Create(filePath);
                    fs.Close();
                }
                string u_id = String.Empty;
                string pin = String.Empty;
                char[] array1 = new char[customer.userName.Length];
                char[] array2 = new char[customer.Password.Length];
                array1 = customer.userName.ToCharArray();
                array2 = customer.Password.ToCharArray();
                for (int i = 0; i < customer.userName.Length; i++)
                {
                    u_id += Encryption(array1[i]);
                }
                for (int i = 0; i < customer.Password.Length; i++)
                {
                    pin += Encryption(array2[i]);
                }
                customer.userName = u_id;
                customer.Password = pin;
                string jsonObject = JsonSerializer.Serialize(customer);
                StreamWriter sw = new StreamWriter(filePath, append: true);
                sw.WriteLine(jsonObject);
                sw.Close();
                result = true;
                return result;
            }
            catch(Exception)
            {
                result = false;
                return result;
            }
        }
        public char Encryption(char c)
        {
            if (Char.IsDigit(c))
            {
                return (char)((int)'9' - (int)c + (int)'0');
            }
            else
            {
                if(c >= 'a' && c <= 'z')
                {
                    return (char)((int)'z' - (int)c + (int)'a');
                }
                else if(c >= 'A' && c <= 'Z')
                {
                    return (char)((int)'Z' - (int)c + (int)'A');
                }
                else
                {
                    return c;
                }
            }
        }
        public char Decryption(char c)
        {
            if (Char.IsDigit(c))
            {
                return (char)((int)'0' - (int)c + (int)'9');
            }
            else
            {
                if (c >= 'a' && c <= 'z')
                {
                    return (char)((int)'a' - (int)c + (int)'z');
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    return (char)((int)'A' - (int)c + (int)'Z');
                }
                else
                {
                    return c;
                }
            }
        }

        /*method read and deserialize objects from file and write in a list*/
        public List<Customer_BO> readFile(string fileName)
        {
            Customer_BO customer = null; List<Customer_BO> list = new List<Customer_BO>();
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                if (File.Exists(filePath))
                {
                    StreamReader sr = new StreamReader(filePath);
                    string jsoninput = sr.ReadLine();
                    while (jsoninput != null)
                    {
                        customer = JsonSerializer.Deserialize<Customer_BO>(jsoninput);
                        string u_id = String.Empty;
                        string pin = String.Empty;
                        char[] array1 = new char[customer.userName.Length];
                        char[] array2 = new char[customer.Password.Length];
                        array1 = customer.userName.ToCharArray();
                        array2 = customer.Password.ToCharArray();
                        for (int i = 0; i < customer.userName.Length; i++)
                        {
                            u_id += Decryption(array1[i]);
                        }
                        for (int i = 0; i < customer.Password.Length; i++)
                        {
                            pin += Decryption(array2[i]);
                        }
                        customer.userName = u_id;
                        customer.Password = pin;
                        list.Add(customer);
                        jsoninput = sr.ReadLine();
                    }
                    sr.Close();
                }
                return list;
            }
            catch(Exception)
            {
                list = null;
                return list;
            }
        }

        /*Method give account no last account created*/
        public int getHighestAccountNo(string fileName)
        {
            try
            {
                int accountNo = 0; Customer_BO customer = null;
                string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                if (File.Exists(filePath))
                {
                    StreamReader sr = new StreamReader(filePath);
                    string jsoninput = sr.ReadLine();
                    while (jsoninput != null)
                    {
                        customer = JsonSerializer.Deserialize<Customer_BO>(jsoninput);
                        if (customer.Account_No > accountNo)
                        {
                            accountNo = customer.Account_No;
                        }
                        jsoninput = sr.ReadLine();
                    }
                    sr.Close();
                }
                return accountNo + 1;
            }
            catch(Exception)
            {
                return -1;
            }
        }
        /*Method actually write list back to file*/
        public bool Write(List<Customer_BO> list,string fileName)
        {
            bool result = false;
            try
            {
                if (list != null)
                {
                    string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                    if (!File.Exists(filePath))
                    {
                        FileStream fs = File.Create(filePath);
                        fs.Close();
                    }
                    StreamWriter sw = new StreamWriter(filePath);
                    foreach (Customer_BO cus in list)
                    {
                        string u_id = String.Empty;
                        string pin = String.Empty;
                        char[] array1 = new char[cus.userName.Length];
                        char[] array2 = new char[cus.Password.Length];
                        array1 = cus.userName.ToCharArray();
                        array2 = cus.Password.ToCharArray();
                        for (int i = 0; i < cus.userName.Length; i++)
                        {
                            u_id += Encryption(array1[i]);
                        }
                        for (int i = 0; i < cus.Password.Length; i++)
                        {
                            pin += Encryption(array2[i]);
                        }
                        cus.userName = u_id;
                        cus.Password = pin;
                        string jsonObject = JsonSerializer.Serialize(cus);
                        sw.WriteLine(jsonObject);
                    }
                    sw.Close();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch(Exception)
            {
                result = false;
            }
            return result;
        }
        /*Method read the transaction file and return as a list*/
        public List<Transaction_BO> readTransactionFile(string fileName)
        {
            List<Transaction_BO> list = new List<Transaction_BO>();
            try
            {
                Transaction_BO transaction = null;
                string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                if (File.Exists(filePath))
                {
                    StreamReader sr = new StreamReader(filePath);
                    string jsoninput = sr.ReadLine();
                    while (jsoninput != null)
                    {
                        transaction = JsonSerializer.Deserialize<Transaction_BO>(jsoninput);
                        list.Add(transaction);
                        jsoninput = sr.ReadLine();
                    }
                    sr.Close();
                }
                return list;
            }
            catch(Exception)
            {
                list = null;
                return list;
            }
        }
        /*method save the transactions into file*/
        public bool saveTransactionFile(Transaction_BO transaction,string fileName)
        {
            bool result = false;
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                if (!File.Exists(filePath))
                {
                    FileStream fs = File.Create(filePath);
                    fs.Close();
                }
                string jsonObject = JsonSerializer.Serialize(transaction);
                StreamWriter sw = new StreamWriter(filePath, append: true);
                sw.WriteLine(jsonObject);
                sw.Close();
                result = true;
                return result;
            }
            catch (Exception)
            {
                result = false;
                return result;
            }
        }
        /*Method read the Administrator file and return as a list*/
        public List<Administrator_BO> readAdministratorFile(string fileName)
        {
            List<Administrator_BO> list = new List<Administrator_BO>();
            try
            {
                Administrator_BO administrator = new Administrator_BO();
                string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                if (File.Exists(filePath))
                {
                    StreamReader sr = new StreamReader(filePath);
                    string jsoninput = sr.ReadLine();
                    while (jsoninput != null)
                    {
                        administrator = JsonSerializer.Deserialize<Administrator_BO>(jsoninput);
                        string u_id = String.Empty;
                        string pin = String.Empty;
                        char[] array1 = new char[administrator.userName.Length];
                        char[] array2 = new char[administrator.Password.Length];
                        array1 = administrator.userName.ToCharArray();
                        array2 = administrator.Password.ToCharArray();
                        for (int i = 0; i < administrator.userName.Length; i++)
                        {
                            u_id += Decryption(array1[i]);
                        }
                        for (int i = 0; i < administrator.Password.Length; i++)
                        {
                            pin += Decryption(array2[i]);
                        }
                        administrator.userName = u_id;
                        administrator.Password = pin;
                        list.Add(administrator);
                        jsoninput = sr.ReadLine();
                    }
                    sr.Close();
                }
                return list;
            }
            catch (Exception)
            {
                list = null;
                return list;
            }
        }
        /*Method save the list of administrator into file*/
        public bool saveAdminList(List<Administrator_BO> list, string fileName)
        {
            bool result = false;
            try
            {
                if (list != null)
                {
                    string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                    if (!File.Exists(filePath))
                    {
                        FileStream fs = File.Create(filePath);
                        fs.Close();
                    }
                    StreamWriter sw = new StreamWriter(filePath);
                    foreach (Administrator_BO admin in list)
                    {
                        string u_id = String.Empty;
                        string pin = String.Empty;
                        char[] array1 = new char[admin.userName.Length];
                        char[] array2 = new char[admin.Password.Length];
                        array1 = admin.userName.ToCharArray();
                        array2 = admin.Password.ToCharArray();
                        for (int i = 0; i < admin.userName.Length; i++)
                        {
                            u_id += Encryption(array1[i]);
                        }
                        for (int i = 0; i < admin.Password.Length; i++)
                        {
                            pin += Encryption(array2[i]);
                        }
                        admin.userName = u_id;
                        admin.Password = pin;
                        string jsonObject = JsonSerializer.Serialize(admin);
                        sw.WriteLine(jsonObject);
                    }
                    sw.Close();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
