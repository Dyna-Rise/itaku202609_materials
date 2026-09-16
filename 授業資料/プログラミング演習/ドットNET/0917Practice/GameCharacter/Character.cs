using System;
using System.Collections.Generic;
using System.Text;

namespace GameCharacter
{
    internal class Character
    {
        private string name;　//名前
        private int hp; //体力

        //コンストラクタ
        public Character() : this("名無し", 100) { }
        public Character(string name,int hp)
        {
            this.name = name;
            this.hp = hp;
        }
        
        //プロパティ
        public string Name
        {
            set
            {
                if (value.Length > 8) this.name = "名無し"; //8文字より多ければ"名無し"
                else this.name = value; //8文字以下ならセット可能
            }
            get { return this.name; }
        }
        public int Hp
        {
            set
            {
                this.hp = value;
                if (this.hp <= 0) this.hp = 1; //0以下なら1にセッティング
            }
            get { return this.hp; }
        }

        //攻撃メソッド
        public virtual void Attack()
        {
            Console.WriteLine($"{this.name}は攻撃した！");
        } 

        //ステータス表示
        public void DisplayStatus()
        {
            Console.WriteLine($"名前：{this.name}");
            Console.WriteLine($"体力：{this.hp}");
        }
    }
}
