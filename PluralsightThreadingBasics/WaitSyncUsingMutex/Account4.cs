using System;
using System.Runtime.InteropServices;
using System.Threading;

public class BankAccount
{
    public readonly int AccountNumber;
    double _balance;
    readonly Mutex _lock = new Mutex();

    public BankAccount(int acctNum, double initDeposit)
    {
        AccountNumber = acctNum;
        _balance = initDeposit;
    }

    public void Credit(double amt)
    {
        if (_lock.WaitOne())
        {
            try
            {
                double temp = _balance;
                temp += amt;
                Thread.Sleep(1);
                _balance = temp;
            }
            finally
            {
                _lock.ReleaseMutex();
            }
        }
    }

    public void Debit(double amt)
    {
        Credit(-amt);
    }

    public double Balance
    {
        get
        {
            double balance = 0;
            if(_lock.WaitOne())
            {
                try
                {
                    balance = _balance;
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
            return (balance);
        }
    }

    public void TransferFrom(BankAccount otherAcct, double amt)
    {

        Console.WriteLine("[{0}] Transfering {1:C0} from account {2} to {3}",
                          Thread.CurrentThread.Name, amt,
                          otherAcct.AccountNumber, this.AccountNumber);

        Mutex[] locks = {_lock, otherAcct._lock};
        if (WaitHandle.WaitAll(locks))
        {
            try
            {
                Thread.Sleep(100);
                otherAcct.Debit(amt);
                this.Credit(amt);
            }
            finally
            {
                foreach (Mutex mutexLock in locks)
                {
                    mutexLock.ReleaseMutex();
                }
            }
            
        }

    }
}
