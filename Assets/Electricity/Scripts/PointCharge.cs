//***********************************************
//By Kai
//Description:点电荷
//
//
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Electricity
{
    public class PointCharge : IElectric
    {
        /// <summary>
        /// 带电量
        /// </summary>
        private float m_quantity;
        /// <summary>
        /// 位置
        /// </summary>
        private Vector3 m_position;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="position"></param>
        public PointCharge(float quantity, Vector3 position)
        {
            this.m_quantity = quantity;
            this.m_position = position;
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
                return m_position;
            }
        }
        public UnityEvent StatusChanged
        {
            get
            {
                return null;
            }
        }

        public Transform transform
        {
            get
            {
                return null;
            }
        }

        public object Clone()
        {
            return new PointCharge(m_quantity, m_position);
        }

        public float GetPotential(Vector3 pos)
        {
            var r_vec = pos - m_position;
            var r_mag = r_vec.magnitude;
            return Math_Ele.K * m_quantity / r_mag;
        }

        public Vector3 GetIntensity(Vector3 pos)
        {
            var r_vec = pos - m_position;
            var r_mag = r_vec.magnitude;
            return Math_Ele.K * m_quantity * r_vec / Mathf.Pow(r_mag, 3);
        }
        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="electric"></param>
        /// <returns></returns>
        public bool IsEqual(IElectric electric)
        {
            if (m_quantity == electric.Quantity && m_position == electric.Position)
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