using DatatablesServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatatablesServerSide.Data
{
    public static class StudentContext
    {
        //StudentList is static to be able to reach in application scope
        public static List<Student> StudentList { get; set; }

        //Creates “studentCount” students and adds into student list
        public static void InitStudentList(int studentCount)
        {
            StudentList = new List<Student>();
            for (int i = 1; i < studentCount + 1; i++)
            {
                StudentList.Add(
                    new Student()
                    {
                        Id = i,
                        Firstname = "Firstname" + i,
                        Lastname = "Lastname" + i,
                        CreatedDate = DateTime.Now
                    }
                );
            }
        }
    }
}
