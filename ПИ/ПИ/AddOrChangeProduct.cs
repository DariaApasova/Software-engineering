using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПИ
{
    public partial class AddOrChangeProduct : Form
    {
        string check;
        int newID;
        public AddOrChangeProduct(int id, string ch, Products p)
        {
            InitializeComponent();
            check = ch;
            newID = id;
            load(p);

        }
        Products products;
        private void load(Products p)
        {
            products = p;
            comboBox1.Items.Add("Туалетная вода");
            comboBox1.Items.Add("Парфюмерная вода");
            if (check=="add")
            {
                dateTimePicker1.Value = DateTime.Now;
            }
            if(check=="change")
            {
                Product change;
                foreach(Product prod in products.getList().Values)
                {
                    if(prod.id==newID)
                    {
                        change = prod;
                    }
                }
                dateTimePicker1.Enabled = false;
                dateTimePicker1.Value = change.dateIn;
                numericUpDown1.Value = change.price;
                numericUpDown2.Value = change.amount;
                numericUpDown3.Value = change.timeRealize;
                textBox1.Text = change.title;
                textBox2.Text = change.manufacturer;
                comboBox1.SelectedItem = change.type;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || comboBox1.SelectedIndex == -1 || textBox2.Text == string.Empty || numericUpDown1.Value == 0 || numericUpDown2.Value == 0||numericUpDown3.Value==0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля для добавления товара");
            }
            else
            {
                Product p = new Product(newID);
                p.title = textBox1.Text;
                p.type = comboBox1.SelectedItem.ToString();
                p.manufacturer = textBox2.Text;
                p.dateIn = dateTimePicker1.Value;
                p.price = numericUpDown1.Value;
                p.amount = Convert.ToInt16(numericUpDown2.Value);
                p.timeRealize = Convert.ToInt16(numericUpDown3.Value);
                p.sales = 0;
                p.edIzm = "шт.";
                products.addProduct(p);
                products.saveProduct();
                MessageBox.Show("Продукт успешно добавлен");
                button2.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
