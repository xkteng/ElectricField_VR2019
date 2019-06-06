using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class View : MonoBehaviour
    {
        [SerializeField]
        private InputField m_inputField_ID;
        [SerializeField]
        private Button m_button_ID;
        [SerializeField]
        private InputField m_inputField_Name;
        [SerializeField]
        private Button m_button_Name;
        [SerializeField]
        private Text m_text_Output;

        private void Awake()
        {
            SubscribeEvent();
        }
        private void SubscribeEvent()
        {
            m_button_ID.onClick.AddListener(OnButtonIDClicked);
            m_button_Name.onClick.AddListener(OnButtonNameClicked);
        }
        private void OnButtonIDClicked()
        {
            int id = int.Parse(m_inputField_ID.text);
            Controller.Instance.SetStudentID(id);
            PrintStudentData(Controller.Instance.GetStudentID(),Controller.Instance.GetStudentName());
        }
        private void OnButtonNameClicked()
        {
            string name = m_inputField_Name.text;
            Controller.Instance.SetStudentName(name);
            PrintStudentData(Controller.Instance.GetStudentID(),Controller.Instance.GetStudentName());
        }
        public void PrintStudentData(int id, string name)
        {
            m_text_Output.text = "ID=" + id + ",Name=" + name;
        }
    }
}
