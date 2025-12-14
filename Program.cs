using System;

// ADMIN CLASS
class Admin
{
    private string username;
    private string password;

    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public double TotalRequiredMoney;
    public static double TotalCollectedMoney;

    public Admin()
    {
        Username = "admin";
        Password = "1234";
        TotalRequiredMoney = 9000000;
        TotalCollectedMoney = 0;
    }

    public bool Login()
    {
        Console.Write("\nAdmin Username: ");
        string u = Console.ReadLine();

        Console.Write("Admin Password: ");
        string p = Console.ReadLine();

        if (u == Username && p == Password)
        {
            Console.WriteLine("Admin Login Successful!");
            return true;
        }


        Console.WriteLine("Invalid Admin Credentials!");
        return false;
    }

    public void AdminMenu(FundGroup fund)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine($"\n--- ADMIN PANEL ({fund.GroupName}) ---");
            Console.WriteLine("1. Change Fund Name");
            Console.WriteLine("2. Change Category Name");
            Console.WriteLine("3. Change Required Money");
            Console.WriteLine("4. View Fund Money Status");
            Console.WriteLine("0. Logout");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter New Fund Name: ");
                fund.GroupName = Console.ReadLine();
            }
            else if (choice == "2")
            {
                Console.Write("Select Category (1-5): ");
                int c = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter New Name: ");
                string n = Console.ReadLine();

                if (c == 1) fund.Topic1.Name = n;
                else if (c == 2) fund.Topic2.Name = n;
                else if (c == 3) fund.Topic3.Name = n;
                else if (c == 4) fund.Topic4.Name = n;
                else if (c == 5) fund.Topic5.Name = n;
            }
            else if (choice == "3")
            {
                Console.Write("Enter New Required Money: ");
                TotalRequiredMoney = Convert.ToDouble(Console.ReadLine());
            }
            else if (choice == "4")
            {
                Console.WriteLine("\n--- FUND MONEY STATUS ---");
                Console.WriteLine($"Required Money : {TotalRequiredMoney}");
                Console.WriteLine($"Collected Money: {TotalCollectedMoney}");
            }
            else if (choice == "0")
            {
                exit = true;
            }
        }
    }
}

// INTERFACES 
interface Display
{
    void Display();
}

interface Donate
{
    double Donate();
}

//  ABSTRACT BASE CLASS 
abstract class BaseCategory : Display, Donate
{
    public string Name;

    public BaseCategory(string name)
    {
        Name = name;
    }

    public virtual void Display()
    {
        Console.WriteLine($"Category: {Name}");
    }

    public virtual double Donate()
    {
        Console.WriteLine($"\nDonation for {Name}");
        return Donation.MakeDonation();
    }

    public abstract void SpecialFeature();
}

//  FUND CATEGORY 
class FundCategory : BaseCategory
{
    public FundCategory(string name) : base(name) { }

    public override void SpecialFeature()
    {
        Console.WriteLine($"Special Feature: Support from {Name}");
    }

    public void CategoryMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine($"\n {Name}");
            Console.WriteLine("1. Donate");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                double donatedAmount = Donate();
                Admin.TotalCollectedMoney += donatedAmount;

                Console.Write("Donate again? (y/n): ");
                if (Console.ReadLine().ToLower() != "y")
                    exit = true;
            }
            else if (choice == "0")
            {
                exit = true;
            }
        }
    }
}

//  DONATION CLASS 
static class Donation
{
    public static double MakeDonation()
    {
        Console.Write("Your Name: ");
        Console.ReadLine();

        Console.Write("Email ID: ");
        Console.ReadLine();

        Console.Write("Country: ");
        Console.ReadLine();
       

        Console.WriteLine("\nChoose ID Type:");
        Console.WriteLine("1. Passport");
        Console.WriteLine("2. NID");
        Console.Write("Select (1/2): ");
        Console.ReadLine();
         Console.Write("Enter Payment Method: ");
        string Method= Console.ReadLine();
        Console.WriteLine("NGO's Payment Number: 01293734445 ");
        

        Console.Write("Donation Amount: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("\nThank you for Donating.");
        return amount;
    }
}

//  FUND GROUP =
class FundGroup
{
    public string GroupName;
    public FundCategory Topic1;
    public FundCategory Topic2;
    public FundCategory Topic3;
    public FundCategory Topic4;
    public FundCategory Topic5;

    public FundGroup(string name)
    {
        GroupName = name;
        LoadDefaultCategories();
    }

    public virtual void LoadDefaultCategories()
    {
        Topic1 = new FundCategory("Category 1");
        Topic2 = new FundCategory("Category 2");
        Topic3 = new FundCategory("Category 3");
        Topic4 = new FundCategory("Category 4");
        Topic5 = new FundCategory("Category 5");
    }

    public void Menu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine($"\n {GroupName}");
            Console.WriteLine($"1. {Topic1.Name}");
            Console.WriteLine($"2. {Topic2.Name}");
            Console.WriteLine($"3. {Topic3.Name}");
            Console.WriteLine($"4. {Topic4.Name}");
            Console.WriteLine($"5. {Topic5.Name}");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            if (choice == "1") Topic1.CategoryMenu();
            else if (choice == "2") Topic2.CategoryMenu();
            else if (choice == "3") Topic3.CategoryMenu();
            else if (choice == "4") Topic4.CategoryMenu();
            else if (choice == "5") Topic5.CategoryMenu();
            else if (choice == "0") exit = true;
        }
    }
}

// CHILD FUND CLASSES 
class DisasterReliefFund : FundGroup
{
    public DisasterReliefFund() : base("Disaster Relief Fund") { }

    public override void LoadDefaultCategories()
    {
        Topic1 = new FundCategory("Emergency Help");
        Topic2 = new FundCategory("Medical Help");
        Topic3 = new FundCategory("Shelter Help");
        Topic4 = new FundCategory("Homeless Families Support");
        Topic5 = new FundCategory("Safety Support");
    }
}

class WarzoneReliefFund : FundGroup
{
    public WarzoneReliefFund() : base("Warzone Relief Fund") { }

    public override void LoadDefaultCategories()
    {
        Topic1 = new FundCategory("Emergency Help");
        Topic2 = new FundCategory("Medical Help");
        Topic3 = new FundCategory("Shelter Help");
        Topic4 = new FundCategory("Refugee Families Support");
        Topic5 = new FundCategory("Safety Support");
    }
}

class AnimalShelterFund : FundGroup
{
    public AnimalShelterFund() : base("Animal Shelter Fund") { }

    public override void LoadDefaultCategories()
    {
        Topic1 = new FundCategory("Food & Items");
        Topic2 = new FundCategory("Animal Treatment");
        Topic3 = new FundCategory("Shelter Support");
        Topic4 = new FundCategory("Adoption Help");
        Topic5 = new FundCategory("Rescue Work");
    }
}

class HealthcareSupportFund : FundGroup
{
    public HealthcareSupportFund() : base("Healthcare Support Fund") { }

    public override void LoadDefaultCategories()
    {
        Topic1 = new FundCategory("Vaccines");
        Topic2 = new FundCategory("Hospital Tools");
        Topic3 = new FundCategory("Free Checkup Camps");
        Topic4 = new FundCategory("Health Awareness");
        Topic5 = new FundCategory("Patient Help");
    }
}

class EducationSupportFund : FundGroup
{
    public EducationSupportFund() : base("Education Support Fund") { }

    public override void LoadDefaultCategories()
    {
        Topic1 = new FundCategory("School Items");
        Topic2 = new FundCategory("Study Scholarships");
        Topic3 = new FundCategory("Teacher Training");
        Topic4 = new FundCategory("Library Support");
        Topic5 = new FundCategory("Student Help");
    }
}

//MAIN PROGRAM 
class Program
{
    static void Main()
    {
        DisasterReliefFund disaster = new DisasterReliefFund();
        WarzoneReliefFund warzone = new WarzoneReliefFund();
        AnimalShelterFund animal = new AnimalShelterFund();
        HealthcareSupportFund health = new HealthcareSupportFund();
        EducationSupportFund edu = new EducationSupportFund();

        Admin admin = new Admin();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n--NGO FUND PLATFORM--");
            Console.WriteLine("1. Disaster Relief Fund");
            Console.WriteLine("2. Warzone Relief Fund");
            Console.WriteLine("3. Animal Shelter Fund");
            Console.WriteLine("4. Healthcare Support Fund");
            Console.WriteLine("5. Education Support Fund");
            Console.WriteLine("6. Admin Login");
            Console.WriteLine("0. Exit");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            if (choice == "1") disaster.Menu();
            else if (choice == "2") warzone.Menu();
            else if (choice == "3") animal.Menu();
            else if (choice == "4") health.Menu();
            else if (choice == "5") edu.Menu();
            else if (choice == "6" && admin.Login())
            {
                Console.Write("Select Fund (1-5): ");
                string f = Console.ReadLine();

                if (f == "1") admin.AdminMenu(disaster);
                else if (f == "2") admin.AdminMenu(warzone);
                else if (f == "3") admin.AdminMenu(animal);
                else if (f == "4") admin.AdminMenu(health);
                else if (f == "5") admin.AdminMenu(edu);
            }
            else if (choice == "0")
                exit = true;
        }

        Console.WriteLine("THANK YOU");
    }
}
