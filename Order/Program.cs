using System;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace Order
{
    interface ITypes
    {
        void see();
        void sell();
    }

    class Ebooks : ITypes
    {
        string? name;
        string? author;
        double? price;

        public Ebooks(string name, string author, double price)
        {
            this.name = name;
            this.author = author;
            this.price = price;
        }

        public void see()
        {
            Console.WriteLine(name + "\t" + author + "\t" + price);
        }

        public void sell()
        {
            throw new NotImplementedException();
        }
        public bool compare(string name)
        {
            if (this.name == name)
            {
                return true;
            }
            return false;
        }
    }
    class Laptops : ITypes
    {
        string? name;
        double? price;
        public int? number;

        public Laptops(string name, double price, int number)
        {
            this.name = name;
            this.price = price;
            this.number = number;
        }

        public void see()
        {
            Console.WriteLine(name + "\t" + price);
        }

        public void sell()
        {
            throw new NotImplementedException();
        }
        public bool compare(string name)
        {
            if (this.name == name)
            {
                return true;
            }
            return false;
        }
    }

    class Goods : ITypes
    {
        Goods? ordered;

        List<Ebooks> ebooks = new List<Ebooks>();
        List<Laptops> laptops = new List<Laptops>();

        public Goods()
        {

        }
        public Goods(Goods goods, Ebooks ebooks)
        {
            this.ebooks = goods.ebooks;
            this.laptops = goods.laptops;
            this.ebooks.Add(ebooks);
        }
        public Goods(Goods goods, Laptops laptops)
        {
            this.ebooks = goods.ebooks;
            this.laptops = goods.laptops;
            this.laptops.Add(laptops);
        }

        public void goods()
        {
            ordered = new Goods();
            ebooks.Add(new Ebooks("Молескін", "Юрій Новодворскі", 150));
            ebooks.Add(new Ebooks("Мистецтво любові", "Еріх Фромм", 100));
            ebooks.Add(new Ebooks("Правило 5 секунд", "Мел Робінс", 100));
            laptops.Add(new Laptops("HP ProBook 450 G5", 14000, 0));
            laptops.Add(new Laptops("HP ProBook 450 G3", 12650, 1));
        }
        public void see()
        {
            Console.WriteLine("\nЕлектронні книги(назва, автор, ціна)");
            foreach(Ebooks ebook in ebooks)
            {
                ebook.see();
            }
            Console.WriteLine("\nНоутбуки(Назва, ціна)");
            foreach (Laptops laptop in laptops)
            {
                laptop.see();
            }
        }
        public void sell()
        {
            string? command;
            see();
            Console.Write("Введіть категорію:   ");
            command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    Console.WriteLine("Введіть назву книги: ");
                    command = Console.ReadLine();
                    foreach(Ebooks ebook in ebooks)
                    {
                        if (command != null)
                        {
                            if (ebook.compare(command) == true)
                            {
                                if (ordered != null)
                                {
                                    ordered = new Goods(ordered, ebook);
                                    Console.WriteLine("Книгу успішно додано у кошик");
                                }
                            }
                        }
                    }
                    break;
                case "2":
                    Console.WriteLine("Введіть назву ноутбуку: ");
                    command = Console.ReadLine();
                    foreach (Laptops laptop in laptops)
                    {
                        if (command != null)
                        {
                            if (laptop.compare(command) == true)
                            {
                                if (laptop.number != 0)
                                {
                                    if (ordered != null)
                                    {
                                        ordered = new Goods(ordered, laptop);
                                        Console.WriteLine("Ноутбук успішно додано у кошик");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Нажаль, в наявності цього товару немає :(\n\n" +
                                        "Проте, не засмучуйтесь. В нас є 1 цікава пропозиція для вас:)\n");
                                    laptops[1].see();
                                    ebooks[0].see();
                                    ebooks[1].see();
                                    ebooks[2].see();
                                    Console.WriteLine("Бажаєте придбати?(y - yes)");
                                    command = Console.ReadLine();
                                    if(command == "y")
                                    {
                                        if (ordered != null)
                                        {
                                            ordered = new Goods(ordered, laptops[1]);
                                            ordered = new Goods(ordered, ebooks[0]);
                                            ordered = new Goods(ordered, ebooks[1]);
                                            ordered = new Goods(ordered, ebooks[2]); 
                                            Console.WriteLine("Товари успішно додані у кошик");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Категорія не знайдена");
                    break;
            }
        }
        public void SeeOrdered()
        {
            Console.WriteLine("Ваші замовлені товари:");
            if(ordered != null)
            ordered.see();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            Goods goods = new Goods();
            goods.goods();
            bool end = false;
            string? command;

            do
            {
                menu(goods);
                command = Console.ReadLine();
                Console.Clear();
                switch (command)
                {
                    case "1":
                        goods.sell();
                        if (Console.ReadLine() == " ") { }
                        Console.Clear();
                        break;
                    case "2":
                        goods.SeeOrdered();
                        if (Console.ReadLine() == " ") { }
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Невідома команда");
                        if (Console.ReadLine() == " ") { }
                        Console.Clear();
                        break;
                }
            }
            while (end != true);

        }
        static void menu(Goods goods)
        {
            Console.WriteLine("\t\tІнтернет-магазин");
            goods.see();
            Console.WriteLine("\n\n1 - замовити" 
                +"\n2 - переглянути усі замовлені товари");
        }
    }
}
