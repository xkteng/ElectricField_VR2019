using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Electricity
{
    public class ElectricityCalculator : MonoBehaviour,IElectricityCalculator
    {
        /// <summary>
        /// 整个电场发生了改变
        /// </summary>
        private static UnityEvent s_statusChanged = new UnityEvent();
        private static List<IElectric> s_electricList = new List<IElectric>();
        /// <summary>
        /// 将对应带电体加入List当中
        /// </summary>
        /// <param name="electric"></param>
        public static void Add(IElectric electric)
        {
            s_electricList.Add(electric);
            electric.StatusChanged.AddListener(OnStatusChanged);
        }
        /// <summary>
        /// 将对应带电体移除List
        /// </summary>
        /// <param name="electric"></param>
        public static  void Remove(IElectric electric)
        {
            s_electricList.Remove(electric);
            electric.StatusChanged.RemoveListener(OnStatusChanged);
        }
        /// <summary>
        /// 状态发生改变时调用
        /// </summary>
        public UnityEvent StatusChanged { get { return s_statusChanged; } }
        /// <summary>
        /// 获取空间中某一点的电势
        /// </summary>
        /// <param name="pos">空间当中的位置</param>
        /// <returns>电势</returns>
        public float GetPotential(Vector3 pos)
        {
            var potential_sum = 0f;
            foreach (var item in s_electricList)
            {
                potential_sum += item.GetPotential(pos);
            }
            return potential_sum;
        }
        /// <summary>
        /// 获取空间中某一点的场强
        /// </summary>
        /// <param name="pos">空间当中的位置</param>
        /// <returns>空间中某一点的场强</returns>
        public Vector3 GetIntensity(Vector3 pos)
        {
            Vector3 intensity_sum = Vector3.zero;
            foreach (var item in s_electricList)
            {
                intensity_sum += item.GetIntensity(pos);
            }
            return intensity_sum;
        }
        /// <summary>
        /// 获取全部点电荷
        /// </summary>
        /// <returns>返回全部点电荷</returns>
        public IElectric[] GetElectrics()
        {
            return s_electricList.ToArray();
        }
        /// <summary>
        /// 状态发生改变
        /// </summary>
        private static void OnStatusChanged()
        {
            s_statusChanged.Invoke();
        }
    }
}