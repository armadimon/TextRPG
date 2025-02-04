using System;
using System.Collections.Generic;

public class Market
{
    private List<Item> marketItem;

    public Market()
    {
        marketItem = new List<Item>
        {
            new Item(1, 10, 1000, "������ ����", "������ ��ǰ���� ������ ���� ������ �����Դϴ�."),
            new Item(1, 15, 1500, "������ ���ϰ�", "������ ���ϰ��� �����Ͽ� ������ ��ȭ�� �����Դϴ�."),
            new Item(1, 30, 3000, "���콺 ����", "���ڱ����� ������ źȯ�� ��Ƴ��� ������ �����Դϴ�."),
            new Item(2, 10, 1000, "�㸧�� ���� ��", "������ ķ������ ���ϰ� �� �� �ִ� ���Դϴ�."),
            new Item(2, 15, 1500, "������ ����", "�������� ������ �����Դϴ�. ���� �������� ���� ưư�մϴ�."),
            new Item(2, 30, 3000, "������ ������ ������", "������ ķ���� ������ ������ å������ �������� �����Դϴ�.")
        };
    }
    public void DisplayMarket(Player player)
    {
        Console.WriteLine("\n[����]");
        Console.WriteLine("�ʿ��� �������� ���� �� �ִ� �����Դϴ�.");
        Console.WriteLine("\n[���� ���]");
        Console.WriteLine("\n" + player.Gold);

        Console.WriteLine("\n[������ ���]");
        for (int i = 0; i < marketItem.Count(); i++)
        {
            string status = player.HasItem(marketItem[i].Name)
                ? "���� �Ϸ�"
                : $"{marketItem[i].Value}";

            string tag = (marketItem[i].Tag == 1)
                ? "���ݷ�"
                : "����";

            Console.WriteLine($"- {i + 1} {marketItem[i].Name} | {tag} {marketItem[i].Stat:+#;-#;0} | {marketItem[i].Desc} | {status}");
        }

        Console.WriteLine("\n1. ������ ����");
        Console.WriteLine("\n0. ������");
    }

    public void DisplayShopping(Player player)
    {
        Console.WriteLine("\n[���� - ������ ����]");
        Console.WriteLine("\n[���� ���]");
        Console.WriteLine("\n" + player.Gold);


        Console.WriteLine("\n[������ ���]");
        for (int i = 0; i < marketItem.Count(); i++)
        {
            string status = player.HasItem(marketItem[i].Name)
                ? "���� �Ϸ�"
                : $"{marketItem[i].Value}";

            string tag = (marketItem[i].Tag == 1)
                ? "���ݷ�"
                : "����";

            Console.WriteLine($"- {i + 1} {marketItem[i].Name} | {tag} {marketItem[i].Stat:+#;-#;0} | {marketItem[i].Desc} | {status}");
        }
        Console.WriteLine("\n0. ������");
    }

    public void BuyItem(String input, Player player)
    {
        int ret;
        if (int.TryParse(input, out ret) && (ret <= marketItem.Count() && ret > 0))
        {
            if (!player.HasItem(marketItem[ret - 1].Name))
            {
                if (player.Gold >= marketItem[ret - 1].Value)
                {
                    player.Gold -= marketItem[ret - 1].Value;
                    player.PutItemToInventory(marketItem[ret - 1]);
                    Console.WriteLine("\n�������� �����Ͽ����ϴ�!");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("\nGold �� �����մϴ�.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("\n�̹� ������ �������Դϴ�!");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("\n�߸��� �Է��Դϴ�.");
            Console.ReadLine();
        }
    }
}