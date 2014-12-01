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
                return "Справка.chm";
            }
        }

        /// <summary>
        /// Задает путь к основной папке, в которой будут храниться данные программы.
        /// </summary>
        /*private static string _defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Notebook", "Resources");*/

       
        /// <summary>
        /// Подготавливает все папки и файл для корректной работы программы.
        /// </summary>
        public static void PrepareBoforeStart()
        {

            if (!File.Exists("Справка.chm") ||
                // Проверяет массивы байт на равенство.
                !File.ReadAllBytes("Справка.chm").
                SequenceEqual(Properties.Resources.Справка))
                File.WriteAllBytes("Справка.chm", Properties.Resources.Справка);
        }

        /// <summary>
        /// Сохраняет переданный список в файл database.dat по пути defaultPath.
        /// </summary>
        /// <param name="notebook">Сохраняемый список.</param>
        public static void Save(List<Student> notebook)
        {
            PrepareBoforeStart();
            notebook = notebook.OrderBy(s => s.FIO).ToList();
            StaticData.students = notebook;
            using (FileStream f = new FileStream(@"..\..\database.dat", FileMode.Create))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(f, notebook);
            }
        }

        /// <summary>
        /// Загружает список Persone из файла database.dat по пути defaultPath.
        /// </summary>
        /// <returns>Загруженный список.</returns>
        public static List<Student> LoadStudent()
        {
            try
            {
                using (FileStream f = new FileStream(@"..\..\database.dat", FileMode.Open))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    return b.Deserialize(f) as List<Student>;
                }
            }
            catch
            {
                File.Create(@"..\..\database.dat").Close();
                return new List<Student>();
            }
        }

        /// <summary>
        /// Сохраняет переданный список в файл database.dat по пути defaultPath.
        /// </summary>
        /// <param name="notebook">Сохраняемый список.</param>
        public static void Save(List<Faculty> notebook)
        {
            PrepareBoforeStart();
            notebook = notebook.OrderBy(f => f.Title).ToList();
            foreach(var f in notebook)
            {
                f.Groups = f.Groups.OrderBy(g => g.Title).ToList();
            }
            using (FileStream f = new FileStream(@"..\..\databaseFac.dat", FileMode.Create))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(f, notebook);
            }
        }

        /// <summary>
        /// Загружает список Persone из файла database.dat по пути defaultPath.
        /// </summary>
        /// <returns>Загруженный список.</returns>
        public static List<Faculty> LoadFac()
        {
            try
            {
                using (FileStream f = new FileStream(@"..\..\databaseFac.dat", FileMode.Open))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    return b.Deserialize(f) as List<Faculty>;
                }
            }
            catch
            {
                File.Create(@"..\..\databaseFac.dat").Close();
                return new List<Faculty>();
            }
        }

        public static List<Student> findByGroup(IEnumerable<Student> stud, List<Group> groups)
        {
            return (from student in stud from @group in groups where student.Group.Title.Equals(@group.Title) select student).ToList();
        }

        public static List<Student> findByFaculty(IEnumerable<Student> stud, List<Faculty> faculties)
        {
            return (from student in stud from faculty in faculties where student.Faculty.Title.Equals(faculty.Title) select student).ToList();
        }
    }
}
