////// See https://aka.ms/new-console-template for more information

////using ConsoleApp1;
////using System;



//////List<Listitems> DemoList = new List<Listitems>();

//////DemoList.Add(new Listitems(101, "Anubhaw", "anubhawkumar@mccit.co.in"));
//////DemoList.Add(new Listitems(101, "Anubhaw", "anubhawkumar@mccit.co.in"));

//////foreach (var item in DemoList)
//////{
//////    Console.WriteLine(item.Id);
//////}


////// =================== Practice question on List ================================





////while (true)
////{
////    Console.WriteLine("\n===== STUDENT MANAGEMENT =====");
////    Console.WriteLine("1. Add Student");
////    Console.WriteLine("2. Remove Student");
////    Console.WriteLine("3. Search Student");
////    Console.WriteLine("4. Placement Eligible Students");
////    Console.WriteLine("5. Batch Statistics");
////    Console.WriteLine("6. Display All Students");
////    Console.WriteLine("7. Exit");

////    Console.Write("\nEnter Choice : ");

////    int choice = Convert.ToInt32(Console.ReadLine());

////    switch (choice)
////    {
////        case 1:
////            StudentHelper.AddStudent();
////            break;

////        case 2:
////            StudentHelper.RemoveStudent();
////            break;

////        case 3:
////            StudentHelper.SearchStudent();
////            break;

////        case 4:
////            StudentHelper.ShowEligibleStudents();
////            break;

////        case 5:
////            StudentHelper.ShowStatistics();
////            break;

////        case 6:
////            StudentHelper.DisplayAllStudents();
////            break;

////        case 7:
////            Console.WriteLine("Thank You");
////            return;

////        default:
////            Console.WriteLine("Invalid Choice.");
////            break;
////    }
////}

////// --------------------------------------------

using ConsoleApp1;
using System.Collections.Generic;
using System.Diagnostics;

//Dictionary<int, string> di = new Dictionary<int, string>();

//di.Add(101, "Avijit");
//di.Add(102, "Komal");
//di.Add(103, "Shaheen");
//di.Add(104, "Anubhaw");
//di.Add(106, "Anmol");
//di.Add(107, "Surbhi");

//foreach (var item in di)
//{
//    Console.WriteLine($"Employee Id : {item.Key} & Employee Name : {item.Value}");

//}


//// -----------------------------------------------------
//Country country = new Country()
//{
//    StudentId = 101,
//    CountryCode = "IN",
//    Name = "India",
//    Standard = "Grade 10",
//    Fees = "50000 INR"
//};
////List<Country> countriesList = new List<Country>()
////{
////    new Country { StudentId = 101, CountryCode = "IN", Name = "India", Standard = "Grade 10", Fees = "50000 INR" },
////    new Country { StudentId = 102, CountryCode = "US", Name = "United States", Standard = "Grade 12", Fees = "1200 USD" },
////    new Country { StudentId = 103, CountryCode = "GB", Name = "United Kingdom", Standard = "Grade 11", Fees = "950 GBP" },
////    new Country { StudentId = 104, CountryCode = "AU", Name = "Australia", Standard = "Grade 10", Fees = "1500 AUD" },
////    new Country { StudentId = 105, CountryCode = "CA", Name = "Canada", Standard = "Grade 12", Fees = "1100 CAD" }
////};

//Dictionary<int, Country> dt = new Dictionary<int, Country>() {
//    {101, new Country{ StudentId = 101, CountryCode = "IN", Name = "Niharika Jaiswal", Standard = "Grade 10", Fees = "50000 INR" } },
//    {102,new Country { StudentId = 102, CountryCode = "US", Name = "Sunidhi Singh", Standard = "Grade 12", Fees = "1200 USD" } },
//    {103,new Country { StudentId = 103, CountryCode = "GB", Name = "Mayank Bhardawaj ", Standard = "Grade 11", Fees = "950 GBP" } },
//    {104,new Country { StudentId = 104, CountryCode = "AU", Name = "Summit Chatterjee", Standard = "Grade 10", Fees = "1500 AUD" } },
//    {105,new Country { StudentId = 105, CountryCode = "CA", Name = "Saloni Gupta", Standard = "Grade 12", Fees = "1100 CAD" } }
//};

//Console.WriteLine("Enter StudentId to fetch details ");
//int input = Convert.ToInt32(Console.ReadLine());
////if (dt.ContainsKey(input))
////{
////    dt[input].Name = "Sunidhi Kumari";
////}
//foreach (var item in dt)
//{
//    if(input == item.Key)
//    Console.WriteLine($"Student Id : {item.Value.StudentId} | Student Name : {item.Value.Name} |" +
//        $"Student CountryCode : {item.Value.CountryCode} | Student Standard : {item.Value.Standard} | Student Fees : {item.Value.Fees} | ");
//}

List<Customers> customer = CreateCustomer(10000);
List<Orders> orders = CreateOrders(50000);

Console.WriteLine($"Customers : {customer.Count}");
Console.WriteLine($"Orders    : {orders.Count}");



Stopwatch stopwatch = Stopwatch.StartNew();


Dictionary<int, Customers> customerDictionary =customer.ToDictionary(c => c.CustomerId);

stopwatch.Stop();

Console.WriteLine();
Console.WriteLine(
    $"Dictionary creation time: {stopwatch.ElapsedMilliseconds} ms");



////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Restart();

List<CustomerOrders> results = new List<CustomerOrders>();
foreach(var item in orders)
{
    results.Add(
        new CustomerOrders
        {
            CustomerId = item.CustomerId,
            CustomerName = customer.Find(c => c.CustomerId == item.CustomerId)?.CustomerName ?? "Unknown",
            OrderId = item.OrderId,
            Amount = item.Amount
        });
}
stopwatch.Stop();
Console.WriteLine( $"Order processing time: {stopwatch.ElapsedMilliseconds} ms");

//////////////////////////////////
//stopwatch.Restart();

//List<CustomerOrders> results = new List<CustomerOrders>();

//foreach (Orders order in orders)
//{
//    if (customerDictionary.TryGetValue(
//            order.CustomerId,
//            out Customers? customerval))
//    {
//        results.Add(new CustomerOrders
//        {
//            OrderId = order.OrderId,
//            CustomerName = customerval.CustomerName,
//            CustomerId = customerval.CustomerId,
//            Amount = order.Amount
//        });
//    }
//}

//stopwatch.Stop();

//Console.WriteLine(
//    $"Order processing time: {stopwatch.ElapsedMilliseconds} ms");

//Console.WriteLine(
//    $"Matched orders: {results.Count}");

//////////////////////////////

static List<Customers> CreateCustomer(int count)
{
    List<Customers> customers = new List<Customers>();

    for(int i=1; i<=count; i++)
    {
        customers.Add(new Customers
        {
            CustomerId = i,
            CustomerName = $"Customer-{i}",
            CustomerEmail = $"Customer{i}@email.com"
        });
    }
    return customers;

}

static List<Orders> CreateOrders(int count)
{
    List<Orders> orders = new List<Orders>();

    for (int i = 1; i <= count; i++)
    {
        Random random = new Random();
        orders.Add(new Orders
        {
            OrderId = i,
            CustomerId =random.Next(1,1001),
            Amount =random.Next(120,10000)
        });
    }
    return orders;

}






