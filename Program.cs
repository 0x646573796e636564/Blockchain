using System;
using Microsoft.VisualBasic;

namespace BlockchainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the blockchain
            Blockchain myBlockchain = new Blockchain();

            // Display the blockchain before mining
            //myBlockchain.DisplayBlockchain();

            // Add some sample transactions
            AddSampleTransactions(myBlockchain);

            // Mine a block for the specified miner
            myBlockchain.MinePendingTransactions("Server");

            // Display the blockchain after mining
            //myBlockchain.DisplayBlockchain();

            // Optional: Add more transactions and mine again
            AddSampleTransactions(myBlockchain);
            myBlockchain.MinePendingTransactions("Server");

            // Final display of the blockchain
            //myBlockchain.DisplayBlockchain();
        }

        private static void AddSampleTransactions(Blockchain blockchain)
        {
            var transaction1 = new Transaction
            {
                Sender = "Daniel",
                Receiver = "Rich",
                Amount = 10
            };
    transaction1.SignTransaction(); // Ensure to sign the transaction

            var transaction2 = new Transaction
            {
                Sender = "Michael",
                Receiver = "Ritchie",
                Amount = 5
            };
    transaction2.SignTransaction(); // Ensure to sign the transaction

    // Add the signed transactions to the transaction pool
    blockchain.AddTransaction(transaction1);
    blockchain.AddTransaction(transaction2);

    blockchain.Log("Sample transactions added to the transaction pool.");
    //Console.ReadLine();
        }
    }
}