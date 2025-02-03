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
        marketItem.Add(new Item(1, 10, 1000, "������ ����", "������ ��ǰ���� ������ ���� ������ �����Դϴ�."));
        marketItem.Add(new Item(1, 15, 1500, "������ ���ϰ�", "������ ���ϰ��� �����Ͽ� ������ ��ȭ�� �����Դϴ�."));
        marketItem.Add(new Item(1, 30, 3000, "���콺 ����", "���ڱ����� ������ źȯ�� ��Ƴ��� ������ �����Դϴ�."));
        marketItem.Add(new Item(2, 10, 1000, "�㸧�� ���� ��", "������ ķ������ ���ϰ� �� �� �ִ� ���Դϴ�."));
        marketItem.Add(new Item(2, 15, 1500, "������ ����", "�������� ������ �����Դϴ�. ���� �������� ���� ưư�մϴ�."));
        marketItem.Add(new Item(2, 30, 3000, "������ ������ ������", "������ ķ���� ������ ������ å������ �������� �����Դϴ�."));
    }

    private void OpenMarket(Player player)
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

    private void ShoppingMode(Player player)
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
            Console.WriteLine("\nGold �� �����մϴ�.");
            Console.ReadLine();
        }
    }

    public void Run()
    {
        Console.WriteLine("������ ķ���� ���Ű� ȯ���մϴ�. �̰��� ������ ���� ������ �� �ȵǴ� ���� �� �ϳ��Դϴ�.\n���� ����� �̸��� �˷��ּ���.");
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
        Console.WriteLine($"���� ����: {currentState}");

        switch (currentState)
        {
            case GameState.MainMenu:
                Console.WriteLine("\n[ķ�� �Ա�]");
                Console.WriteLine("\n1. ���� ����");
                Console.WriteLine("2. �κ��丮");
                Console.WriteLine("3. ����");
                Console.WriteLine("4. Ž�� ���");
                Console.WriteLine("5. �޽��ϱ�");
                Console.WriteLine("0. ����");
                break;
            case GameState.StatusMenu:
                player.ShowStatus();
                Console.WriteLine("1. ķ�� �Ա��� ���ư���");
                break;
            case GameState.Exploring:
                Console.WriteLine("\n[Ž�� ��]");
                Console.WriteLine("\n1. ķ�� �Ա��� ���ư���");
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
                Console.WriteLine("\n[���� ����]");
                Console.WriteLine("1. ķ�� �Ա��� ���ư���");
                break;
        }
    }

    private void HandleInput(Player player)
    {
        Console.WriteLine("\n���Ͻô� �ൿ�� �Է����ּ���. ");
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
        Console.WriteLine("������ �����մϴ�.");
        isRunning = false;
    }
}
