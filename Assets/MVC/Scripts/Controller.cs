using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class Controller : MonoBehaviour
    {
        private static Controller m_instance = null;

        private void Awake()
        {
            m_instance = this;
        }
        public static Controller Instance
        {
            get
            {
                return m_instance;
            }
        }
        private Model m_model = new Model();
        [SerializeField]
        private View m_view;

        public void SetStudentName(string name)
        {
            m_model.SetStudentName(name);
        }
        public string GetStudentName()
        {
            return m_model.GetStudentName();
        }
        public void SetStudentID(int id)
        {
            m_model.SetStudentID(id);
        }
        public int GetStudentID()
        {
            return m_model.GetStudentID();
        }
    }
}
