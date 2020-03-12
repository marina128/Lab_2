using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    public class Program
    {
        public static FileStream  fout = new FileStream("output.txt", FileMode.Create);
        public static StreamWriter fstr_out = new StreamWriter(fout);

        static void Main(string[] args)
        {
            string s = Reader();
            if (s.Length == 0)
                return;
            List<float> list = CrList(s);
            if (list.Count ==0)
                return;
            s = FormRes(list);
            Writer(s);
            Console.ReadKey();
        }

        /// <summary>
        /// Считывание строки из файла
        /// </summary>
        /// <returns>Считанную строку</returns>
        public static string Reader()
        {
            FileStream fin;
            string s;
            try
            {
                fin = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
                StreamReader fstr_in = new StreamReader(fin);

                s = fstr_in.ReadLine();

                fin.Close();
                //проверка
                Console.WriteLine(s);
                if (s == null)
                    throw new Exception("Файл пустой.");
                return s;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                fstr_out.Write($"Ошибка: {ex.Message}");
                fstr_out.Close();
                return "";
            }
        }

        /// <summary>
        /// Запись чисел из строки в список
        /// </summary>
        /// <param name="s">Строка с данными из файла</param>
        /// <returns>Список чисел</returns>
        public static List<float> CrList(string s)
        {
            int l = s.Length;
            string stmp;
            List<float> list = new List<float>();
            try
            {
                for (int i = 0; i < l; i++)
                {
                    if (s[i] != ' ')
                    {
                        stmp = s[i].ToString();
                        if (i < l - 1)
                        {
                            i++;
                            if (stmp == "0" && s[i] != ' ')
                                throw new Exception("Входная строка имела неверный формат.");
                            while (s[i] != ' ' && i < l - 1)
                            {
                                stmp += s[i];
                                i++;
                            }
                            if (i == l - 1)
                                stmp += s[i];
                        }
                        
                        list.Add(float.Parse(stmp.Replace('.', ',')));
                    }
                }
                if (list.Count == 0)
                    throw new Exception("Входная строка имела неверный формат.");
                list.Sort();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                fstr_out.Write($"Ошибка: {ex.Message}");
                fstr_out.Close();
                list.Clear();
                return list;
            }
        }

        /// <summary>
        /// Запись повторяющихся чисел в строку
        /// </summary>
        /// <param name="list">Список чисел</param>
        /// <returns>Ничего не возвращает</returns>
        public static string FormRes(List<float> list)
        {
            //удаление лишних значений
            for (int i = 0; i < list.Count; i++)        
            {
                if (i < list.Count - 1 && list[i] == list[i + 1])
                {
                    i++;
                    while (i < list.Count - 1 && list[i] == list[i + 1])
                    {
                        list.RemoveAt(i);
                    }
                }
                list.RemoveAt(i);
                i--;
            }

            //запись в строку 
            string res = "";
            for (int i = 0; i < list.Count; i++)
            {
                res += list[i].ToString() + " ";
            }

            return res;
        }

        /// <summary>
        /// Считывание строки из файла
        /// </summary>
        /// <param name="res">Строка, с результатом</param>
        /// <returns>Считанную строку</returns>
        public static void Writer(string res)
        {
            try
            {
                //проверка
                Console.WriteLine(res);

                fstr_out.Write(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                fstr_out.Write($"Ошибка: {ex.Message}");
                fstr_out.Close();
                return;
            }

            fstr_out.Close();
        }
    }
}
