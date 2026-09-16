namespace EmployeeSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Employeeクラス
            Employee e = new Employee();
            e.SetName("かまど");
            e.SetSalary(180000);
            e.ShowInfo();

            //Managerクラス
            Manager m = new Manager();
            m.SetName("とみおか");
            m.SetSalary(220000);
            m.SetBonusRate(0.3f);
            m.ShowInfo();
        }
    }
}
