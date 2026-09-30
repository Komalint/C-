//// See https://aka.ms/new-console-template for more information
//using MyFirstConsoleApp;

//Console.WriteLine("Hello, World!");

//// Input from user 
////int num1, num2, sum;
////Console.WriteLine("Enter first number");
////num1 = Convert.ToInt32(Console.ReadLine());
////Console.WriteLine("Enter first number");
////num2 = Convert.ToInt32(Console.ReadLine());
////sum = num1 + num2;
////Console.WriteLine("The sum of {0} and {1} is : {2}",num1,num2,sum);

////Student student = new Student(1,"Komal Gupta" , "11 Seal Lane" , "Computer Science");
////Console.WriteLine("Student Id : {0}" ,student.Id);
////Console.WriteLine("Student Name {0}", student.Name);
////Console.WriteLine("Student Address {0}", student.Address);
////Console.WriteLine("Student Course {0}", student.Course);

////Console.WriteLine("Enter Another Key:");
////ConsoleKeyInfo var2 = Console.ReadKey();
////Console.WriteLine($"\nEntered Key: {var2.Key} KeyChar:{var2.KeyChar} ASCII:{(int)var2.KeyChar}");


//// Employee e1 = new Employee(101, "Anubhaw", 15000f);
//// e1.display();

//Console.WriteLine("--------------------------------1-----------------------------");
//Payment p1 =new Payment("Savings", "2547 8956 2578 9451", 4500 ,"Security token here","UPI");
//Console.WriteLine($"Account Type :   {p1.AccountType}");
//Console.WriteLine($"Card Number : { p1.CardNumbar}");
//Console.WriteLine($"Account Balance : {p1.AccountBalance}");
//Console.WriteLine($"Security Token : {p1.SecurityToken}");
//Console.WriteLine($"Payment Type : {p1.PaymentType}");

//Console.WriteLine("----------------------------2---------------------------------");
//PatientRecord p = new PatientRecord("P101", "John Smith", "Diabetes and Hypertension", 72, 120);
//Console.WriteLine($"Patient ID: {p.PatientId}");
//Console.WriteLine($"Patient Name: {p.PatientName}");
//Console.WriteLine($"Medical History: {p.MedicalHistory}");
//Console.WriteLine($"Heart Rate: {p.HeartRate}");
//Console.WriteLine($"Blood Pressure: {p.BloodPressure}");
//Console.WriteLine($"Billing Flag: {p.BillingStatus}");


//Console.WriteLine("------------------------------3-------------------------------");
//Employee e1 = new FullTime(101, "John", 50000, 8000);
//Employee e2 = new ContarctEmployee(102, "David", 40000, 5000);
//Employee e3 = new Freelancer(103, "Smith", 10000, 500, 20);

//e1.display();

//Console.WriteLine();

//e2.display();

//Console.WriteLine();

//e3.display();

//Console.WriteLine();

//Console.WriteLine("-----------------------------4--------------------------------");
//ElectricTruck truck = new ElectricTruck("TRK101", "Mumbai", 50, 80);

//truck.LogGPS();          // Inherited from Vehicle
//truck.StartEngine();     // Inherited from MotorizedVehicle
//truck.DisplayDetails();  // Own Method


//Console.WriteLine("--------------------------5-----------------------------------");




//            List<Notification> notifications = new List<Notification>()
//            {
//                new EmailNotification(),
//                new SMSNotification(),
//                new WhatsAppNotification(),
//            };

//        foreach (Notification n in notifications)
//            {
//                n.SendNotification("message");   
//            }


//Console.WriteLine("--------------------------6-----------------------------------");
//ShapeAreaCal s = new ShapeAreaCal("Rectangle", 10, 20, 30);

//s.Draw("Rectangle");

//Console.WriteLine("Area of Rectangle : " + s.Area(10, 20));
//Console.WriteLine("Area of Cuboid : " + s.Area(10, 20, 30));


//Console.WriteLine("--------------------------7-----------------------------------");
//SmartDevice d1 = new SmartLight();
//SmartDevice d2 = new SmartThermostat();
//SmartDevice d3 = new SmartAC();

//d1.TurnOn();
//Console.WriteLine(d1.GetEnergyConsumption());
//d1.TurnOff();

//d2.TurnOn();
//Console.WriteLine(d2.GetEnergyConsumption());
//d2.TurnOff();

//d3.TurnOn();
//Console.WriteLine(d3.GetEnergyConsumption());
//d3.TurnOff();

//Console.WriteLine("--------------------------8-----------------------------------");
//Loan l1 = new HomeLoan();
//Loan l2 = new CarLoan();
//Loan l3 = new EducationLoan();

//l1.VerifyDocuments();
//Console.WriteLine(l1.CalculateInterestRate());
//Console.WriteLine(l1.CheckEligibility());
//l1.SanctionAmount();

//Console.WriteLine();

//l2.VerifyDocuments();
//Console.WriteLine(l2.CalculateInterestRate());
//Console.WriteLine(l2.CheckEligibility());
//l2.SanctionAmount();

//Console.WriteLine();

//l3.VerifyDocuments();
//Console.WriteLine(l3.CalculateInterestRate());
//Console.WriteLine(l3.CheckEligibility());
//l3.SanctionAmount();
//Console.WriteLine();


//Console.WriteLine("--------------------------9-----------------------------------");

//ICloudStorageProvide obj1 = new S3Storage();

//obj1.UploadFile("Employee_Records_2026.xlsx");
//obj1.DownloadFile("EMP-2026-001");
//obj1.DeleteFile("OLD-BACKUP-2025");
//Console.WriteLine();

//ICloudStorageProvide obj2 = new AzureStorage();

//obj2.UploadFile("Project_Report_Q3.pdf");
//obj2.DownloadFile("PRJ-Q3-2026");
//obj2.DeleteFile("TEMP-PROJECT-FILE");
//Console.WriteLine();

//Console.WriteLine("--------------------------10-----------------------------------");

//BaseRobot robot = new WarehouseRobot("WR-101");

//robot.StartRobot();
//robot.PerformTask();

//WarehouseRobot wr = new WarehouseRobot("WR-101");

//wr.StartRobot();
//wr.PerformTask();
//wr.Navigate();
//wr.ChargeBattery();

//Console.WriteLine("-------------------------------------------------------------------");

//sEmployee emp1 = new Developer
//{
//    Id = 1001,
//    Name = "Ramesh",
//    Salary = 500000,
//    Designation = "Developer"
//};
//double bonus = emp1.CalculateBonus(emp1.Salary);
//Console.WriteLine($"Name: {emp1.Name}, Designation: {emp1.Designation}, Salary: {emp1.Salary}, Bonus:{bonus}");
//Console.WriteLine();
//sEmployee emp2 = new Manager
//{
//    Id = 1002,
//    Name = "Sachin",
//    Salary = 800000,
//    Designation = "Manager"
//};
//bonus = emp2.CalculateBonus(emp2.Salary);
//Console.WriteLine($"Name: {emp2.Name}, Designation: {emp2.Designation}, Salary: {emp2.Salary}, Bonus:{bonus}");
//Console.WriteLine();






using PartialClassDemo;

partialEmployee emp = new partialEmployee
{
    FirstName = "Avijit",
    LastName = "Rout",
    Salary = 100000,
    Gender = "Male"
};
emp.DisplayFullName();
emp.DisplayEmployeeDetails();
Console.ReadKey();