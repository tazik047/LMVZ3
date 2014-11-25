using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lmvz3
{
    public class Student
    {
        string fio; //ФИО
        string pass; //Номер пасспотра
        string id; //Идентификационный номер
        string home;//место проживания 
        DateTime birth;//дата рождения
        string faculty; //Факультет
        string speciality; //Специальность
        string formOfStudy;//форма обучения
        int number;//номер телефона
        public string FIO //ФИО
        {
            get { return fio; }
            set { fio = value; }
        }
        public string ID //Код
        {
            get { return id; }
            set { id = value; }
        }
        public string Pass //Номер пасспорта
        {
            get { return pass; }
            set { pass = value; }
        }

        public DateTime Birth //дата рождения
        {
            get { return birth; }
            set { birth = value; }
        }
        public string Home //Место проживания
        {
            get { return home; }
            set { home = value; }
        }
        public string Faculty //факультет
        {
            get { return faculty; }
            set { faculty = value; }
        }
        public string Speciality //Специальность
        {
            get { return speciality; }
            set { speciality = value; }
        }
        public string FormOfStudy //форма обучения
        {
            get { return formOfStudy; }
            set { formOfStudy = value; }
        }
        public int Number //Номер телефона
        {
            get { return number; }
            set { number = value; }
        }
    
        public Student()
        {
        }
        public Student(string fio,string pass,  string id, string home, DateTime birth, string faculty,string speciality, string formOfStudy, int number)
        {
            fio = FIO;
            pass = Pass;
            id = ID;
            home = Home;
            birth = Birth;
            faculty = Faculty;
            speciality = Speciality;
            formOfStudy = FormOfStudy;
            number = Number;
        }
    }
}
