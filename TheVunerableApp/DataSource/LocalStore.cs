/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD4nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheVunerableApp.Model;

namespace TheVunerableApp.DataSource
{
    internal class LocalStore
    {
        public LocalStore()
        {
            FilePath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["TRPath"];
        }

        public string FilePath { get; }

        public bool StoreTransactions(Transaction transaction)
        {
            string transactionInJson = JsonSerializer.Serialize(transaction);
            string path = transaction.TransactionId + ".json";
            File.WriteAllText(Path.Combine(FilePath, path), transactionInJson);
            return true;
        }

        public Transaction LoadTransaction(string path)
        {
            /*1. 
             * Identified as CWE-502 Deserialization of Untrusted Data
             * 29/09/2023 identified by Nishan Shrestha
             * 29/09/2023 Exploited by Nishan Shresth
             * 29/09/2023 Patched by nishan Shrestha
             */
            try
            {
                string jsonContent = File.ReadAllText(path);
                Console.WriteLine(jsonContent);

                // Deserialize and validate the JSON structure
                Transaction transaction = JsonSerializer.Deserialize<Transaction>(jsonContent);

                // Check if the deserialized transaction follows the expected structure
                if (IsValidTransaction(transaction))
                {
                    
                    return transaction;
                }
                else
                {
                    throw new Exception("Invalid JSON structure or content.");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log, rethrow, etc.)
                Console.WriteLine("Error loading transaction: " + ex.Message);
                return null;
            }

            //return JsonSerializer.Deserialize<Transaction>(jsonContent);
        }
                private bool IsValidTransaction(Transaction transaction)
                {
                     return !string.IsNullOrEmpty(transaction.TransactionId)
                        && !string.IsNullOrEmpty(transaction.TransactionDate)
                        && !string.IsNullOrEmpty(transaction.SourceAccount)
                        && !string.IsNullOrEmpty(transaction.TargetAccount)
                        && transaction.Amount >= 0;

                }
    }
}
