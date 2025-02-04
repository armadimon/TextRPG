using System;
using System.IO;
using System.Text.Json;

class TextRPG
{
    private static void Main()
    {
        Title title = new Title();
        GameManager gameManager = new GameManager();

        gameManager.Run();
    }
}

class Title
{


    private Player CreateNewPlayer()
    {
        Console.Write("이름을 입력하세요 >> ");
        string? name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            name = "Default";
        }

        Player player = new Player(name);
        return player;
    }

    public Player NewPlayerData()
    {
        Console.WriteLine("생존자 캠프에 오신걸 환영합니다. 이곳은 핵전쟁 이후 남겨진 몇 안되는 마을 중 하나입니다.\n먼저 당신의 이름을 알려주세요.");
        return CreateNewPlayer();
    }

    public Player LoadPlayerData()
    {
        string jsonData;
        Player player;
        if (File.Exists("save.json"))
        {
            jsonData = File.ReadAllText("save.json");
            try
            {
                player = JsonSerializer.Deserialize<Player>(jsonData) ?? throw new Exception("Deserialize의 결과가 null입니다.");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"저장된 데이터가 손상되었습니다. 새로운 캐릭터를 생성합니다.");
                Console.WriteLine($"에러 : {exception.Message}\n");
                player = CreateNewPlayer();
            }
        }
        else
        {
            Console.WriteLine("저장된 파일이 비어 있습니다. 새로운 캐릭터를 생성합니다.");
            player = CreateNewPlayer();
        }
        return player;
    }

    public void DisplayTitle()
    {
        Console.WriteLine("1. 새 게임");
        Console.WriteLine("2. 불러오기");
        Console.WriteLine("3. 게임 종료");
    }
}
