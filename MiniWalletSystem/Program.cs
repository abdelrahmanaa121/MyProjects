using MiniWalletSystem.Models;

namespace MiniWalletSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Test CRUD
            /*using (AppDbContext appDbContext = new AppDbContext())
            {
                //Create New User
                User Newuser = new User() { Name = "Abdelrahman", Email = "abdelrahman@gmail.com" };
                //Create New Wallet
                Wallet Newwallet = new Wallet()
                {
                    Balance = 0, //Intial balance = 0
                    Owner = Newuser //FK 
                };
                //Add Entites to Context
                appDbContext.Add(Newuser);
                appDbContext.Add(Newwallet);
                appDbContext.SaveChanges();
                Console.WriteLine("User and Wallet Created successfully");
                Console.WriteLine("------------------------------");
                //Read & Update
                var MyWallet = appDbContext.wallets.FirstOrDefault(w => w.UserID == Newuser.UserID);
                if (MyWallet != null)
                {
                    MyWallet.Balance += 1500;
                    appDbContext.SaveChanges();
                }
            } */
            #endregion
            Console.WriteLine(" *_* Welcome to mini E-Wallet System *_*");
            using (AppDbContext context = new AppDbContext())
            {
                #region Login or Create a New Account

                Console.WriteLine("Please Enter your Email:");
                string Email = Console.ReadLine();

                //Check if email already exists in DB
                var CurrentUser = context.users.FirstOrDefault(u => u.Email == Email);
                Wallet currentwallet = null;

                if (CurrentUser == null)
                {
                    Console.WriteLine("Create New Account User");
                    Console.WriteLine("Enter your name");
                    string Name = Console.ReadLine();

                    CurrentUser = new User() { Name = Name, Email = Email };
                    currentwallet = new Wallet() { Balance = 0, Owner = CurrentUser };

                    context.users.Add(CurrentUser);
                    context.wallets.Add(currentwallet);
                    context.SaveChanges();

                    Console.WriteLine("Your account and wallet have been created successfully");
                }

                else
                {
                    Console.WriteLine($"Welcome Back, {CurrentUser.Name}");
                    currentwallet = context.wallets.FirstOrDefault(w => w.UserID == CurrentUser.UserID);
                }
                #endregion

                #region Ineractive Menu
                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("--- Main Menu ---");
                    Console.WriteLine("1. Check Balance");
                    Console.WriteLine("2. Deposit Money");
                    Console.WriteLine("3. Withdraw Money");
                    Console.WriteLine("4. View Account Statement (Transaction History)");
                    Console.WriteLine("5. Exit");
                    Console.Write("Choose an option: ");
                    string Choose = Console.ReadLine();
                    switch (Choose)
                    {
                        case "1":
                            Console.WriteLine($"Your account balance = {currentwallet.Balance}");
                            break;
                        case "2":
                            Console.WriteLine("Enter amount to deposite");
                            if (decimal.TryParse(Console.ReadLine(), out decimal depAmount) && depAmount > 0)
                            {
                                currentwallet.Balance += depAmount;

                                var transaction = new WalletTransaction()
                                {
                                    Amount = depAmount,
                                    TransactionType = "Deposite",
                                    DateTime = DateTime.Now,
                                    walletID = currentwallet.WalletID
                                };
                                context.walletTransactions.Add(transaction);

                                context.SaveChanges();
                                Console.WriteLine($"Deposit successful. Your new balance is: {currentwallet.Balance} SAR ");
                            }
                            else
                            {
                                Console.WriteLine("Invalid amount. Please enter a valid number");
                            }
                            break;
                        case "3":
                            Console.WriteLine("Enter amount to Withdraw");

                            if (decimal.TryParse(Console.ReadLine(), out decimal witAmount) && witAmount > 0)
                            {
                                if (currentwallet.Balance >= witAmount)
                                {
                                    currentwallet.Balance -= witAmount;

                                    var transaction = new WalletTransaction()
                                    {
                                        Amount = witAmount,
                                        TransactionType = "Withdraw",
                                        DateTime = DateTime.Now,
                                        walletID = currentwallet.WalletID
                                    };
                                    context.walletTransactions.Add(transaction);

                                    context.SaveChanges();
                                    Console.WriteLine($"Withdraw successful. Your new balance is: {currentwallet.Balance} SAR ");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Insufficient funds! (Current balance: {currentwallet.Balance} SAR)");
                            }
                            break;
                        case "4":
                            Console.WriteLine("Transaction History");
                            var transc = context.walletTransactions
                                .Where(t => t.walletID == currentwallet.UserID)
                                .OrderByDescending(t => t.DateTime)
                                .ToList();
                            if (!transc.Any())
                            {
                                Console.WriteLine("No transactions found yet.");
                            }
                            else
                            {
                                foreach (var tx in transc)
                                {
                                    Console.WriteLine($"[{tx.DateTime}] | Type: {tx.TransactionType,-10} | Amount: {tx.Amount} SAR");
                                }
                            }
                            Console.WriteLine("-------------------------");
                            break;
                        case "5":
                            exit = true;
                            Console.WriteLine(" ^_^ Thank you for using the E-Wallet System ^_^ ");
                            break;
                        default:
                            Console.WriteLine(" Invalid choice, please try again.");
                            break;

                    }
                }
                #endregion
            }
        }
    }
}
