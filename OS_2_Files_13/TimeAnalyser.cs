using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_2_Files_13
{
    class TimeAnalyser
    {
        /// <summary>
        /// Нахождение и сравнени диапазонов дат созданий файлов в директориях
        /// </summary>
        /// <param name="dir1">Первая директория</param>
        /// <param name="dir2">Вторая директория</param>
        /// <returns>Информацию о датах создания файлов в виде строки</returns>
        public static string Analysis(string dir1, string dir2)
        {
            if (dir1 == dir2)
                return "Передано две одинаковых директории";
            string result="";
            bool compare = true; //Признак того, не пустая ли одна из директорий
            DateTime dateBegin1, dateBegin2, dateEnd1, dateEnd2;
            dateBegin1 = dateBegin2 = DateTime.MaxValue; //минимальные и максимальные даты созданий файлов в директориях
            dateEnd1 = dateEnd2 = DateTime.MinValue;

            int count1 = DirectoryMethods.GetCountAll(dir1); //количество файлов в директориях
            int count2 = DirectoryMethods.GetCountAll(dir2);
            //находим диапазон дат создания файлов в первой директории
            if (count1==0)
            {
                compare = false; //Если директория пуста, либо в ней содержится только один файл, сравнивать диапазоны дат не нужно
                result += "В 1-й директории отсутствуют файлы, диапазон дат получить невозможно\r\n";
            }
            else 
                if (count1 == 1)
                {
                    compare = false;
                    result += "В 1-й директории только один файл, диапазон дат получить невозможно\r\n";
                    result += "Дата создания этого файла: " + DirectoryMethods.GetAllCreationDates(dir1).First().ToString();
                }
                else
                { 
                    foreach (DateTime date in DirectoryMethods.GetAllCreationDates(dir1))
                    {
                        if (date < dateBegin1)
                            dateBegin1 = date;
                        else
                            if (date > dateEnd1)
                                dateEnd1 = date;
                    }
                    result += "Диапазон дат создания файлов 1-й директории: " + dateBegin1.ToString() + " - " + dateEnd1.ToString() + "\r\n";
            }
            //аналогично для второй
            if (count2 == 0)
            {
                compare = false; //Если директория пуста, либо в ней содержится только один файл, сравнивать диапазоны дат не нужно
                result += "Во 2-й директории отсутствуют файлы, диапазон дат получить невозможно\r\n";
            }
            else
                if (count2 == 1)
            {
                compare = false;
                result += "Вo 2-й директории только один файл, диапазон дат получить невозможно\r\n";
                result += "Дата создания этого файла: " + DirectoryMethods.GetAllCreationDates(dir2).First().ToString();
            }
            else
            {
                foreach (DateTime date in DirectoryMethods.GetAllCreationDates(dir2))
                {
                    if (date < dateBegin2)
                        dateBegin2 = date;
                    else
                        if (date > dateEnd2)
                        dateEnd2 = date;
                }
                result += "Диапазон дат создания файлов 2-й директории: " + dateBegin2.ToString() + " - " + dateEnd2.ToString() + "\r\n";
            }
            if (compare) //если обе директории содержат более одного файла, сравниваем диапазоны
            {
                if ((dateEnd1 - dateBegin1) > (dateEnd2 - dateBegin2))
                    result += "Диапазон дат создания файлов в 1-й директории больше, чем во 2-й";
                else
                    result += "Диапазон дат создания файлов вo 2-й директории больше, чем в 1-й";
            }
            return result;
        }
    }
}
