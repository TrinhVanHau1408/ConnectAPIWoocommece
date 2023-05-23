using System;
using System.Windows;

namespace WpfApp
{
    public partial class AddProductForm : Window
    {
       
        // Properties to hold the entered product details
        public string ?NameProduct { get; set; }
        public string ?Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public AddProductForm()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Set the entered product details
            NameProduct = nameTextBox.Text;
            Description = descriptionTextBox.Text;
            Price = decimal.Parse(priceTextBox.Text);
            Quantity = int.Parse(quantityTextBox.Text);

            DialogResult = true;
            Close();
        }
    }
}