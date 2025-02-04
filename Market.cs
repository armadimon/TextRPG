using System;
using System.Collections.Generic;

public class Market
{
    private List<Item> marketItem;

    public Market()
    {
        marketItem = new List<Item>
        {
            new Item(1, 10, 1000, "조잡한 소총", "누군가 부품들을 조립해 만든 조잡한 소총입니다."),
            new Item(1, 15, 1500, "개조된 네일건", "공업용 네일건을 개조하여 위력을 강화한 무기입니다."),
            new Item(1, 30, 3000, "가우스 소총", "전자기장의 힘으로 탄환을 쏘아내는 강력한 무기입니다."),
            new Item(2, 10, 1000, "허름한 가죽 옷", "생존자 캠프에서 흔하게 볼 수 있는 옷입니다."),
            new Item(2, 15, 1500, "오래된 군복", "누군가의 오래된 군복입니다. 많이 낡았지만 아직 튼튼합니다."),
            new Item(2, 30, 3000, "수선된 정찰대 전투복", "생존자 캠프의 방위와 정찰을 책임지는 정찰대의 군복입니다.")
        };
    }
    public void DisplayMarket(Player player)
    {
        Console.WriteLine("\n[상점]");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine("\n[보유 골드]");
        Console.WriteLine("\n" + player.Gold);

        Console.WriteLine("\n[아이템 목록]");
        for (int i = 0; i < marketItem.Count(); i++)
        {
            string status = player.HasItem(marketItem[i].Name)
                ? "구매 완료"
                : $"{marketItem[i].Value}";

            string tag = (marketItem[i].Tag == 1)
                ? "공격력"
                : "방어력";

            Console.WriteLine($"- {i + 1} {marketItem[i].Name} | {tag} {marketItem[i].Stat:+#;-#;0} | {marketItem[i].Desc} | {status}");
        }

        Console.WriteLine("\n1. 아이템 구매");
        Console.WriteLine("\n0. 나가기");
    }

    public void DisplayShopping(Player player)
    {
        Console.WriteLine("\n[상점 - 아이템 구매]");
        Console.WriteLine("\n[보유 골드]");
        Console.WriteLine("\n" + player.Gold);


        Console.WriteLine("\n[아이템 목록]");
        for (int i = 0; i < marketItem.Count(); i++)
        {
            string status = player.HasItem(marketItem[i].Name)
                ? "구매 완료"
                : $"{marketItem[i].Value}";

            string tag = (marketItem[i].Tag == 1)
                ? "공격력"
                : "방어력";

            Console.WriteLine($"- {i + 1} {marketItem[i].Name} | {tag} {marketItem[i].Stat:+#;-#;0} | {marketItem[i].Desc} | {status}");
        }
        Console.WriteLine("\n0. 나가기");
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
                    Console.WriteLine("\n아이템을 구매하였습니다!");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("\nGold 가 부족합니다.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("\n이미 구매한 아이템입니다!");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("\n잘못된 입력입니다.");
            Console.ReadLine();
        }
    }
}