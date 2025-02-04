using System;
using System.Text.Json.Serialization;

public class Player
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int AttackDamage { get; set; }
    public int DefensePoint { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    public int Level { get; set; }
    public Item? Weapon { get; set; }
    public Item? Armor { get; set; }

    [JsonInclude]
    public List<Item> inventory;

    public Player(string name)
    {
        Name = name;
        Level = 1;
        Description = "�뺴";
        AttackDamage = 10;
        DefensePoint = 5;
        Health = 100;
        Gold = 1500;
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

    public void ShowInventory()
    {
        Console.WriteLine("\n[�κ��丮]");

        Console.WriteLine("\n[������ ���]");
        for (int i = 0; i < inventory.Count(); i++)
        {
            string equip = "";
            if (Weapon != null && (inventory[i].Name == Weapon.Name))
            {
                equip = "[E]";
            }
            else if (Armor != null && (inventory[i].Name == Armor.Name))
            {
                equip = "[E]";
            }
            string tag = (inventory[i].Tag == 1)
                ? "���ݷ�"
                : "����";

            Console.WriteLine($"- {equip} {i + 1}. {inventory[i].Name} | {tag} {inventory[i].Stat:+#;-#;0} | {inventory[i].Desc}");
        }
        Console.WriteLine("\n1. ���� ����");
        Console.WriteLine("\n0. ������");
    }

    public void EquipdManagement()
    {

        Console.WriteLine("\n[�κ��丮] - ���� ����");


        Console.WriteLine("\n[������ ���]");
        for (int i = 0; i < inventory.Count(); i++)
        {
            string equip = "";
            if (Weapon != null && (inventory[i].Name == Weapon.Name))
            {
                equip = "[E]";
            }
            else if (Armor != null && (inventory[i].Name == Armor.Name))
            {
                equip = "[E]";
            }
            string tag = (inventory[i].Tag == 1)
                ? "���ݷ�"
                : "����";
            Console.WriteLine($"- {equip} {i + 1}. {inventory[i].Name} | {tag} {inventory[i].Stat:+#;-#;0} | {inventory[i].Desc}");
        }
        Console.WriteLine("\n0. ������");
    }

    public void Equip(string input)
    {
        int ret;

        if (int.TryParse(input, out ret) && (ret <= inventory.Count() && ret > 0))
        {
            if (inventory[ret - 1].Tag == 1)
            {
                if (Weapon == null)
                    Weapon = inventory[ret - 1];
                else
                {
                    if (Weapon.Name == inventory[ret - 1].Name)
                        Weapon = null;
                    else
                        Weapon = inventory[ret - 1];
                }
            }
            else
            {
                if (Armor == null)
                    Armor = inventory[ret - 1];
                else
                {
                    if (Armor.Name == inventory[ret - 1].Name)
                        Armor = null;
                    else
                        Armor = inventory[ret - 1];
                }
            }
        }
        else
        {
            Console.WriteLine("\n�߸��� �Է��Դϴ�.");
            Console.ReadLine();
        }
    }

    public void PutItemToInventory(Item newItem)
    {
        if (newItem != null)
        {
            inventory.Add(newItem);
        }
        else
        {
            Console.WriteLine("\n�߰� ������ �������� �����ϴ�!");
            Console.ReadLine();
        }
    }

    public void RemoveItemFromInventory(Item newItem)
    {
        Item? targetItem = inventory.Find(item => item.Name == newItem.Name);
        if (targetItem != null)
        {
            inventory.Remove(targetItem);
        }
        else
        {
            Console.WriteLine("\n�� �� �ִ� �������� �����ϴ�!");
            Console.ReadLine();
        }
    }

    public void ShowStatus()
    {

        string ap = Weapon != null
            ? $"{Weapon.Stat:+#;-#;0}"
            : "0";

        string dp = Armor != null
            ? $"{Armor.Stat:+#;-#;0}"
            : "0";

        Console.WriteLine($"���� ����");
        Console.WriteLine($"ĳ������ ������ ǥ�õ˴ϴ�.\n");

        Console.WriteLine($"Lv . {Level:D2}");
        Console.WriteLine($"{Name} ({Description})");
        Console.WriteLine($"���ݷ� : {AttackDamage} ({ap})");
        Console.WriteLine($"���� : {DefensePoint} ({dp})");
        Console.WriteLine($"ü�� : {Health}");
        Console.WriteLine($"Gold : {Gold} G");
    }
}
