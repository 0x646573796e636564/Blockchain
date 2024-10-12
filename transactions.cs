using System;
using System.Security.Cryptography;
using System.Text;

public class Transaction
{
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public decimal Amount { get; set; }
    public string Signature { get; private set; }

    private readonly Cryptography crypto;

    public Transaction()
    {
        // Use a valid PEM-encoded private key for testing
        string privateKey = @"
-----BEGIN PRIVATE KEY-----
MIICXAIBAAKBgQCq22GoZiPaAm/oB6qVhL2RvzYN4ynxPIYrRyMNmgCHGT4mIfRN
tlZrYzntHnjToq+budoPjR4xUx7UBiMibBzw1+mBL8Hpq9WAb8oSVIid7QFhcflt
FDg4qj4RSpz9XQqWN2vX5DU01hRvADjwhC80jgKb6ZDi63suqE5YKuW95QIDAQAB
AoGBAIo0MuaY2F88oXC64s1XNlByrzLEkeTE5HKVdFFIRS0CyjbmOEu0NmtfNDgN
8gafDhWVXEJAEDAt4D35SOpLMYx9P5SZBRR7cGXmMyswwRwXL0LLUvKo3MkL7dbA
I9L++ttzJJ+lPqprjg+qoPKJDRnGaj1OVfBv3cJViLYs2fwBAkEA2XkckzbFm9ju
oAlsZ0pCmws3y2p6uz6LI75IjTlTrwzsAY8njcsqOO1/lSpdReHTxmqBWmfs0SeF
L+QJDf9UJQJBAMkgH+GDo4xNdcpM1vita/0gjXcW8Zha41FsUdGP/4ITZ0T8hy9E
oJNWSI9ZdYtgWjblI4ne91mE91Gc/yKxtsECQHqE6v5dKzNEIhvzcyk2AxRKW6K0
WHTJJaZ7e3BkzaqfQw8V0ZjmzuDHnMsy1N2b/q2YL0v5pMeo1jZG6HsEUCkCQA/9
J8urxWKv/b85YJWgY8dZwSVIg6hTAWNFszNvuSZEGJ+ZW73cPM+5ukb7G6ca39Eh
YjPhr9REB81LJ9VAP4ECQE04acseBZWuwjNVyaR73w6Qdv7WC1UQFUtPlOlGXMbS
oHJeXfaZBKTp1uc06uaGW0Mfb062f9uywYxAOfrfPxA=
-----END PRIVATE KEY-----";

        crypto = new Cryptography(privateKey);
    }

    public void SignTransaction()
    {
        // Create a string representation of the transaction
        string dataToSign = $"{Sender}:{Receiver}:{Amount}";
        Signature = crypto.Sign(dataToSign);
    }

    public bool VerifyTransaction()
    {
        // Create a string representation of the transaction to verify
        string dataToVerify = $"{Sender}:{Receiver}:{Amount}";
        return crypto.Verify(dataToVerify, Signature);
    }

    public override string ToString()
    {
        return $"{Sender}->{Receiver}: {Amount}";
    }
}