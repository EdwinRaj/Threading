﻿using System;
using System.Threading;

public class BankAccount
{
    public readonly int AccountNumber;
    double _balance;
    object _lock = new object();

    public BankAccount(int acctNum, double initDeposit)
    {
        AccountNumber = acctNum;
        _balance = initDeposit;
    }

    public void Credit(double amt)
    {
        lock (_lock)
        {
            double temp = _balance;
            temp += amt;
            Thread.Sleep(1);
            _balance = temp;
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
            lock (this._lock)
            {
                balance = _balance;
            }
            return (balance);
        }
    }

    public void TransferFrom(BankAccount otherAcct, double amt)
    {

        Console.WriteLine("[{0}] Transfering {1:C0} from account {2} to {3}",
                          Thread.CurrentThread.Name, amt,
                          otherAcct.AccountNumber, this.AccountNumber);
        lock (this._lock)
        {
            Thread.Sleep(100);
            lock (otherAcct._lock)
            {
                Thread.Sleep(100);
                otherAcct.Debit(amt);
                this.Credit(amt);
            }
        }
        
    }
}
