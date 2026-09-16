using System;
using System.Collections.Generic;
using System.Text;

namespace GameCharacter
{
    internal class Warrior : Character
    {
        //コンストラクタ
        public Warrior() : base() { }
        public Warrior(string name, int hp) : base(name, hp) { }

        //オーバーライド
        public override void Attack()
        {
            Console.WriteLine($"{base.Name}の強力な一撃！");
        }

        //追加メソッド
        public void HyperAttack()
        {
            Console.WriteLine($"{base.Name}の魂の一撃！");
            if (base.Hp > 10)
            {
                base.Hp-=10;
                Console.WriteLine($"体力が10減った！残り({base.Hp})");
            }
            else Console.WriteLine("不発！体力が足りない！");
        }

    }
}
