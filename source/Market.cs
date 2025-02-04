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
        Console.WriteLine(player.Gold + "G");

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
        Console.WriteLine("\n2. ������ �Ǹ�");
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

    public void DisplaySellMenu(Player player)
    {
        Console.WriteLine("\n[���� - ������ �Ǹ�]");
        Console.WriteLine("������ �������� �� �� �ִ� �����Դϴ�.");
        Console.WriteLine("\n[���� ���]");
        Console.WriteLine(player.Gold + "G");


        Console.WriteLine("\n[������ ���]");
        for (int i = 0; i < player.inventory.Count(); i++)
        {
            string equip = "";
            if (player.Weapon != null && (player.inventory[i].Name == player.Weapon.Name))
            {
                equip = "[E]";
            }
            else if (player.Armor != null && (player.inventory[i].Name == player.Armor.Name))
            {
                equip = "[E]";
            }
            string tag = (player.inventory[i].Tag == 1)
                ? "���ݷ�"
                : "����";

            Console.WriteLine($"- {equip} {i + 1}. {player.inventory[i].Name} | {tag} {player.inventory[i].Stat:+#;-#;0} | {player.inventory[i].Desc}");
        }
        Console.WriteLine("\n0. ������");
    }

    public void SellItem(String input, Player player)
    {
        int ret;
        if (int.TryParse(input, out ret) && (ret <= player.inventory.Count() && ret > 0))
        {
            int sellValue = (player.inventory[ret - 1].Value * 85) / 100;
            player.Gold += sellValue;
            if (player.inventory[ret - 1].Name == player.Weapon?.Name)
                player.Weapon = null;
            else if (player.inventory[ret - 1].Name == player.Armor?.Name)
                player.Armor = null;
            player.RemoveItemFromInventory(player.inventory[ret - 1]);
            Console.WriteLine("\n�������� �Ǹ��Ͽ����ϴ�!");

            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("\n�߸��� �Է��Դϴ�.");
            Console.ReadLine();
        }
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