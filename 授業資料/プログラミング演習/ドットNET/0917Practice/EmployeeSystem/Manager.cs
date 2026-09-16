using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeSystem
{
    internal class Manager:Employee
    {
        private float bonusRate = 0.2f;  //ボーナスの倍率

        //変数bonusへのアクセサメソッド
        public void SetBonusRate(float bonusRate)
        {
            this.bonusRate = bonusRate;
        }

        //ボーナス込みでの表示にオーバーライド
        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"ボーナス：{this.bonusRate * base.GetSalary()}");
        }
    }
}
