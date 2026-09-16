using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeSystem
{
    internal class Employee
    {
        private string name; //従業員名
        private int salary; //基本給

        //変数nameへのアクセサメソッド
        public void SetName(string name)
        {
            this.name = name;
        }
        //変数salaryへのアクセサメソッド
        public void SetSalary(int salary)
        {
            this.salary = salary;
            if (this.salary < 200000) this.salary = 2000000; //最低でも20万円
        }
        public int GetSalary() { return this.salary; }

        //ステータス表示
        public virtual void ShowInfo()
        {
            Console.WriteLine($"名前：{this.name}");
            Console.WriteLine($"給与：{this.salary}");
        }
    }
}
