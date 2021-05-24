using ShoppingFormAppp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoppingFormAppp
{
    public partial class CustomerForm : Form
    {
        ShoppingContext db = new ShoppingContext();
        Product selectedProduct;
        public CustomerForm()
        {
            InitializeComponent();
        }
        public void FillCategoryCombo()
        {
            cmbCategories.Items.AddRange(db.Categories.Select(x => x.Fullname).ToArray());
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            FillCategoryCombo();
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPrice.Visible = false;
            cmbProducts.Text = "";
            string categoryName = cmbCategories.Text;
            int catId = db.Categories.FirstOrDefault(x => x.Fullname == categoryName).Id;
            var productList = db.Products.Where(x => x.CategoryId == catId).Select(x => x.Name).ToArray();
            cmbProducts.Items.Clear();
            cmbProducts.Items.AddRange(productList);

        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProduct = db.Products.FirstOrDefault(x => x.Name == cmbProducts.Text);
            lblPrice.Text = selectedProduct.Price + "Azn";
            lblPrice.Visible = true;
            nmQuantity.Value = 1;
        }

        private void nmQuantity_ValueChanged(object sender, EventArgs e)
        {
            nmQuantity.Maximum = (decimal)selectedProduct.Count;
            lblPrice.Text = selectedProduct.Price * nmQuantity.Value+ "Azn";
        }
    }
}
