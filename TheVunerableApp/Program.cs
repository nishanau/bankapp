﻿/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD4nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;
using TheVunerableApp.Controller;
using TheVunerableApp.DataSource;
using TheVunerableApp.Model;
using TheVunerableApp.View;

namespace TheVunerableApp
{
    /*
     * This is a driver file that tests the code
     * For the assessment this class is not 
     * required to be analysed.
     * */
    internal class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Start");
            /*1.  
             * 30/09/2023 Identified as CWE-287 Improper Authentication
             * 30/09/2023 Identified by Namita Odari
             * 30/09/2023 Exploited by Namita Odari
             * 30/09/2023 Patched by Namita Odari
             */
            Program.userAuthentication();


            //Program.DbSetUpForTesting(); // For setup only Do not use it unless the db is in inconsistence state.
            //used for user authentication

            /* Following are test methods that you can use individually to run some test drivers for the code.
             * However, you should be writing your test code in Test.cs */
            //Program.CreateUser();
            // Program.UpdateCustomerDetails();
            //Program.DisplayUserDetails();
            //Program.SearchCustomerByAccountNumeber();
            // Program.getAllUserAccounts();
            //Program.AddCustomerToAccount();
            //Program.getAccountBalance();
            //Program.PrintFilePathsFromAppSettings();
            // Program.CloseCustomerAccount();


            // Program.closeAccount();  //closes account based on the parameters in Test class
            // Program.createTransaction();
           // Program.loadTransaction();
            //Program.accountBalance();

        }
        private static void loadTransaction()
        {
            Test.Test.loadTransaction();

        }

       
        private static void closeAccount()
        {
            Test.Test.closeAccount();
            
        }

        private static void accountBalance()
        {
            Test.Test.accountBalance();

        }
        private static void userAuthentication()
        {
            
            Console.WriteLine("Enter username: ");
            string username = (Console.ReadLine());

            SQLiteDB db = new SQLiteDB();
            if (db.getAuthForTest(username))
            {
                Console.WriteLine("Logged in as Admin");
                UserController.SetIsAdmin(true);
            }
            else Console.WriteLine("Logged in as Customer");
        }





        private static void PrintFilePathsFromAppSettings()
        {
            Console.WriteLine(TransactionController.getTRPath());
        }        
        private static void SearchCustomerByAccountNumeber() 
        {
            List<Customer> customers = UserController.SearchCustomerByAccountNumber("31146289");
            Console.WriteLine(customers[0]);
        }
        private static void CreateUser() 
        {
            AdminView.CreateUser();
        }

        /*1. 
         * 30/09/2023 Identified as CWE-20: Improper Input Validation
         * 30/09/2023 Identified by Nishan Shrestha
         * 30/09/2023 Exploited by Nishan Shrestha
         * 30/09/2023 Patched by Nishan Shrestha
         * 
         * Old Code:
         *          private static void UpdateCustomerDetails() 
                {
                    UserController.UpdateUser("6763996216", "Jean", "Grey", "j.grey@xmen.com", "321-456-9876");
                 }
         */
        private static void UpdateCustomerDetails() 
        {   

            //these data will be retrieved from the user in a real app
            string customerId = "6763996216";
            string fname = "Jean";
            string sname = "Grey";
            string email = "j.grey@xmen.com";
            string govId = "321-456-9876"; 

            if (string.IsNullOrWhiteSpace(customerId) || string.IsNullOrWhiteSpace(fname) ||
            string.IsNullOrWhiteSpace(sname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(govId))
            {
                throw new ArgumentException("All parameters must be non-null and non-empty.");
            }
            UserController.UpdateUser(customerId, fname, sname, email, govId );
        }
        private static void DisplayUserDetails() 
        {
            Customer customer = UserController.DisplayUserDetails("SB19-87204084A");  
            Console.WriteLine(customer);
        }
        private static void AddCustomerToAccount() 
        {
            AccountController.AddCustomerToAnAccount("89048295", "6763996216");
        }
        private static void getAllUserAccounts() 
        {
            List<string> accounts = AccountController.SearchAccountByCustomer("6763996216");
            foreach (var item in accounts)
            {
                Console.WriteLine(item);
            }
        }
        private static void getAccountBalance()
        {
           Console.WriteLine(AccountController.GetAccountBalance("89048295"));
        }
        /* private static void CloseCustomerAccount()
         {
             AccountController.CloseAccount("6763996216", "89048295");
         }*/

        private static void DbSetUpForTesting()
        {   /*
            //This is just a driver method to create baseline dataset for testing of the system
            //1. Create 1 Admin
            //2. Create 4 Users
            //3. Create 3 Current Accounts
            //4. Create 3 Savings Accounts
            //5. Create 10 Transactions between these accounts

            // Creating RequestObjects
            //1.
             string admin = UserController.CreateUser(UserType.Admin, "408-332-8739", "Charles", "Xavier", "c.xavier@xmen.com", "iCanLocate1111", DateTime.Parse("01/04/1956"), Model.Position.manager, Model.Role.Admin, "Sandy Bay", "SB19");

            //2.
             string user1 = UserController.CreateUser(UserType.Customer, "321-456-9876", "Jean", "Grey", "j.grey@xmen.com", "iReadMind1978", DateTime.Parse("27/07/2019"), Model.Position.none, Model.Role.none, "Sandy Bay", "SB19");
              string user2 = UserController.CreateUser(UserType.Customer, "456-335-1135", "Henry", "McCoy", "h.mcoy@xmen.com", "iAmTheBeast1968", DateTime.Parse("26/06/2018"), Model.Position.none, Model.Role.none, "Sandy Bay", "SB19");
              string user3 = UserController.CreateUser(UserType.Customer, "789-336-1698", "Scott", "Summers", "s.summers@xmen.com", "theThirdEye200", DateTime.Parse("23/11/2011"), Model.Position.none, Model.Role.none, "Sandy Bay", "SB19");
              string user4 = UserController.CreateUser(UserType.Customer, "870-667-0773", "Bobby", "Drake", "b.drake@xmen.com", "zeroC00l11", DateTime.Parse("17/03/2022"), Model.Position.none, Model.Role.none, "Sandy Bay", "SB19");
            
            //3.
             AccountController.CreateCurrentAccount(user1, 100, 50, 10);
              AccountController.CreateCurrentAccount(user2, 7500, 50, 10);
              AccountController.CreateCurrentAccount(user4, 9500, 50, 10);
            
            //4.
             AccountController.CreateSavingsAccount(user1, 2.4, 10000);
              AccountController.CreateSavingsAccount(user2, 4.2, 7500);
              AccountController.CreateSavingsAccount(user3, 1.9, 19807);
            

            TransactionController.StoreTransactions("46101163", 675.00, "52805275");
            Console.WriteLine("Setup complete..");

            SQLiteDB sQLiteDB = new SQLiteDB();
            sQLiteDB.ConnectToDS();
             sQLiteDB.getAuthForTest();
            */

        }
    }
}
