using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWalletSystem.Models
{
    internal class Wallet
    {
        //Properties
        public int WalletID { get; set; }
        public decimal Balance { get; set; }
        //Foreign Key
        public int UserID { get; set; }
        //Navigation properties
        public User Owner { get; set; }

    }
}
