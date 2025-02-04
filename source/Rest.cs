using System;

public class Rest
{
    public void DisplayRestMenu(Player player)
    {
        Console.WriteLine("\n[휴식하기]");
        Console.Write("500 G 를 내면 체력을 회복할 수 있습니다. ");
        Console.WriteLine($"(보유 골드 : {player.Gold})");

        Console.WriteLine("\n1. 휴식하기");
        Console.WriteLine("\n0. 나가기");
    }

    public void UseRest(Player player)
    { 
        if (player.Gold >= 500)
        {
            player.Gold -= 500;
            player.Health = 100;
            Console.WriteLine("\n휴식을 완료했습니다.");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("\nGold 가 부족합니다..");
            Console.ReadLine();
        }
    }
}
