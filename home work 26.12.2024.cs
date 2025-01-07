using System;
using System.Text;
using System.Threading;
using GAME;

class Program
{
    static void Main()
    {
        Assassin assassin = new Assassin("Assassin", 100, 50, 10, Race.Human);
        Samurai samurai = new Samurai("Samurai", 200, 40, 20, Race.Human);
        Ninja ninja = new Ninja("Ninja", 100, 30, 5, Race.Human);
        Vampyre vampyre = new Vampyre("Vampyre", 100, 35, 15, Race.Human);

        Character target1 = new Character("Target", 2000, 20, 10, Race.Ork);

        Console.WriteLine("Battle Begins!\n");

        Console.WriteLine($"Target's Health: {target1.Health}");
        Console.WriteLine($"Assassin's Health: {assassin.Health}");
        Console.WriteLine($"Samurai's Health: {samurai.Health}");
        Console.WriteLine($"Ninja's Health: {ninja.Health}");
        Console.WriteLine($"Vampyre's Health: {vampyre.Health}");
        Console.WriteLine();

        while (assassin.isAlive() && target1.isAlive())
        {
            assassin.attack(target1);
            Console.WriteLine($"Target's Health: {target1.Health}");
            target1.attack(assassin);
            Console.WriteLine($"Assassin's Health: {assassin.Health}");
            Console.WriteLine();
        }
        Character target2 = new Character("Target", 400, 20, 10, Race.Ork);

        while (samurai.isAlive() && target2.isAlive())
        {
            samurai.attack(target2);
            Console.WriteLine($"Target's Health: {target2.Health}");
            target2.attack(samurai);
            Console.WriteLine($"Samurai's Health: {samurai.Health}");
            Console.WriteLine();
        }
        Character target3 = new Character("Target", 100, 20, 10, Race.Ork);
        
        while (ninja.isAlive() && target3.isAlive())
        {
            ninja.attack(target3);
            Console.WriteLine($"Target's Health: {target3.Health}");
            target3.attack(ninja);
            Console.WriteLine($"Ninja's Health: {ninja.Health}");
            Console.WriteLine();
        }
        Character target4 = new Character("Target", 100, 20, 10, Race.Ork);
        
        while (vampyre.isAlive() && target4.isAlive())
        {
            vampyre.attack(target4);
            Console.WriteLine($"Target's Health: {target4.Health}");
            target4.attack(vampyre);
            Console.WriteLine($"Vampyre's Health: {vampyre.Health}");
            Console.WriteLine();
        }
    }
}



//drugoe


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace GAME
{
    enum Race
    {
        Human,
        Ork,
        Elf,
        Dwarf
    }

    class Character
    {
        protected string? name;
        protected int health;
        protected int damage;
        protected int defence;
        protected Race race;

        public string? Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = Math.Max(value, 0); }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int Defence
        {
            get { return defence; }
            set { defence = value; }
        }
        public Race Race
        {
            get { return race; }
            set { race = value; }
        }
        private double GetDamageRace(Race attacker, Race defender)
        {
            if (attacker == Race.Human && defender == Race.Elf) return 1.2;
            if (attacker == Race.Elf && defender == Race.Human) return 0.8;
            if (attacker == Race.Elf && defender == Race.Ork) return 1.5;
            if (attacker == Race.Ork && defender == Race.Elf) return 0.7;
            if (attacker == Race.Ork && defender == Race.Dwarf) return 1.3;
            if (attacker == Race.Dwarf && defender == Race.Ork) return 0.9;
            if (attacker == Race.Dwarf && defender == Race.Human) return 1.1;
            if (attacker == Race.Human && defender == Race.Dwarf) return 0.9;
            return 1.0;
        }
        public Character() : this("Jonny", 100, 5, 0, Race.Human) { }
        public Character(string? name, int health, int damage, int defence, Race race)
        {
            this.name = name;
            Health = health;
            this.damage = damage;
            this.defence = defence;
            this.race = race;
        }
        public void print()
        {
            Console.WriteLine($"-< {name} >-");
            Console.WriteLine($" Здоров\'я: {health}");
            Console.WriteLine($" Шкода: {damage}");
            Console.WriteLine($" Захист: {defence}");
            Console.WriteLine($" Раса: {race}");
        }

        public virtual int takeDamage(int damage)
        {
            health = Math.Max(health - damage, 0);
            return health;
        }

        public virtual int attack(Character target)
        {
            int new_damage = (int)(GetDamageRace(this.race, target.race) * damage);
            return target.takeDamage(new_damage);
        }

        public bool isAlive()
        {
            return health > 0;
        }
        public double raceAttackBonus(Race targetRace)
        {
            if (targetRace == this.race) return 1;
            else if (targetRace == Race.Ork) return 0.9;
            else return 1.1;
        }
    }

    class Berserk : Character
    {
        bool odin = true;
        public Berserk(string? name, int health, int damage, int defence, Race race = Race.Human) : base(name, health, damage, defence, race) { }
        public Berserk() : this("Jonny", 100, 5, 0, Race.Human) { }
        public override int takeDamage(int damage)
        {
            health = Math.Max(health - damage, 0);
            if (this.health <= 0)
            {
                if (odin)
                {
                    this.health += 1;
                    odin = false;
                }
            }
            return health;
        }
        public new int attack(Character target)
        {
            int final_damage = (int)(this.damage * this.raceAttackBonus(target.Race));
            final_damage = this.health < 50 ? (int)(final_damage * 1.5) : final_damage;
            return target.takeDamage(final_damage);
            // return target.takeDamage(final_damage);
        }

    }

    class Assassin : Character
    {
        Random rnd = new Random();

        public Assassin(string? name, int health, int damage, int defence, Race race = Race.Human) : base(name, health, damage, defence, race) { }

        public Assassin() : this("Jonny", 100, 5, 0, Race.Human) { }

        public override int attack(Character target)
        {
            int finalDamage = Math.Max(0, Damage - target.Defence);
            if (rnd.Next(0, 100) < 75)
            {
                Console.WriteLine($"{this.Name} performs a deadly strike!");
                finalDamage = 1000;
            }
            return target.takeDamage(finalDamage);
        }
    }

    class Samurai : Character
    {
        int comboHits = 0;

        public Samurai(string? name, int health, int damage, int defence, Race race = Race.Human) : base(name, health, damage, defence, race) { }

        public Samurai() : this("Jonny", 100, 5, 0, Race.Human) { }

        public override int attack(Character target)
        {
            comboHits++;
            if (comboHits > 5) comboHits = 0;
            double damageMultiplier = 1 + (0.1 * comboHits);
            int finalDamage = (int)(this.Damage * damageMultiplier) - target.Defence;
            finalDamage = Math.Max(0, finalDamage);
            Console.WriteLine($"{this.Name} attacks with {comboHits * 10}% extra damage!");
            return target.takeDamage(finalDamage);
        }
    }


    class Ninja : Character
    {
        Random rnd = new Random();

        public Ninja(string? name, int health, int damage, int defence, Race race = Race.Human) : base(name, health, damage, defence, race) { }

        public Ninja() : this("Jonny", 100, 5, 0, Race.Human) { }

        public override int takeDamage(int damage)
        {
            if (rnd.Next(0, 100) < 25)
            {
                Console.WriteLine($"{this.Name} dodged the attack!");
                return this.health;
            }
            return this.takeDamage(damage);
        }
    }

    class Vampyre : Character
    {
        public Vampyre(string? name, int health, int damage, int defence, Race race = Race.Human) : base(name, health, damage, defence, race) { }

        public Vampyre() : this("Jonny", 100, 5, 0, Race.Human) { }

        public override int attack(Character target)
        {
            int damageDealt = Math.Max(0, this.Damage - target.Defence);
            int healthRestored = (int)(damageDealt * 0.1);
            this.Health += healthRestored;
            Console.WriteLine($"{this.Name} attacks {target.Name} for {damageDealt} damage and restores {healthRestored} health.");
            return target.takeDamage(damageDealt);
        }
    }
}
