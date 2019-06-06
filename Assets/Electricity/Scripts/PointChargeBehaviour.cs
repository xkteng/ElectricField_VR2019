//***********************************************
//By Kai
//Description：点电荷组件
//
//
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Electricity
{
    public class PointChargeBehaviour : MonoBehaviour, IElectric
    {
        /// <summary>
        /// 检测的时间间隔
        /// </summary>
        private const float DELAY_TIME = 0.03f;

        /// <summary>
        /// 带电量
        /// </summary>
        [SerializeField]
        protected float m_quantity = 1;
        /// <summary>
        /// 是否自己判断状态变化
        /// </summary>
        [SerializeField]
        private bool m_selfJudge = true;
        /// <summary>
        /// 状态变化事件
        /// </summary>
        protected UnityEvent m_statusChanged = new UnityEvent();
        /// <summary>
        /// 上一次记录的带电体状态
        /// </summary>
        protected IElectric m_electric_LastRecord;


        private void OnEnable()
        {
            ElectricityCalculator.Add(this);

        }

        private void OnDisable()
        {
            ElectricityCalculator.Remove(this);

        }
        private void Start()
        {
            if (m_selfJudge)
            {
                StartCoroutine(JudgeStatusEnumerator());
            }
        }
        public float Quantity
        {
            get
            {
                return m_quantity;
            }
        }
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }
        public UnityEvent StatusChanged
        {
            get
            {
                return m_statusChanged;
            }
        }
        public object Clone()
        {
            return new PointCharge(m_quantity, transform.position);
        }
        public float GetPotential(Vector3 pos)
        {
            var r_vec = pos - transform.position;
            var r_mag = r_vec.magnitude;
            return Math_Ele.K * m_quantity / r_mag;
        }

        public Vector3 GetIntensity(Vector3 pos)
        {
            var r_vec = pos - transform.position;
            var r_mag = r_vec.magnitude;
            return Math_Ele.K * m_quantity * r_vec / Mathf.Pow(r_mag, 3);
        }


        private IEnumerator JudgeStatusEnumerator()
        {
            var wait = new WaitForSeconds(DELAY_TIME);
            RecordStatus();
            while (true)
            {
                if (IsStatusChanged())
                {
                    StatusChanged.Invoke();
                }
                RecordStatus();
                yield return wait;
            }
        }
        /// <summary>
        /// 记录状态
        /// </summary>
        private void RecordStatus()
        {
            m_electric_LastRecord = (IElectric)this.Clone();
        }
        /// <summary>
        /// 状态是否发生改变
        /// </summary>
        /// <returns></returns>
        private bool IsStatusChanged()
        {
            if (!this.IsEqual(m_electric_LastRecord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsEqual(IElectric electric)
        {
            if (m_quantity == electric.Quantity && transform.position == electric.Position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}