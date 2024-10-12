using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; private set; }
    public int Nonce { get; private set; } // For proof of work

    public Block(int index, DateTime timestamp, List<Transaction> transactions, string previousHash)
    {
        Index = index;
        Timestamp = timestamp;
        Transactions = transactions;
        PreviousHash = previousHash;
        Hash = CalculateHash(); // Calculate hash when creating the block
    }

    public string CalculateHash()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string rawData = $"{Index}{Timestamp}{PreviousHash}{Nonce}{TransactionsToString()}";
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower(); // Return hash as a hex string
        }
    }

    public void MineBlock(int difficulty)
    {
        // Create a string of leading zeros based on the difficulty
        string leadingZeros = new string('0', difficulty);
        while (Hash == null || Hash.Substring(0, difficulty) != leadingZeros)
        {
            Nonce++; // Increment nonce
            Hash = CalculateHash(); // Recalculate hash
        }
        Console.WriteLine($"Block mined: {Hash}");
    }

    private string TransactionsToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var tx in Transactions)
        {
            sb.Append(tx.ToString()); // Ensure your Transaction class has a proper ToString() implementation
        }
        return sb.ToString();
    }
}