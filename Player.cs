using System;

public class Player
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int AttackDamage { get; set; }
    public int DeffencePoint { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    public int Level { get; set; }
    public Item? Weapon { get; set; }
    public Item? Armor { get; set; }
    private List<Item> inventory;

    public Player(string name)
    {
        Name = name;
        Level = 1;
        Description = "용병";
        AttackDamage = 10;
        DeffencePoint = 5;
        Health = 100;
        Gold = 15000;
        Weapon = null;
        Armor = null;
        inventory = new List<Item>();
    }

    public bool HasItem(string itemName)
    {
        for (int i = 0; i < inventory.Count(); i++)
        {
            if (inventory[i].Name == itemName)
            {
                return (true);
            }
        }
        return (false);
    }


    public bool IsEquipped(string itemName)
    {
        for (int i = 0; i < inventory.Count(); i++)
        {
            if (inventory[i].Name == itemName)
            {
                return (true);
            }
        }
        return (false);
    }

    public void ShowInventory()
    {
        Console.WriteLine("\n[인벤토리]");

        Console.WriteLine("\n[아이템 목록]");
        for (int i = 0; i < inventory.Count(); i++)
        {
            string equip = "";
            if (Weapon != null)
            {
                equip = (inventory[i].Name == Weapon.Name)
                ? "[E]"
                : "";
            }
            if (Armor != null)
            {
                equip = (inventory[i].Name == Armor.Name)
                ? "[E]"
                : "";
            }
            string tag = (inventory[i].Tag == 1)
                ? "공격력"
                : "방어력";

            string sign = inventory[i].Stat >= 0
                ? $"+"
                : $"";
            Console.WriteLine($"- {equip} {i + 1} {inventory[i].Name} | {tag} {sign}{inventory[i].Stat} | {inventory[i].Desc}");
        }
        Console.WriteLine("\n1. 장착 관리");
        Console.WriteLine("\n0. 나가기");
    }

    public void Equip(string input)
    {
        int ret;

        if (int.TryParse(input, out ret) && (ret <= inventory.Count() && ret > 0))
        {
            if (inventory[ret - 1].Tag == 1)
            {
                Weapon = inventory[ret - 1];
            }
            else
            {
                Armor = inventory[ret - 1];
            }
        }
        else
        {
            Console.WriteLine("\nGold 가 부족합니다. (아무키나 눌러 계속하세요.)");
            Console.ReadLine();
        }
    }

    public void EquipdManagement()
    {

        Console.WriteLine("\n[인벤토리] - 장착 관리");


        Console.WriteLine("\n[아이템 목록]");
        for (int i = 0; i < inventory.Count(); i++)
        {
            string equip = "";
            if (Weapon != null)
            {
                equip = (inventory[i].Name == Weapon.Name)
                ? "[E]"
                : "";
            }
            if (Armor != null)
            {
                equip = (inventory[i].Name == Armor.Name)
                ? "[E]"
                : "";
            }
            string tag = (inventory[i].Tag == 1)
                ? "공격력"
                : "방어력";

            string sign = inventory[i].Stat >= 0
                ? $"+"
                : $"";
            Console.WriteLine($"- {equip} {i + 1} {inventory[i].Name} | {tag} {sign}{inventory[i].Stat} | {inventory[i].Desc}");
        }
        Console.WriteLine("\n0. 나가기");
    }

    public void PutItemToInventory(Item newItem)
    {
        inventory.Add(newItem);
    }

    public void ShowStatus()
    {
        //string apSign = inventory[i].Stat >= 0
        //    ? $"+"
        //    : $"";
        //string dpSign = Weapon.Stat >= 0
        //    ? $"+"
        //    : $"";

        int ap = Weapon != null
            ? Weapon.Stat
            : 0;
        int dp = Armor != null
            ? Armor.Stat
            : 0;
        Console.WriteLine($"상태 보기");
        Console.WriteLine($"캐릭터의 정보가 표시됩니다.\n");

        Console.WriteLine($"Lv . {Level:N2}");
        Console.WriteLine($"{Name} ({Description})");
        Console.WriteLine($"공격력 : {AttackDamage} ({ap})");
        Console.WriteLine($"방어력 : {DeffencePoint} ({dp})");
        Console.WriteLine($"체력 : {Health}");
        Console.WriteLine($"Gold : {Gold} G");
    }
}
