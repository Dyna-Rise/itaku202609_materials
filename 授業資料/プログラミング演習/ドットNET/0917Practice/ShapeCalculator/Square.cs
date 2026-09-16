using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeCalculator
{
    internal class Square : Shape
    {
        private float bottom; //底辺
        private float height; //高さ

        //コンストラクタ
        public Square() : this(100, 100) { }
        public Square(float bottom, float height) : base("四角形")
        {
            this.bottom = bottom;
            if (this.bottom < 10) this.bottom = 10;
            this.height = height;
            if (this.height < 10) this.height = 10;
        }

        //面積を求めるプログラム
        public override void GetArea()
        {
            Console.WriteLine($"{base.Info}の面積は{this.bottom * this.height}");
        }
    }
}
