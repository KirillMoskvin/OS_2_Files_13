using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;
using System.ComponentModel;

namespace OS_2_Files_13
{
    class DirectoryMethods
    {
        /// <summary>
        /// Формирует путь требуемый функцией FindFirstFile.
        /// </summary>
        private static string MakePath(string path)
        {
            return Path.Combine(path, "*");
        }

        /// <summary>
        /// Возвращает список файлов или каталогов находящихся по заданному пути path.
        /// </summary>
        /// <param name="path">Путь для которого нужно возвратить список.</param>
        /// <param name="isGetDirs">
        /// Если true - функция возвращает список каталогов, иначе файлов.
        /// </param>
        /// <returns>Список файлов или каталогов.</returns>
        private static IEnumerable<string> GetInternal(string path, bool isGetDirs)
        {
            // Структура в которую функции FindFirstFile и FindNextFile возвращают
            // информацию о текущем файле.
            WIN32_FIND_DATA findData;
            // Получаем информацию о текущем файле и дескриптор перечислителя.
            // Этот дескриптор требуется передавать функции FindNextFile для плучения
            // следующих файлов.
            IntPtr findHandle = FindFirstFile(MakePath(path), out findData);

            //  Если произошла ошибка, то
            // нужно вынуть информацию об ошибке и перепаковать ее в исключение.
            if (findHandle == INVALID_HANDLE_VALUE)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            try
            {
                do
                    if (isGetDirs
                        ? (findData.dwFileAttributes & FileAttributes.Directory) != 0
                        : (findData.dwFileAttributes & FileAttributes.Directory) == 0)
                        yield return findData.cFileName;
                while (FindNextFile(findHandle, out findData));
            }
            finally
            {
                FindClose(findHandle);
            }
        }

        /// <summary>
        /// Возвращает список файлов для некоторого пути.
        /// </summary>
        /// <param name="path">
        /// Каталог для которого нужно получить список файлов.
        /// </param>
        /// <returns>Список файлов каталога.</returns>
        public static IEnumerable<string> GetFiles(string path)
        {
            return GetInternal(path, false);
        }

        /// <summary>
        /// Возвращает список каталогов для некоторого пути. Функция не перебирает
        /// вложенные подкаталоги!
        /// </summary>
        /// <param name="path">
        /// Каталог для которого нужно получить список подкаталогов.
        /// </param>
        /// <returns>Список файлов каталога.</returns>
        public static IEnumerable<string> GetDirectories(string path)
        {
            return GetInternal(path, true);
        }

        /// <summary>
        /// Функция возвращает список относительных путей ко всем подкаталогам
        /// (в том числе и вложенным) заданного пути.
        /// </summary>
        /// <param name="path">Путь для которого унжно получить подкаталоги.</param>
        /// <returns>Список подкатлогов.</returns>
        public static IEnumerable<string> GetAllDirectories(string path)
        {
            // Сначала перебираем подкаталоги первого уровня вложенности...
            foreach (string subDir in GetDirectories(path))
            {
                // игнорируем имя текущего каталога и родительского.
                if (subDir == ".." || subDir == ".")
                    continue;

                // Комбинируем базовый путь и имя подкаталога.
                string relativePath = Path.Combine(path, subDir);

                // возвращаем пользователю относительный путь.
                yield return relativePath;

                // Создаем, рекурсивно, итератор для каждого подкаталога и...
                // возвращаем каждый его элемент в качестве элементов текущего итератора.
                // Этот прием позволяет обойти ограничение итераторов C# 2.0 связанное
                // с невозможностью вызовов "yield return" из функций вызваемых из 
                // функции итератора. К сожалению это приводит к созданию временного
                // вложенного итератора на каждом шаге рекурсии, но затраты на создание
                // такого объекта относительно не велики, а удобство очень даже ощутимо.
                foreach (string subDir2 in GetAllDirectories(relativePath))
                    yield return subDir2;
            }
        }
        #region Для задачи
        /// <summary>
        /// Метод позволяет получить все файлы из данного каталога и всех подкаталогов
        /// </summary>
        /// <param name="directory">Заданный каталог</param>
        /// <returns>Список полных имен файлов (с учетом директорий)</returns>
        public static IEnumerable<String> GetAllFilesInDirectory (string directory)
        {
            foreach (string file in GetFiles(directory))
                yield return Path.Combine(directory,file); //Получили все файлы из данной директории

            foreach (string dir in GetAllDirectories(directory))
                foreach (string file in GetFiles(dir))
                    yield return Path.Combine(dir, file); //и делаем то же самое для каждого имени файла
        }

        /// <summary>
        /// Функция считает количество файлов в папке
        /// </summary>
        /// <param name="dir">Папка</param>
        /// <returns>Количество файлов</returns>
        public static int GetCountAll(string dir)
        {
            int res = 0;
            foreach (string str in GetAllFilesInDirectory(dir))
                res++;
            return res;
        }

        /// <summary>
        /// Функция получает список дат создания файлов в звдвнной директории
        /// </summary>
        /// <param name="directory">Директория</param>
        /// <returns>Даты создания</returns>
        public static IEnumerable<DateTime> GetAllCreationDates(string directory)
        {
            foreach (string fileName in GetAllFilesInDirectory(directory))
                yield return File.GetCreationTime(fileName);
        }
        #endregion

        #region Импорт из kernel32

        private const int MAX_PATH = 260;

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        [BestFitMapping(false)]
        private struct WIN32_FIND_DATA
        {
            public FileAttributes dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternate;
        }

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindFirstFile(string lpFileName,
            out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool FindNextFile(IntPtr hFindFile,
            out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FindClose(IntPtr hFindFile);

        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        #endregion
    }
}
