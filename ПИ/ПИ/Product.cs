using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ПИ
{
    public class Product
    {
        public int id;
        public string title;
        public string type;
        public string manufacturer;
        public DateTime dateIn;
        public decimal price;
        public int amount;
        public string edIzm;
        public int timeRealize;
        public int sales;
        public Product(int id)
        {
            this.id = id;
        }
        public Product(string q, string w, string e, string r, string t, string y, string u, string i, string o, string p)
        {
            this.id = Convert.ToInt16(q);
            this.title = w;
            this.type = e;
            this.manufacturer = r;
            this.dateIn = Convert.ToDateTime(t);
            this.price = Convert.ToDecimal(y);
            this.amount = Convert.ToInt16(u);
            this.edIzm = i;
            this.timeRealize = Convert.ToInt16(o);
            this.sales = Convert.ToInt16(p);
        }
    }
    public class  Products
    {
        public  Dictionary<int,Product> allProduct;
        public Products()
        {
            allProduct = new Dictionary<int, Product>();
            allProduct = readProduct();
        }
        private Dictionary<int, Product> readProduct()
        {
            Dictionary<int, Product> spisok = new Dictionary<int, Product>();
            foreach(string line in File.ReadLines("base.txt", Encoding.UTF8))
            {
                string[] p = line.Split(new string[] { ";" }, StringSplitOptions.None);
                Product pr = new Product(p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7], p[8], p[9]);
                spisok.Add(pr.id, pr);
            }
            return spisok;
        }
        public void addProduct(Product a)
        {
            allProduct.Add(a.id, a);
        }
        public void delProduct(Product a)
        {
            allProduct.Remove(a.id);
        }
        public void saveProduct()
        {
            StreamWriter f = new StreamWriter("base.txt", false, Encoding.UTF8);
            foreach (Product p in allProduct.Values)
            {
                f.WriteLine($"{p.id};{p.title};{p.type};{p.manufacturer};{p.dateIn.ToShortDateString()};{p.price};{p.amount};{p.edIzm};{p.timeRealize};{p.sales}");
            }
            f.Close();
        }
        public int getMaxId()
        {
            int max = 0;
            foreach(Product p in allProduct.Values)
            {
                if (max < p.id) max = p.id;
            }
            return max;
        }
        public Dictionary<int, Product> getList()
        {
            return allProduct;
        }

    }
}
