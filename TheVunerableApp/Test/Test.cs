/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD4nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheVunerableApp.Controller;
using TheVunerableApp.View;

namespace TheVunerableApp.Test
{
    internal class Test
    {
        //Added over here
            
        public static void closeAccount() //CWE-306 Missing Authentication for Critical Function (patched in AccountController.cs)

                                          //This can be exploit ofr CWE-287 Improper Authentication as
                                          //there is no way to authenticate the user. Any user could close an account
                                          //(patched in program.cs)
        {
            AccountController.CloseAccount("6763996216", "46101163", "SSB19-01355436A");
        }

        public static void loadTransaction() //CWE-502 Deserialization of Untrusted Data (patched in LocalStore.cs)
        {
            TransactionController.LoadTransaction("badfile");

        }

        public static void accountBalance() //CWE-125 Out of Bounds Read (Patched in UserController.cs)
        {
            List<string> balances =  UserController.ListOfAccountBalance("8829905701");
            foreach (string balance in balances) 
            {
                Console.WriteLine(balance);
            }

        }
        public static void createUser() // CWE-862 Missing Authorization (patched in UserController.cs)
        {
            AdminView.CreateUser();
        }
    }
}
