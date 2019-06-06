//***********************************************
//By Kai
//Description��
//
//
//***********************************************
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Electricity
{
    public sealed class ElectricityManager : SerializedMonoBehaviour,IElectricityManager
    {
        [SerializeField]
        private bool m_isAutoRefresh = false;

        private static ElectricityManager _instance;
        public static ElectricityManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ElectricityManager>();
                }
                return _instance;
            }
        }
        [SerializeField]
        private IElectricityCalculator m_calculator;
        [SerializeField]
        private IElectricityRenderer m_renderer;
        [SerializeField]
        private float m_delta = 0.1f;
        [SerializeField]
        private float m_distance = 0.01f;
        [SerializeField]
        private int m_times = 12;
        [SerializeField]
        private float m_maximum_Intensity = 100000f;
        [SerializeField]
        private float m_minimum_Intensity = 0.001f;

        [Button("Refresh")]
        public void Refresh()
        {
            m_renderer.RenderFieldLines(Intensity_Func, Potential_Func);
        }
        public void ShowFieldLine()
        {
            m_renderer.Show();
        }
        public void HideFieldLine()
        {
            m_renderer.Hide();
        }
        public void Reset()
        {
            m_renderer.DestroyAllRenders();
            m_renderer.CreateFieldLineRendersAroundPoints(m_calculator.GetElectrics(), m_distance, m_times, m_delta, m_maximum_Intensity, m_minimum_Intensity);
            m_renderer.RenderFieldLines(Intensity_Func, Potential_Func);
        }

        private void Start()
        {
            //Reset();
            if (m_isAutoRefresh)
            {
                m_calculator.StatusChanged.AddListener(OnStatusChanged);
            }
        }
        private void OnStatusChanged()
        {
            Refresh();
        }
        private Vector3 Intensity_Func(Vector3 pos)
        {
            return m_calculator.GetIntensity(pos);
        }
        private float Potential_Func(Vector3 pos)
        {
            return m_calculator.GetPotential(pos);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Refresh();
            }
        }
    }
}