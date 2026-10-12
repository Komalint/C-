using ConsoleApp2;
using Microsoft.Extensions.Configuration;

// Read appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional : true, reloadOnChange : true)
    .Build();



// Get connection string
string connectionString =configuration.GetConnectionString("SqlConnectionString");

// Repository object
ProductRepository repo = new ProductRepository(connectionString);

while (true)
{
    Console.WriteLine("\n1. Add Product");
    Console.WriteLine("2. View All Products");
    Console.WriteLine("3. View Product By Id");
    Console.WriteLine("4. Update Product");
    Console.WriteLine("5. Delete Product");
    Console.WriteLine("6. Exit");

    Console.Write("Enter Choice: ");
    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Stock: ");
            int stock = Convert.ToInt32(Console.ReadLine());

            repo.AddProduct(new Product
            {
                Name = name,
                Price = price,
                Stock = stock
            });
            break;

        case 2:
            foreach (var p in repo.GetAllProducts())
            {
                Console.WriteLine(
                    $"{p.Id} | {p.Name} | {p.Price} | {p.Stock}");
            }
            break;

        case 3:
            Console.Write("Enter Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var product = repo.GetProductById(id);

            Console.WriteLine(
                $"{product.Id} | {product.Name} | {product.Price} | {product.Stock}");
            break;

        case 4:
            Product updateProduct = new Product();

            Console.Write("Enter Product Id: ");
            updateProduct.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("New Name (blank to skip): ");
            string nameInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nameInput))
                updateProduct.Name = nameInput;

            Console.Write("New Price (blank to skip): ");
            string priceInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(priceInput))
                updateProduct.Price = Convert.ToDecimal(priceInput);

            Console.Write("New Stock (blank to skip): ");
            string stockInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(stockInput))
                updateProduct.Stock = Convert.ToInt32(stockInput);

            repo.UpdateProduct(updateProduct);
            break;

        case 5:
            Console.Write("Enter Id: ");
            int deleteId = Convert.ToInt32(Console.ReadLine());

            repo.DeleteproductById(deleteId);
            break;

        case 6:
            return;

        default:
            Console.WriteLine("Invalid Choice");
            break;
    }
}