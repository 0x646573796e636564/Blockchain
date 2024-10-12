using System.Collections.Generic;

public class TransactionPool
{
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    public void AddTransaction(Transaction transaction)
    {
        if (transaction.VerifyTransaction())
        {
            Transactions.Add(transaction);
        }
        else
        {
            Console.WriteLine("Invalid transaction.");
        }
    }

    public List<Transaction> GetPendingTransactions()
    {
        return Transactions;
    }
}