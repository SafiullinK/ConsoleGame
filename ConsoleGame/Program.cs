class Program
{
    static void Main(string[] args)
    {
        string[] dungeonEvents = { "Монстр", "Ловушка", "Сундук", "Торговец", "Пустая комната" };
        string[] dungeonMap = new string[10];
        Random rand = new Random();

        // Игрок
        int health = 100;
        int potions = 3;
        int gold = 0;
        int arrows = 5;
        string[] inventory = new string[5];
        int inventoryCount = 0;
        int poison = 1;

        // Генерация карты
        for (int i = 0; i < 9; i++)
        {
            int eventIndex = rand.Next(dungeonEvents.Length);
            dungeonMap[i] = dungeonEvents[eventIndex];
        }
        dungeonMap[9] = "Босс";

        // Игровой процесс
        for (int room = 0; room < dungeonMap.Length; room++)
        {
            Console.WriteLine($"\nВы вошли в комнату {room + 1}: {dungeonMap[room]}");
            if (health<100 && health>0 && poison >0) 
            {
                int poisonhealth = rand.Next(10, 50);
            Console.WriteLine($"\nВаше здоровье равно: {health} HP. \n Вы можете принять зелье.Согласны? \n1. Да\n2. Нет");
                string yesornotpoison = Console.ReadLine();
                switch (yesornotpoison)
                {
                    case "1":
                        health = health + poisonhealth;
                        if (health > 100)
                        {
                            health = 100; 
                        }
                        Console.WriteLine($"\nВы приняли зелье восстановив {poisonhealth} HP и теперь ваше здоровье: {health} HP");
                        break;
                    case "2":
                        Console.WriteLine($"\nВы отказались принимать зелье, ваше здоровье осталось преждним");
                        break;
                }
            }
            
            switch (dungeonMap[room])
            {
                case "Монстр":
                    FightMonster(ref health, ref potions, ref arrows); Console.ReadKey(); Console.WriteLine("Нажми пробел чтобы перейти дальше...");
                    break; 

                case "Ловушка":
                    TriggerTrap(ref health);  Console.WriteLine("Нажми пробел чтобы перейти дальше..."); Console.ReadKey();
                    break;
                case "Сундук":
                    OpenChest(ref inventory, ref inventoryCount, ref gold, ref poison); Console.WriteLine("Нажми пробел чтобы перейти дальше..."); Console.ReadKey();
                    break;
                case "Торговец":
                    Trade(ref gold, ref poison);  Console.WriteLine("Нажми пробел чтобы перейти дальше..."); Console.ReadKey();
                    break;
                case "Пустая комната":
                    Console.WriteLine("Ничего не происходит.");  Console.WriteLine("Нажми пробел чтобы перейти дальше..."); Console.ReadKey();
                    break;
                case "Босс":
                    Console.WriteLine("Вы столкнулись с Боссом! Бой начинается!");  Console.WriteLine("Нажми пробел чтобы перейти дальше..."); Console.ReadKey();
                    FightBoss(ref health);
                    break;
            }
            
            if (health <= 0)
            {
                Console.WriteLine("Вы проиграли. Игра окончена.");
                break;
            }
           
        }
        
        
    }

    static void FightMonster(ref int health, ref int potions, ref int arrows)
    {
        Random rand = new Random();
        int monsterHealth = rand.Next(20, 51);
        Console.WriteLine($"Вы встретили монстра с {monsterHealth} HP!");
        Console.WriteLine("Нажми пробел чтобы перейти дальше...");  Console.ReadKey();
        while (monsterHealth > 0)
        {
            Console.WriteLine("Выберите оружие:\n1. Меч\n2. Лук");
            string choice = Console.ReadLine();
            int damage = 0;
            
            switch (choice)
            {
                case "1":
                    damage = rand.Next(10, 21);
                    Console.WriteLine($"Вы нанесли {damage} урона мечом."); 
                    break;
                case "2":
                    if (arrows > 0)
                    {
                        damage = rand.Next(5, 16);
                        arrows--;
                        Console.WriteLine($"Вы нанесли {damage} урона луком. Осталось стрел: {arrows}."); 
                    }
                    else
                    {
                        Console.WriteLine("У вас закончились стрелы! Используйте меч."); 
                        continue;
                    }
                    break;
                default:
                    Console.WriteLine("Недопустимый выбор."); 
                    continue;
            }

            monsterHealth -= damage;
            Console.WriteLine($"Монстр остался с {monsterHealth} HP.");

            if (monsterHealth > 0)
            {
                int monsterDamage = rand.Next(5, 16);
                health -= monsterDamage;
                Console.WriteLine($"Монстр нанес вам {monsterDamage} урона. У вас осталось {health} HP."); 
            } 
        }
        Console.WriteLine("Вы победили монстра!");
    }

    static void TriggerTrap(ref int health)
    {
        Random rand = new Random();
        int trapDamage = rand.Next(10, 21);
        health -= trapDamage;
        Console.WriteLine($"Вы попали в ловушку и потеряли {trapDamage} HP. У вас осталось {health} HP.");
    }

    static void OpenChest(ref string[] inventory, ref int inventoryCount, ref int gold, ref int poison)
    {
        Random rand = new Random();
        int a = rand.Next(1, 100);
        int b = rand.Next(1, 100);
        int correctAnswer;
        int answer;



        Console.WriteLine($"Чтобы открыть сундук, решите: {a} + {b} = ?");
        correctAnswer = a + b;
        while (true)
        {
            Console.Write("Введите ответ: ");
            answer = int.Parse(Console.ReadLine());
            if (answer == correctAnswer)
            {
                Console.WriteLine("Сундук открыт!");
                int loot = rand.Next(1, 4);
                switch (loot)
                {
                    case 1:
                        if (inventoryCount < 5)
                        {
                            inventory[inventoryCount++] = "Зелье";
                            poison += 1;
                            Console.WriteLine("Вы нашли зелье!");
                            
                        }
                        else
                        {
                            Console.WriteLine("Инвентарь полон, не можете взять зелье.");
                        }
                        break;
                    case 2:
                        gold += 50;
                        Console.WriteLine("Вы нашли 50 золота!");
                        break;
                    case 3:
                        if (inventoryCount < 5)
                        {
                            inventory[inventoryCount++] = "Стрелы x5";
                            Console.WriteLine("Вы нашли 5 стрел!");
                        }
                        else
                        {
                            Console.WriteLine("Инвентарь полон, не можете взять стрелы.");
                        }
                        break;
                }
                break;
            }
            else
            {
                Console.WriteLine("Неправильный ответ. Попробуйте снова.");
            }
        }
    }

    static void Trade(ref int gold,ref int poison)
    {
        Console.WriteLine("Торговец предлагает зелье за 30 золота. У вас есть " + gold + " золота.");
        if (gold >= 30)
        {
            gold -= 30;

            if (poison < 4)
            {
                poison += 1;
                Console.WriteLine("Вы купили зелье!");
            }
            else
            {
                Console.WriteLine("Инвентарь забит зельями");
            }
            
        }
        else
        {
            Console.WriteLine("У вас недостаточно золота.");
        }
    }

    static void FightBoss(ref int health)
    {
        Random rand = new Random();
        int bossHealth = 100;
        Console.WriteLine($"Босс имеет {bossHealth} HP!");

        while (bossHealth > 0 && health > 0)
        {
            int damage = rand.Next(10, 21);
            bossHealth -= damage;
            Console.WriteLine($"Вы нанесли {damage} урона Боссу. Осталось {bossHealth} HP.");

            if (bossHealth > 0)
            {
                int bossDamage = rand.Next(10, 21);
                health -= bossDamage;
                Console.WriteLine($"Босс нанес вам {bossDamage} урона. У вас осталось {health} HP.");
            }
        }
        if (health > 0 && bossHealth <0) 
        { 
            Console.WriteLine("Вы победили Босса!");
        }
        
    }
}
