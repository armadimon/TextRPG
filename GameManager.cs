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
    private Market market;

    public GameManager()
    {
        market = new Market();
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
                Console.WriteLine("0. 캠프 입구로 돌아가기");
                break;
            case GameState.Exploring:
                Console.WriteLine("\n[탐험 중]");
                Console.WriteLine("\n0. 캠프 입구로 돌아가기");
                break;
            case GameState.Inventory:
                player.ShowInventory();
                break;
            case GameState.EquipMode:
                player.EquipdManagement();
                break;
            case GameState.Market:
                market.DisplayMarket(player);
                break;
            case GameState.Shopping:
                market.DisplayShopping(player);
                break;
            case GameState.GameOver:
                Console.WriteLine("\n[게임 오버]");
                Console.WriteLine("0. 캠프 입구로 돌아가기");
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
                if (input == "0") ChangeState(GameState.MainMenu);
                break;
            case GameState.Exploring:
                if (input == "0") ChangeState(GameState.MainMenu);
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
                    market.BuyItem(input, player);
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
                if (input == "0")
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
