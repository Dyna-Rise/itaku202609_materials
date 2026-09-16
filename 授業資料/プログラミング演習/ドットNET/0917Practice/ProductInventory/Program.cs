namespace ProductInventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Food[] foods =
            {
                new Food("ざるそば",500,5,3),
                new Food("ざるうどん",480,3,3),
                new Food("天丼",680,3,2),
                new Food("からあげ弁当",580,2,1),
                new Food("ハンバーグ弁当",480,1,1),
                new Food("幕ノ内弁当",680,3,2)
            };

            for(int i = 0; i < foods.Length; i++)
            {
                foods[i].GetStockList();
            }
        }
    }
}
