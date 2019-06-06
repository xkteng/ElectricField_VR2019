using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MVC
{
    public class Model
    {
        private Student m_student=new Student(1,"Tom");
        public void SetStudentID(int id)
        {
            m_student.m_ID = id;
        }
        public int GetStudentID()
        {
            return m_student.m_ID;
        }
        public void SetStudentName(string name)
        {
            m_student.m_name = name;
        }
        public string GetStudentName()
        {
            return m_student.m_name;
        }
    }
    public class Student
    {
        public int m_ID;
        public string m_name;

        public Student(int ID, string name)
        {
            this.m_ID = ID;
            this.m_name = name;
        }
    }
}
