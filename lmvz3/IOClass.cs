using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace lmvz3
{
    /// <summary>
    /// Класс, для работы с файловой системой.
    /// </summary>
    public static class IOClass
    {
        /// <summary>
        /// Путь к справке программы.
        /// </summary>
        public static string PathHelp
        {
            get 
            {
                PrepareBoforeStart();
                return Path.Combine(_defaultPath, "Справка.chm");
            }
        }

        /// <summary>
        /// Задает путь к основной папке, в которой будут храниться данные программы.
        /// </summary>
        private static string _defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Notebook", "Resources");

       
        /// <summary>
        /// Подготавливает все папки и файл для корректной работы программы.
        /// </summary>
        public static void PrepareBoforeStart()
        {
            if (!Directory.Exists(_defaultPath))
                Directory.CreateDirectory(_defaultPath);

            if (!File.Exists(Path.Combine(_defaultPath, "Справка.chm")) ||
                // Проверяет массивы байт на равенство.
                !File.ReadAllBytes(Path.Combine(_defaultPath, "Справка.chm")).
                SequenceEqual(Properties.Resources.Справка))
                File.WriteAllBytes(Path.Combine(_defaultPath, "Справка.chm"), Properties.Resources.Справка);
        }

        /// <summary>
        /// Сохраняет переданный список в файл database.dat по пути defaultPath.
        /// </summary>
        /// <param name="notebook">Сохраняемый список.</param>
        public static void Save(List<Student> notebook)
        {
            PrepareBoforeStart();
            using(FileStream f = new FileStream(Path.Combine(_defaultPath,"database.dat"),FileMode.Create))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(f, notebook);
            }
        }

        /// <summary>
        /// Загружает список Persone из файла database.dat по пути defaultPath.
        /// </summary>
        /// <returns>Загруженный список.</returns>
        public static List<Student> Load()
        {
            try
            {
                using (FileStream f = new FileStream(Path.Combine(_defaultPath, "database.dat"), FileMode.Open))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    return b.Deserialize(f) as List<Student>;
                }
            }
            catch
            {
                File.Create(Path.Combine(_defaultPath, "database.dat")).Close();
                return new List<Student>();
            }
        }
    }
}
