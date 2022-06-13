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
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
            loadProduct();
        }
        Products products;
        Dictionary<int, Product> list;
        int idchange;
        private void loadProduct()
        {
            dataGridView1.ReadOnly = true;

            comboBox1.Items.Add("Парфюмерная вода");
            comboBox1.Items.Add("Туалетная вода");
            products = new Products();
            var gg= products.getList();
            int r = 0;
            foreach(Product p in gg.Values)
            {
                fill(p, r);
                r++;
            }
        }
        private void fill(Product p, int r)
        {
            dataGridView1.Rows.Add();
            dataGridView1[0, r].Value = p.id;
            dataGridView1[1, r].Value = p.title;
            dataGridView1[2, r].Value = p.type;
            dataGridView1[3, r].Value = p.manufacturer;
            dataGridView1[4, r].Value = p.dateIn;
            dataGridView1[5, r].Value = p.price;
            dataGridView1[6, r].Value = p.amount;
            dataGridView1[7, r].Value = p.edIzm;
            dataGridView1[8, r].Value = p.timeRealize;
            dataGridView1[9, r].Value = p.sales;
        }
        private void sort()
         {
            bool a = (textBox1.Text == string.Empty);
            bool b = (textBox2.Text == string.Empty);
            bool price1 =(textBox3.Text==string.Empty);
            bool price2 = (textBox4.Text==string.Empty);
            bool date = (dateTimePicker1.Value.ToShortDateString() == "01.01.2000");
            bool type = (comboBox1.SelectedIndex==-1);
            bool count1 = (numericUpDown1.Value == 0);
            bool count2 = (numericUpDown2.Value == 0);
            List<Product> sorted1 =new List<Product>();
            List<Product> sorted2 =new List<Product>();
            List<Product> sorted3 = new List<Product>();
            List<Product> sorted4 = new List<Product>();
            if(a==false)
            {
                dataGridView1.Rows.Clear();
                int r = 0;
                foreach(Product p in list.Values)
                {
                    if(p.title.ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        fill(p,r );
                        r++;
                    }
                }
                b = true; price1 = true; price2 = true; date = true;type = true;count1 = true;count2 = true;
            }
            if (b==false)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.manufacturer.ToLower().Contains(textBox2.Text.ToLower()))
                    {
                        sorted1.Add(p);
                    }
                }
            }
            if(price1==false&&price2==false)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.price>=Convert.ToDecimal(textBox3.Text)&&p.price<=Convert.ToDecimal(textBox4.Text))
                    {
                        sorted2.Add(p);
                    }
                }
            }
            if(price1==false&&price2==true)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.price>= Convert.ToDecimal(textBox3.Text))
                    {
                        sorted2.Add(p);
                    }
                }
            }
            if(price1==true&&price2==false)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.price<= Convert.ToDecimal(textBox4.Text))
                    {
                        sorted2.Add(p);
                    }
                }
            }
            if(date==false&&type==false)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.dateIn.ToShortDateString()==Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString()&&p.type==comboBox1.SelectedItem.ToString())
                    {
                        sorted3.Add(p);
                    }
                }
            }
            if(date==false&&type==true)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.dateIn.ToShortDateString()==Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString())
                    {
                        sorted3.Add(p);
                    }
                }
            }
            if(date==true&&type==false)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.type==comboBox1.SelectedItem.ToString())
                    {
                        sorted3.Add(p);
                    }
                }
            }
            if(count1==false&&count2==false)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.amount>=numericUpDown1.Value&&p.amount<=numericUpDown2.Value)
                    {
                        sorted4.Add(p);
                    }
                }
            }
            if(count1==false&&count2==true)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.amount>=numericUpDown1.Value)
                    {
                        sorted4.Add(p);
                    }
                }
            }
            if(count1==true&&count2==false)
            {
                dataGridView1.Rows.Clear();
                foreach(Product p in list.Values)
                {
                    if(p.amount<=numericUpDown2.Value)
                    {
                        sorted4.Add(p);
                    }
                }
            }
            List<Product> itog = new List<Product>();
            if(sorted1.Count==0)
            {
                if(sorted2.Count==0&&sorted3.Count==0&&sorted4.Count!=0)
                {
                    itog = sorted4;
                }
                if(sorted2.Count==0&&sorted3.Count!=0&&sorted4.Count==0)
                {
                    itog = sorted3;
                }
                if(sorted2.Count!=0&&sorted3.Count==0&&sorted4.Count==0)
                {
                    itog = sorted2;
                }
                if(sorted2.Count!=0&&sorted3.Count!=0&&sorted4.Count==0)
                {
                    itog = sorted2.Intersect(sorted3).ToList();
                }
                if(sorted2.Count!=0&sorted3.Count==0&&sorted4.Count!=0)
                {
                    itog = sorted2.Intersect(sorted4).ToList();
                }
                if(sorted2.Count==0&&sorted3.Count!=0&&sorted4.Count!=0)
                {
                    itog = sorted3.Intersect(sorted4).ToList();
                }
                if(sorted2.Count!=0&&sorted3.Count!=0&&sorted4.Count!=0)
                {
                    itog = sorted2.Intersect(sorted3).Intersect(sorted4).ToList();
                }
            }
            if(sorted1.Count!=0)
            {
                if(sorted2.Count==0&&sorted3.Count==0&&sorted4.Count==0)
                {
                    itog = sorted1;
                }
                if (sorted2.Count == 0 && sorted3.Count == 0 && sorted4.Count != 0)
                {
                    itog =sorted1.Intersect(sorted4).ToList();
                }
                if (sorted2.Count == 0 && sorted3.Count != 0 && sorted4.Count == 0)
                {
                    itog =sorted1.Intersect(sorted3).ToList();
                }
                if (sorted2.Count != 0 && sorted3.Count == 0 && sorted4.Count == 0)
                {
                    itog =sorted1.Intersect(sorted2).ToList();
                }
                if (sorted2.Count != 0 && sorted3.Count != 0 && sorted4.Count == 0)
                {
                    itog =sorted1.Intersect(sorted2).Intersect(sorted3).ToList();
                }
                if (sorted2.Count != 0 & sorted3.Count == 0 && sorted4.Count != 0)
                {
                    itog =sorted1.Intersect(sorted2).Intersect(sorted4).ToList();
                }
                if (sorted2.Count == 0 && sorted3.Count != 0 && sorted4.Count != 0)
                {
                    itog =sorted1.Intersect(sorted3).Intersect(sorted4).ToList();
                }
                if (sorted2.Count != 0 && sorted3.Count != 0 && sorted4.Count != 0)
                {
                    itog =sorted1.Intersect(sorted2).Intersect(sorted3).Intersect(sorted4).ToList();
                }
            }
            int q = 0;
            foreach(Product p in itog)
            {
                fill(p, q);
                q++;
            }
            sorted1.Clear();
            sorted2.Clear();
            sorted3.Clear();
            sorted4.Clear();
        }
        private void newLoad()
        {
            dataGridView1.Rows.Clear();
            var g = products.getList();
            int r = 0;
            foreach (Product p in g.Values)
            {
                fill(p, r);
                r++;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sort();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text ="";
            dateTimePicker1.Value= Convert.ToDateTime("01.01.2000");
            comboBox1.SelectedIndex = -1;
            numericUpDown1.Value =0;
            numericUpDown2.Value =0;
            int r = 0;
            foreach (Product p in list.Values)
            {
                fill(p, r);
                r++;
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int new_id = products.getMaxId()+1;
            AddOrChangeProduct form = new AddOrChangeProduct(new_id, "add", products);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Новый продукт";
            form.FormClosing += new FormClosingEventHandler(formclose);
            form.ShowDialog();
        }
        void formclose( object sender , FormClosingEventArgs e)
        {
            newLoad();
               
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrChangeProduct form = new AddOrChangeProduct(idchange, "change", products);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = $"Изменение продукта № {idchange}";
            form.FormClosing += new FormClosingEventHandler(formclose);
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int t = e.RowIndex;
            idchange =Convert.ToInt16(dataGridView1[0,t].Value);
        }
    }
}
