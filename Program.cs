using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    abstract class Sourse
    {
        public string Name { get; set; }

        public string ALastName { get; set; }

        public Sourse(string name, string lname)
        {
            Name = name;
            ALastName = lname;
        }
        
        public virtual void Display()
        {
            Console.WriteLine($"Name: {Name}, author's last name: {ALastName}");
        }

        //определить, являеся ли данное издание искомым
        public bool Check(string name, string lname)
        {
            bool fl = true;
            if (Name != name)
                fl = false;
            if (ALastName != lname)
                fl = false;
            return fl;
        }
    }

    ///КНИГА
    class Book : Sourse
    {
        public int Year { get; set; }
        public string PublHouse { get; set; }

        public Book(string name, string lname, int year, string house)
            : base(name, lname)
        {
            Year = year;
            PublHouse = house;
        }
        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, author's last name: {ALastName}, year of publishing: {Year}, publishing house {PublHouse}");
        }
    }
    ///ГАЗЕТА
    class Paper : Sourse
    {
        public string Magazine { get; set; }
        public int MagNum { get; set; }
        public int Year { get; set; }

        public Paper(string name, string lname, string magazine, int num, int year)
            : base(name, lname)
        {
            Year = year;
            Magazine = magazine;
            MagNum = num;
        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, author's last name: {ALastName}, year of publishing: {Year}, name of the magazine: {Magazine}, number of the magazine: {MagNum}");
        }
    }

    ///ЭЛЕКТРОН
    class EResources : Sourse
    {
        public string Link { get; set; }
        public string Annotation { get; set; }

        public EResources(string name, string lname, string link, string annot)
            : base(name, lname)
        {
            Link = link;
            Annotation = annot;
        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, author's last name: {ALastName}, link: {Link}, annotation: {Annotation}");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            FileStream fin;
            string s, stmp;
            int n, l, num;
            List<string> list = new List<string>();
            Book book;
            Paper paper;
            EResources er;

            //созать массив, газету, эл.книгу
            fin = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
            StreamReader fstr_in = new StreamReader(fin);

            n = Convert.ToInt32(fstr_in.ReadLine());
            Sourse[] arr = new Sourse[n];

            for (int i = 0; i < n; i++)
            {
                s = fstr_in.ReadLine();
                l = s.Length;
                stmp = "";

                for (int j = 0; j < l; j++)
                {
                    while (s[j] != ' ' && j < l)
                    {
                        stmp += s[j];
                        j++;
                    }

                    list.Add(stmp);
                }

                if (Int32.TryParse(list[2], out num))
                {
                    book = new Book(list[0], list[1], num, list[3]);
                    arr[i] = book;
                }
                else if (Int32.TryParse(list[3], out num))
                {
                    paper = new Paper(list[0], list[1], list[2], num, Convert.ToInt32(list[4]));
                    arr[i] = paper;
                }
                else
                {
                    er = new EResources(list[0], list[1], list[2], list[3]);
                    arr[i] = er;
                }
                list.Clear();
            }

            fin.Close();

            ///ПОИСК ИЗАНИЯ ПО ФАМИЛИИ
            Console.WriteLine("Введите фамилию.");
            string search = Console.ReadLine();
            for (int i = 0; i < n; i++)
            {
                if (arr[i].ALastName == search)
                    arr[i].Display();
            }

            Console.ReadKey();

        }
    }
}
