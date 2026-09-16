using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeCalculator
{
    internal class Triangle : Shape
    {
        protected float bottom; //底辺
        protected float height; //高さ

        //コンストラクタ
        public Triangle() : this(100, 100) { }
        public Triangle(float bottom, float height) : base("三角形")
        {
            this.bottom = bottom;
            if (this.bottom < 10) this.bottom = 10;
            this.height = height;
            if (this.height < 10) this.height = 10;
        }

        //面積を求めるプログラム
        public override void GetArea()
        {
            Console.WriteLine($"{base.Info}の面積は{this.bottom * this.height / 2}");
        }
    }
}
