using System;
using System.Collections.Generic;
using System.Text;

namespace ProductInventory
{
    internal class Product
    {
        private string name; //商品名
        private int price; //値段
        private int number; //在庫数

        //コンストラクタ
        public Product() : this("未登録", 100,0) { }
        public Product(string name,int price,int number)
        {
            this.name = name;
            this.price = price;
            this.number = number;            
        }

        //プロパティ
        public string Name
        { 
            set { this.name = value; }
            get { return this.name; }
        }
        public int Price
        {
            set
            {
                this.price = value;
                if (this.price < 0) this.price = 0;
            }
            get { return this.price; }
        }
        public int Number
        {
            set
            {
                this.number = value;
                if (this.number < 0) this.number = 0;
            }
            get { return this.number; }

        }

        //在庫リスト
        public virtual void GetStockList()
        {
            Console.WriteLine($"商品名：{this.name}、値段{this.price}円、在庫数{this.number}個");
        }
    }
}
