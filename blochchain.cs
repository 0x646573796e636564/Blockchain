using System;
using System.Collections.Generic;
using System.Linq;

public class Blockchain
{
    private const string LogFilePath = "blockchain_logs.txt";

    public List<Block> Chain { get; private set; }
    private List<Transaction> currentTransactions; // Mempool for pending transactions

    public Blockchain()
    {
        Chain = new List<Block>();
        currentTransactions = new List<Transaction>();
        Chain.Add(CreateGenesisBlock()); // Initialize the chain with the genesis block
    }

    private Block CreateGenesisBlock()
    {
        return new Block(0, DateTime.Now, new List<Transaction>(), "0"); // First block with no previous hash
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction.VerifyTransaction())
        {
            currentTransactions.Add(transaction);
            Log("Transaction added to mempool.");
        }
        else
        {
            Log("Invalid transaction. Not added to mempool.");
        }
    }

    public Block GetLatestBlock()
    {
        return Chain.Last();
    }

    public void MinePendingTransactions(string minerAddress)
    {
        // Reward the miner with a transaction
        Transaction rewardTx = new Transaction
        {
            Sender = "Network",
            Receiver = minerAddress,
            Amount = 1 // Block reward
        };

        // Add the reward transaction to the current transactions
        currentTransactions.Add(rewardTx);

        // Create a new block with the current transactions
        Block newBlock = new Block(
            Chain.Count,
            DateTime.Now,
            currentTransactions,
            GetLatestBlock().Hash
        );

        // Mine the new block
        newBlock.MineBlock(5); // You can adjust the difficulty here

        // Add the new block to the blockchain
        Chain.Add(newBlock);
        currentTransactions.Clear(); // Clear the mempool after mining
        Log("New block mined and added to the blockchain.");
    }

    public void Log(string message)
    {
        string logMessage = $"{DateTime.Now}: {message}";
         foreach (var block in Chain)
        {
            File.WriteAllText("blocklog.txt", $"Block# : {block.Index} - Hash: {block.Hash}");
            foreach (var tx in block.Transactions)
            {
                File.AppendAllText("blocklog.txt", $"Transaction: {tx}");
            }
        }
        File.AppendAllText("blocklog.txt", logMessage);
    }


    public void DisplayBlockchain()
    {
        Log("Displaying blockchain.");
        
        foreach (var block in Chain)
        {
            // Log block details
            Log($"Block #{block.Index} - Hash: {block.Hash}");
            foreach (var tx in block.Transactions)
            {
                Log($"  Transaction: {tx}"); // Assuming tx overrides ToString()
            }
        }
    }

    public bool IsValidNewBlock(Block newBlock)
    {
        Block lastBlock = GetLatestBlock();
        if (newBlock.PreviousHash != lastBlock.Hash)
        {
            Log("Invalid block: Previous hash does not match.");
            return false;
        }

        if (newBlock.Hash != newBlock.CalculateHash())
        {
            Log("Invalid block: Hash is not valid.");
            return false;
        }

        return true;
    }
}