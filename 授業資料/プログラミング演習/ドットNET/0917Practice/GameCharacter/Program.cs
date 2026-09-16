namespace GameCharacter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player1 = new Character("ごんたろー", 100);
            player1.DisplayStatus();
            player1.Attack();

            Warrior player2 = new Warrior("じんのすけ", 145);
            player2.DisplayStatus();
            while (true) //体力がある限りは無限ループで強攻撃
            {
                player2.HyperAttack();
                if (player2.Hp <= 10) break;　//不発の条件がきたら抜ける
            }
        }
    }
}
