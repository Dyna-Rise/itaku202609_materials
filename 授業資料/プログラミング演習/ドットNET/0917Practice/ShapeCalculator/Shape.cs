using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeCalculator
{
    internal class Shape
    {
        private string info; //図形の種類

        //コンストラクタ
        public Shape() : this("無形") { }
        public Shape(string info) { this.info = info; }

        //変数infoに対するプロパティ
        public string Info
        {
            set { this.info = value; }
            get { return this.info; }
        }

        //面積を求める
        public virtual void GetArea()
        {
            Console.WriteLine($"{this.info}の面積は なし");
        }
    }
}
