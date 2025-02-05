using System;


public class ExplorationManager
{
    EasyExploration easy = new EasyExploration();
    NormalExploration normal = new NormalExploration();
    HardExploration hard = new HardExploration();


    public void DisplayExplorationMenu(Player player)
    {
        Console.WriteLine("\n[탐험 중]\n");

        Console.WriteLine("1. 쉬운 탐험    |    방어력 5 이상 권장");
        Console.WriteLine("2. 일반 탐험    |    방어력 11 이상 권장");
        Console.WriteLine("3. 어려운 탐험   |    방어력 17 이상 권장");

        Console.WriteLine("\n0. 나가기");
    }

    public void HandleExplore(string input, Player player)
    {
        if (input == "1")
            easy.Explore(player);
        else if (input == "2")
            normal.Explore(player);
        else if (input == "3")
            hard.Explore(player);
        else
        {
            Console.WriteLine("\n잘못된 입력입니다.");
            Console.ReadLine();
        }
    }
}

public abstract class Exploration
{
    public abstract string Name { get; }
    public abstract int RequiredDefense { get; }
    public abstract int ClearReward { get; }

    public virtual void Explore(Player player)
    {
        Random rnd = new Random();
        int successRate = 0;
        int healthLoss = rnd.Next(20, 35);
        int totalDP = player.DefensePoint + (player.Armor?.Stat ?? 0);
        double totalAP = (player.AttackDamage + (double)(player.Weapon?.Stat ?? 0)) * 100;
        if (totalDP >= RequiredDefense)
            successRate = 100;
        else
            successRate = rnd.Next(0, 100);

        if (successRate < 40)
        {
            Console.WriteLine($"{Name}에 실패하였습니다! (필요: {RequiredDefense}, 현재: {totalDP})");
            healthLoss /= 2;
            player.Health -= healthLoss;
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("[던전 클리어]");
            Console.WriteLine("축하합니다!!");
            Console.WriteLine($"{Name}을 클리어하였습니다");


            Console.WriteLine("\n[탐험 결과]");


            Console.Write($"체력 {player.Health}  ->");
            if (successRate == 100)
                healthLoss -= (totalDP - RequiredDefense);
            player.Health -= healthLoss;
            Console.WriteLine(player.Health);
            Console.Write($"Gold {player.Gold} G ->");

            int bonusPercentage = rnd.Next((int)totalAP, (int)totalAP * 2 + 1);
            int bonusReward = (ClearReward * bonusPercentage) / 10000;
            int totalReward = ClearReward + bonusReward;
            player.Gold += totalReward;
            Console.WriteLine($"{player.Gold} G");

            player.ClearCount++;
            if (player.ClearCount == player.Level)
            {
                Console.WriteLine($"\n[레벨 업!!]\n");


                Console.Write($"Lv. {player.Level} -> Lv 0.");
                player.Level++;
                Console.WriteLine($"{player.Level}");

                Console.Write($"공격력 : {player.AttackDamage} -> ");
                player.AttackDamage += 0.5;
                Console.WriteLine($"{player.AttackDamage}");

                Console.Write($"방어력 : {player.DefensePoint} -> ");
                player.DefensePoint += 1;
                Console.WriteLine($"{player.DefensePoint}");

                player.ClearCount = 0;
            }

            Console.ReadLine();
        }
    }
}

public class EasyExploration : Exploration
{
    public override string Name => "쉬운 탐험";
    public override int RequiredDefense => 5;
    public override int ClearReward => 1000;
}

public class NormalExploration : Exploration
{
    public override string Name => "일반 탐험";
    public override int RequiredDefense => 11;
    public override int ClearReward => 1700;
}

public class HardExploration : Exploration
{
    public override string Name => "어려운 탐험";
    public override int RequiredDefense => 17;
    public override int ClearReward => 2500;
}