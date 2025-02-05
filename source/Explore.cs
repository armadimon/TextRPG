using System;


public class ExplorationManager
{
    EasyExploration easy = new EasyExploration();
    NormalExploration normal = new NormalExploration();
    HardExploration hard = new HardExploration();


    public void DisplayExplorationMenu(Player player)
    {
        Console.WriteLine("\n[Ž�� ��]\n");

        Console.WriteLine("1. ���� Ž��    |    ���� 5 �̻� ����");
        Console.WriteLine("2. �Ϲ� Ž��    |    ���� 11 �̻� ����");
        Console.WriteLine("3. ����� Ž��   |    ���� 17 �̻� ����");

        Console.WriteLine("\n0. ������");
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
            Console.WriteLine("\n�߸��� �Է��Դϴ�.");
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
            Console.WriteLine($"{Name}�� �����Ͽ����ϴ�! (�ʿ�: {RequiredDefense}, ����: {totalDP})");
            healthLoss /= 2;
            player.Health -= healthLoss;
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("[���� Ŭ����]");
            Console.WriteLine("�����մϴ�!!");
            Console.WriteLine($"{Name}�� Ŭ�����Ͽ����ϴ�");


            Console.WriteLine("\n[Ž�� ���]");


            Console.Write($"ü�� {player.Health}  ->");
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
                Console.WriteLine($"\n[���� ��!!]\n");


                Console.Write($"Lv. {player.Level} -> Lv 0.");
                player.Level++;
                Console.WriteLine($"{player.Level}");

                Console.Write($"���ݷ� : {player.AttackDamage} -> ");
                player.AttackDamage += 0.5;
                Console.WriteLine($"{player.AttackDamage}");

                Console.Write($"���� : {player.DefensePoint} -> ");
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
    public override string Name => "���� Ž��";
    public override int RequiredDefense => 5;
    public override int ClearReward => 1000;
}

public class NormalExploration : Exploration
{
    public override string Name => "�Ϲ� Ž��";
    public override int RequiredDefense => 11;
    public override int ClearReward => 1700;
}

public class HardExploration : Exploration
{
    public override string Name => "����� Ž��";
    public override int RequiredDefense => 17;
    public override int ClearReward => 2500;
}