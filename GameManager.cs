using System;

public enum GameState
{
    MainMenu,
    StatusMenu,
    Exploring,
    Inventory,
    EquipMode,
    Market,
    Shopping,
    Rest,
    GameOver
}


public class GameManager
{
    private GameState currentState = GameState.MainMenu;
    private bool isRunning = true;
    private List<Item> marketItem;

    public GameManager()
    {
        marketItem = new List<Item>();
        marketItem.Add(new Item(1, 10, 1000, "조잡한 소총", "누군가 부품들을 조립해 만든 조잡한 소총입니다."));
        marketItem.Add(new Item(1, 15, 1500, "개조된 네일건", "공업용 네일건을 개조하여 위력을 강화한 무기입니다."));
        marketItem.Add(new Item(1, 30, 3000, "가우스 소총", "전자기장의 힘으로 탄환을 쏘아내는 강력한 무기입니다."));
        marketItem.Add(new Item(2, 10, 1000, "허름한 가죽 옷", "생존자 캠프에서 흔하게 볼 수 있는 옷입니다."));
        marketItem.Add(new Item(2, 15, 1500, "오래된 군복", "누군가의 오래된 군복입니다. 많이 낡았지만 아직 튼튼합니다."));
        marketItem.Add(new Item(2, 30, 3000, "수선된 정찰대 전투복", "생존자 캠프의 방위와 정찰을 책임지는 정찰대의 군복입니다."));
    }

    private void OpenMarket(Player player)
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

    private void ShoppingMode(Player player)
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

    private void BuyItem(String input, Player player)
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
            Console.WriteLine("\nGold 가 부족합니다.");
            Console.ReadLine();
        }
    }

    public void Run()
    {
        Console.WriteLine("생존자 캠프에 오신걸 환영합니다. 이곳은 핵전쟁 이후 남겨진 몇 안되는 마을 중 하나입니다.\n먼저 당신의 이름을 알려주세요.");
        Player player = new Player(Console.ReadLine());

        while (isRunning)
        {
            DisplayMenu(player);
            HandleInput(player);
        }
    }

    private void DisplayMenu(Player player)
    {
        Console.Clear();
        Console.WriteLine($"현재 상태: {currentState}");

        switch (currentState)
        {
            case GameState.MainMenu:
                Console.WriteLine("\n[캠프 입구]");
                Console.WriteLine("\n1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 탐사 출발");
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("0. 종료");
                break;
            case GameState.StatusMenu:
                player.ShowStatus();
                Console.WriteLine("1. 캠프 입구로 돌아가기");
                break;
            case GameState.Exploring:
                Console.WriteLine("\n[탐험 중]");
                Console.WriteLine("\n1. 캠프 입구로 돌아가기");
                break;
            case GameState.Inventory:
                player.ShowInventory();
                break;
            case GameState.EquipMode:
                player.EquipdManagement();
                break;
            case GameState.Market:
                OpenMarket(player);
                break;
            case GameState.Shopping:
                ShoppingMode(player);
                break;
            case GameState.GameOver:
                Console.WriteLine("\n[게임 오버]");
                Console.WriteLine("1. 캠프 입구로 돌아가기");
                break;
        }
    }

    private void HandleInput(Player player)
    {
        Console.WriteLine("\n원하시는 행동을 입력해주세요. ");
        Console.Write(">> ");
        string input = Console.ReadLine();

        switch (currentState)
        {
            case GameState.MainMenu:
                if (input == "1") ChangeState(GameState.StatusMenu);
                else if (input == "2") ChangeState(GameState.Inventory);
                else if (input == "3") ChangeState(GameState.Market);
                else if (input == "4") ChangeState(GameState.Exploring);
                else if (input == "5") ChangeState(GameState.Rest);
                else if (input == "0") QuitGame();
                break;
            case GameState.StatusMenu:
                if (input == "1") ChangeState(GameState.MainMenu);
                break;
            case GameState.Exploring:
                if (input == "1") ChangeState(GameState.MainMenu);
                break;
            case GameState.Market:
                if (input == "1")
                    ChangeState(GameState.Shopping);
                else if (input == "0")
                    ChangeState(GameState.MainMenu);
                break;
            case GameState.Shopping:
                if (input == "0")
                    ChangeState(GameState.Market);
                else
                    BuyItem(input, player);
                break;
            case GameState.Inventory:
                if (input == "1")
                    ChangeState(GameState.EquipMode);
                else if (input == "0")
                    ChangeState(GameState.MainMenu);
                break;
            case GameState.EquipMode:
                if (input == "0")
                    ChangeState(GameState.Inventory);
                else
                    player.Equip(input);
                break;
            case GameState.GameOver:
                if (input == "1")
                    ChangeState(GameState.MainMenu);
                break;
        }
    }

    private void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    private void QuitGame()
    {
        Console.WriteLine("게임을 종료합니다.");
        isRunning = false;
    }
}
