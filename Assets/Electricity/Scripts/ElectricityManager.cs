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
    public sealed class ElectricityManager : SerializedMonoBehaviour, IElectricityManager
    {
   
        /// <summary>
        /// 单例对象
        /// </summary>
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
        /// <summary>
        /// 是否自动刷新
        /// </summary>
        [SerializeField]
        private bool m_isAutoRefresh = false;
        /// <summary>
        /// 电场计算器
        /// </summary>
        [SerializeField]
        private IElectricityCalculator m_calculator;
        /// <summary>
        /// 电场渲染器
        /// </summary>
        [SerializeField]
        private IElectricityRenderer m_renderer;
        /// <summary>
        /// 步进长度
        /// </summary>
        [SerializeField]
        private float m_delta = 0.1f;
        /// <summary>
        /// 绘制电场线的起点和电荷之间的距离
        /// </summary>
        [SerializeField]
        private float m_startDistance = 0.2f;
        /// <summary>
        /// 当绘制点与任意电荷之间距离小于这个数值时，停止绘制
        /// </summary>
        [SerializeField]
        private float m_minDistance = 0.1f;
        /// <summary>
        /// 当绘制点与任意电荷之间距离大于这个数值时，停止绘制
        /// </summary>
        [SerializeField]
        private float m_maxDistance = 5.0f;
        /// <summary>
        /// 单个电荷射出的电场线
        /// </summary>
        [SerializeField]
        private int m_times = 12;
        /// <summary>
        /// 刷新
        /// </summary>
        [Button("Refresh")]
        public void Refresh()
        {
            m_renderer.RenderFieldLines(Intensity_Func);
        }
        /// <summary>
        /// 显示电场线
        /// </summary>
        public void ShowFieldLine()
        {
            m_renderer.Show();
        }
        /// <summary>
        /// 隐藏电场线
        /// </summary>
        public void HideFieldLine()
        {
            m_renderer.Hide();
        }
        /// <summary>
        /// 重置
        /// </summary>
        [Button("Reset")]
        public void Reset()
        {
            m_renderer.DestroyAllRenders();
            m_renderer.CreateFieldLineRendersAroundPoints(m_calculator.GetElectrics(), m_startDistance, m_minDistance, m_maxDistance, m_times, m_delta);
            m_renderer.RenderFieldLines(Intensity_Func);
        }
        #region PRIVATE
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
        #endregion
    }
}