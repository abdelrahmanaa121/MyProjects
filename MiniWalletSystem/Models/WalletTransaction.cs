using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWalletSystem.Models
{
    internal class WalletTransaction
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime DateTime { get; set; }
        //FK
        public int walletID { get; set; }
        // Navigation Property
        public Wallet Wallet { get; set; }
    }
}
