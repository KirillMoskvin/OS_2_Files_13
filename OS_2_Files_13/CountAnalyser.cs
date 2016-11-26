using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_2_Files_13
{
    class CountAnalyser
    {
        /// <summary>
        /// Функция находит количество файлов в двух директориях, сравнивает их
        /// и выводит результаты в виде строки
        /// </summary>
        /// <param name="dir1">Первая директория</param>
        /// <param name="dir2">Вторая директория</param>
        /// <returns>Данные о количестве файлов в директориях</returns>
        public static string Analysis(string dir1, string dir2)
        {
            int count1 = DirectoryMethods.GetCountAll(dir1);
            int count2 = DirectoryMethods.GetCountAll(dir2);
            string result="";
            result += "В 1-й директории " + count1 + " файлов, во 2-й: " + count2 + "\r\n";
            if (count1 > count2)
                result += "В 1-й директории на " + (count1 - count2).ToString() + " файлов больше\r\n";
            else
                if (count1 < count2)
                result += "Вo 2-й директории на " + (count2 - count1).ToString() + " файлов больше\r\n";
            else
                result += "Количество файлов в директориях одинаковo\r\n";
            return result;
        }
    }
}
