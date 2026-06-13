using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWalletSystem.Models
{
    internal class User
    {
        //Properties
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //Navigation properties
        public Wallet UserWallet { get; set; }
    }
}
