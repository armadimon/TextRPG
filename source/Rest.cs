using System;

public class Rest
{
    public void DisplayRestMenu(Player player)
    {
        Console.WriteLine("\n[�޽��ϱ�]");
        Console.Write("500 G �� ���� ü���� ȸ���� �� �ֽ��ϴ�. ");
        Console.WriteLine($"(���� ��� : {player.Gold})");

        Console.WriteLine("\n1. �޽��ϱ�");
        Console.WriteLine("\n0. ������");
    }

    public void UseRest(Player player)
    { 
        if (player.Gold >= 500)
        {
            player.Gold -= 500;
            player.Health = 100;
            Console.WriteLine("\n�޽��� �Ϸ��߽��ϴ�.");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("\nGold �� �����մϴ�..");
            Console.ReadLine();
        }
    }
}
