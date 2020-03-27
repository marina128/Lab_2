using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp2
{
    [XmlInclude(typeof(Book))]
    [XmlInclude(typeof(Paper))]
    [XmlInclude(typeof(EResources))]
    [Serializable]
    /// <summary>
    /// Абстрактный класс, описывающий издание
    /// </summary>
    public abstract class Source
    {
        public string Name { get; set; }

        public string ALastName { get; set; }

        public Source() { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Название публикации</param>
        /// <param name="lname">Фамилия автора публикации</param>
        public Source(string name, string lname)
        {
            Trace.WriteLine("Entering Sourse.Sourse");
            Name = name;
            ALastName = lname;
            Trace.WriteLine("Exiting Sourse.Sourse");
        }

        /// <summary>
        /// Процедура вывода информации об издании
        /// </summary>
        public virtual void Display()
        {
            Trace.WriteLine("Entering Sourse.Display");
            Console.WriteLine($"Name: {Name}, author's last name: {ALastName}");
            Trace.WriteLine("Exiting Sourse.Display");
        }

        /// <summary>
        /// Определяет, являеся ли данное издание искомым
        /// </summary>
        /// <param name="name">Название публикации</param>
        /// <param name="lname">Фамилия автора публикации</param>
        /// <returns></returns>
        public bool Check(string name, string lname)
        {
            Trace.WriteLine("Entering Sourse.Check");
            bool fl = true;
            if (Name != name)
                fl = false;
            if (ALastName != lname)
                fl = false;
            Trace.WriteLine("Exiting Sourse.Check");
            return fl;
        }
    }

    /// <summary>
    /// Класс, описывающий книгу
    /// </summary>
    public class Book : Source
    {
        public int Year { get; set; }
        public string PublHouse { get; set; }

        public Book() { }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Название публикации</param>
        /// <param name="lname">Фамилия автора публикации</param>
        /// <param name="year">Год издания</param>
        /// <param name="house">Издательство</param>
        public Book(string name, string lname, int year, string house)
            : base(name, lname)
        {
            Trace.WriteLine("Entering Book.Book");
            Year = year;
            PublHouse = house;
            Trace.WriteLine("Exiting Book.Book");
        }

        /// <summary>
        /// Процедура вывода информации об издании
        /// </summary>
        public override void Display()
        {
            Trace.WriteLine("Entering Book.Display");
            Console.WriteLine($"Название: {Name}; фамилия автора: {ALastName}; год издания: {Year}; издательство: {PublHouse}.");
            Trace.WriteLine("Exiting Book.Display");
        }
    }

    /// <summary>
    /// Класс, описывающий газету
    /// </summary>
    public class Paper : Source
    {
        public string Magazine { get; set; }
        public int MagNum { get; set; }
        public int Year { get; set; }

        public Paper() { }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Название публикации</param>
        /// <param name="lname">Фамилия автора публикации</param>
        /// <param name="magazine">Название журнала</param>
        /// <param name="num">Номер журнала</param>
        /// <param name="year">Год издания</param>
        public Paper(string name, string lname, string magazine, int num, int year)
            : base(name, lname)
        {
            Trace.WriteLine("Entering Paper.Paper");
            Year = year;
            Magazine = magazine;
            MagNum = num;
            Trace.WriteLine("Exiting Paper.Paper");
        }

        /// <summary>
        /// Процедура вывода информации об издании
        /// </summary>
        public override void Display()
        {
            Trace.WriteLine("Entering Paper.Display");
            Console.WriteLine($"Название: {Name}; фамилия автора: {ALastName}; год издания: {Year}; название журнала: {Magazine}; номер журнала: {MagNum}.");
            Trace.WriteLine("Exiting Paper.Display");
        }
    }

    /// <summary>
    /// Класс, описывающий электронную книгу
    /// </summary>
    public class EResources : Source
    {
        public string Link { get; set; }
        public string Annotation { get; set; }

        public EResources() { }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Название публикации</param>
        /// <param name="lname">Фамилия автора публикации</param>
        /// <param name="link">Ссылка</param>
        /// <param name="annot">Аннотация</param>
        public EResources(string name, string lname, string link, string annot)
            : base(name, lname)
        {
            Trace.WriteLine("Entering EResources.EResources");
            Link = link;
            Annotation = annot;
            Trace.WriteLine("Exiting EResources.EResources");
        }

        /// <summary>
        /// Процедура вывода информации об издании
        /// </summary>
        public override void Display()
        {
            Trace.WriteLine("Entering EResources.Display");
            Console.WriteLine($"Название: {Name}; фамилия автора: {ALastName}; ссылка: {Link}; аннотация: {Annotation}.");
            Trace.WriteLine("Exiting EResources.Display");
        }
    }


    class Program
    {
        /// <summary>
        /// Метод считываетиз файла данные и записывает их в массив
        /// </summary>
        /// <returns>Массив типа Sourse</returns>
        public static Source[] CrArr()
        {
            Trace.WriteLine("Entering Program.CrArr");
            FileStream fin;
            string s, stmp;
            int n, l, num;
            List<string> list = new List<string>();
            Source[] arr;
            Book book;
            Paper paper;
            EResources er;
            
            fin = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
            StreamReader fstr_in = new StreamReader(fin);
            
            n = Convert.ToInt32(fstr_in.ReadLine());
            arr = new Source[n];

            for (int i = 0; i < n; i++)
            {
                s = fstr_in.ReadLine();
                l = s.Length;
                int cnt = 0;
                stmp = "";

                if (s[cnt] != '"')
                {
                    throw new Exception();
                }
                stmp += s[cnt];
                cnt++;
                while (s[cnt] != '"' && cnt < l)
                {
                    stmp += s[cnt];
                    cnt++;
                }
                stmp += s[cnt];
                cnt++;
                
                if (cnt == l - 1)
                {
                    throw new Exception();
                }
                list.Add(stmp);
                stmp = "";

                for (int j = cnt; j < l; j++)
                {
                    while (j < l && s[j] != ' ')
                    {
                        stmp += s[j];
                        j++;
                    }
                    if (stmp != "")
                    {
                        list.Add(stmp);
                        stmp = "";
                    }
                    
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
            Trace.WriteLine("Exiting Program.CrArr");
            return arr;
        }

        /// <summary>
        /// Метод производит поиск и вывод изданий по фамилии автора
        /// </summary>
        /// <param name="arr">Массив всех изданий</param>
        /// <param name="lastname">Фамилия автора</param>
        public static void Search(Source[] arr, string lastname)
        {
            Trace.WriteLine("Entering Program.Search");
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ALastName == lastname)
                    arr[i].Display();
            }
            Trace.WriteLine("Exiting Program.Search");
        }

        static void Main(string[] args)
        {
            Trace.WriteLine("Entering Program.Main");
            XmlSerializer serializer = new XmlSerializer(typeof(Source));
            try
            {
                Source[] arr = CrArr();

                Console.WriteLine("Введите фамилию.");
                string search = Console.ReadLine();

                Search(arr, search);
                
                using (StreamWriter streamWriter = new StreamWriter("Source.xml"))
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        serializer.Serialize(streamWriter, arr[i]);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Неверный формат введенных днных.");
            }

            Trace.WriteLine("Exiting Program.Main");
            Console.ReadKey();

        }
    }
}
