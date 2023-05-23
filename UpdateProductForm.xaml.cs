using System;
using System.Windows;
namespace WpfApp
{
    public partial class UpdateProductForm : Window
    {
        public int Id{ get; set; }
        public string?NameProduct { get; set; }
        public string? Type {get; set;}

        public int Quantity {get; set;}
        public decimal Price { get; set; }
    
        public string ?Description {get; set;}

        public UpdateProductForm()
        {
            InitializeComponent();


        }

        public void setData(Product product)
        {
            idTextBoxUpdateForm.Text = product.id;
            nameTextBoxUpdateForm.Text = product.name;
            priceTextBoxUpdateForm.Text = product.regular_price;
            quantityTextBoxUpdateForm.Text = product.stock_quantity;
            cmbTypeProductUpdateForm.Text = product.type;
            descriptionTextBoxUpdateForm.Text = product.description;

        }

        private void getDataToForm()
        {

             Id = int.Parse(idTextBoxUpdateForm.Text);
             NameProduct = nameTextBoxUpdateForm.Text;
             Price = decimal.Parse(priceTextBoxUpdateForm.Text);
             Quantity = int.Parse(quantityTextBoxUpdateForm.Text);
             Type = cmbTypeProductUpdateForm.Text;
             Description = descriptionTextBoxUpdateForm.Text;


        }

        private void SaveButtonUpdateButton_Click (object sender, RoutedEventArgs e)
        {
            // Set the entered product details
            getDataToForm();
        

            DialogResult = true;
            Close();
        }




    }

}
