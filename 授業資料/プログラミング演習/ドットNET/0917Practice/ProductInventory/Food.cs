using System;
using System.Collections.Generic;
using System.Text;

namespace ProductInventory
{
    internal class Food : Product
    {
        private int untilExpiration; //残り消費期限日

        //コンストラクタ
        public Food() : base() { this.untilExpiration = 10; }
        public Food(string name, int price, int number,int untilExpiration) : base(name, price,number)
        {
            this.untilExpiration = untilExpiration;
        }

        //在庫リストのオーバーライド
        public override void GetStockList()
        {
            base.GetStockList();
            Console.WriteLine($"消費期限まであと{this.untilExpiration}日");
        }
    }
}
