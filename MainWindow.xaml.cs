using System;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace api
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> productList = new ObservableCollection<Product>();
        private List<Product> TempProductList = new List<Product>();

        // Init variable id when click in listview product
        private int selectedId = 0;


        // Set my WooCommerce API credentials and the URL of your store
        private string consumerKey = "ck_93e51d31f052691467d638b8e53f6afdb6596e41";
        private string consumerSecret = "cs_b74cfe5b05277d90194fa30e0b4c3616d8318d52";
        private string storeUrl = "http://localhost/HauShop";

        public MainWindow()
        {
            InitializeComponent();

            lvProduct.ItemsSource = productList; // Set the ListView's ItemsSource to the collection

            getAllProduct();
            
        }

        // Create HttpClient & authentication
        private HttpClient CreateHttpClient_Auth()
        {
            // Create a new HttpClient with the base URL of the WooCommerce API
            var httpClient = new HttpClient { BaseAddress = new Uri($"{storeUrl}/wp-json/wc/v3/") };
            // Add authentication headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{consumerKey}:{consumerSecret}")));
            return httpClient;
        }

        // Get all product 
        private async void getAllProduct()
        {
            // Clear listview
            productList.Clear();
            
            TempProductList.Clear();

            var httpClient = CreateHttpClient_Auth();

            // Get product of all page. Start page 1
            int pageNumber = 1;
            while (true)
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "products?page=" + pageNumber);

                    // Send the request to the WooCommerce API
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {

                        string jsonString = await response.Content.ReadAsStringAsync();
                        List<Product>? products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(jsonString);


                        if (products != null)
                        {
                            if (products.Count < 0) break;
                            foreach (Product product in products)
                            {
                                Product itemProduct = new Product
                                {
                                    id = product.id,
                                    name = product.name,
                                    stock_quantity = product.stock_quantity,
                                    stock_status = product.stock_status,
                                    description = product.description,
                                    type = product.type,
                                    regular_price = product.regular_price,
                                };
                                productList.Add(itemProduct);
                            }
                            pageNumber++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: " + response.ReasonPhrase);
                        break;
                    }
                }
                catch
                {
                    break;
                }
            }

        }

        // Get one product by ID
        private async Task<Product> getProductById(int id)
        {
            // Gọi hàm để tạo httpClietn và thêm auth vào header
            var httpClient = CreateHttpClient_Auth();

            // Create a new HttpRequestMessage with the product data 
            // with Post method with get method
            var request = new HttpRequestMessage(HttpMethod.Get, "products/" + id);

            // Send the request to the WooCommerce API
            var response = await httpClient.SendAsync(request);

            string jsonString = await response.Content.ReadAsStringAsync();

            Product? product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(jsonString);

            return product;
        }
        // Add new product
        private async void addNewProduct(string jsonProduct)
        {

            // Gọi hàm để tạo httpClient và thêm auth vào header
            var httpClient = CreateHttpClient_Auth();

            // Create a new HttpRequestMessage 
            // with the product data with Post method
            var request = new HttpRequestMessage(HttpMethod.Post, "products")
            {
                Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json")
            };

            // Send the request to the WooCommerce API
            var response = await httpClient.SendAsync(request);

            // If the request was successful, 
            // display the product ID that was added
            if (response.IsSuccessStatusCode)
            {
                // Muốn xử lý gì thì xử lý ở đây khi thành công
                // var responseBody = await response.Content.ReadAsStringAsync();
                // var addedProduct = JsonConvert.DeserializeObject<dynamic>(responseBody);

            }
            else
            {
                MessageBox.Show($"Failed to add product: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }

        // Update product by ID
        private async void updateProductById(int id, string jsonProduct)
        {
            // Gọi hàm để tạo httpClietn và thêm auth vào header
            var httpClient = CreateHttpClient_Auth();

            // Create a new HttpRequestMessage 
            // with the product data wiht Put method
            var request = new HttpRequestMessage(HttpMethod.Put, "products/" + id)
            {
                Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);

            // If the request was successful, display the product ID that was added
            if (response.IsSuccessStatusCode)
            {
                //update selectedID
                selectedId = 0;
                // var responseBody = await response.Content.ReadAsStringAsync();
                // var addedProduct = JsonConvert.DeserializeObject<dynamic>(responseBody);

            }
            else
            {
                // Console.WriteLine($"Failed to add product: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                MessageBox.Show($"Failed to add product: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }

        // Delete product by ID
        private async void deleteProductById(int id)
        {
            // Gọi hàm để tạo httpClietn và thêm auth vào header
            var httpClient = CreateHttpClient_Auth();

            // Create a new HttpRequestMessage with the product data
            var request = new HttpRequestMessage(HttpMethod.Delete, "products/" + id);

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //update selectedID
                selectedId = 0;
            }
            else
            {
                // Console.WriteLine($"Failed to add product: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                MessageBox.Show($"Failed to add product: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }

        }

        // Add a new product to the collection
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addProductForm = new WpfApp.AddProductForm();

            if (addProductForm.ShowDialog() == true)
            {
                // Get the entered product details from the form addProductForm
                string? name = addProductForm.NameProduct;
                string? description = addProductForm.Description;
                decimal price = addProductForm.Price;
                int quantity = addProductForm.Quantity;

                // Define the product data to add
                bool is_manage_stock = false;
                if (addProductForm.Quantity > 0)
                {
                    is_manage_stock = true;
                }
                var product = new
                {
                    name = addProductForm.NameProduct?.ToString(),
                    type = "simple",
                    regular_price = addProductForm.Price.ToString(),
                    description = addProductForm.Description?.ToString(),
                    manage_stock = is_manage_stock,
                    stock_quantity = addProductForm.Quantity,
                };

                // Convert the product data to JSON
                var jsonProduct = JsonConvert.SerializeObject(product);

                addNewProduct(jsonProduct);
            }
        }

        // When itemlick in listView 
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if an item is selected
            if (lvProduct.SelectedItem != null)
            {
                // Access the selected item and retrieve its values
                Product selectedItem = (Product)lvProduct.SelectedItem;
                string? id = selectedItem.id;

                // Save id pick when item click
                if (id != null)
                {
                    selectedId = int.Parse(id);
                }
            }
        }

        // Update the selected product in the collection
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Gọi hàm getProductById để nhận kết quả trả về
            Product product = await getProductById(selectedId);

            if (product != null)
            {
                var updateProductForm = new WpfApp.UpdateProductForm();
                updateProductForm.setData(product);

                if (updateProductForm.ShowDialog() == true)
                {
                    int id = updateProductForm.Id;

                    // Define the product data to update
                    var productUpdate = new
                    {
                        name = updateProductForm.NameProduct?.ToString(),
                        type = updateProductForm.Type?.ToString(),
                        manage_stock = true,
                        stock_quantity = updateProductForm.Quantity.ToString(),
                        regular_price = updateProductForm.Price.ToString(),
                        description = updateProductForm.Description?.ToString(),

                    };

                    // Convert data from json
                    var jsonProduct = JsonConvert.SerializeObject(productUpdate);

                    updateProductById(id, jsonProduct);
                }
            }
        }

        // Delete product button ui
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId > 0) {
                 deleteProductById(selectedId);
            }
           
        }

        //Reload product display listview
        private void ReloadClick_Click(object sender, RoutedEventArgs e)
        {
            getAllProduct();
        }

    }
}