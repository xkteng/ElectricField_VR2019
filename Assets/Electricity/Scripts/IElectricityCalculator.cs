using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Electricity
{
    public interface IElectricityCalculator
    {
        /// <summary>
        /// 状态发生改变
        /// </summary>
        UnityEvent StatusChanged { get; }
        /// <summary>
        /// 获取所有带电体
        /// </summary>
        /// <returns></returns>
        IElectric[] GetElectrics();
        /// <summary>
        /// 获取电场强度
        /// </summary>
        /// <param name="pos">空间中的坐标</param>
        /// <returns>Vector3 类型的三维矢量</returns>
        Vector3 GetIntensity(Vector3 pos);
        /// <summary>
        /// 获取势能
        /// </summary>
        /// <param name="pos">空间中的坐标</param>
        /// <returns>float 类型的势能</returns>
        float GetPotential(Vector3 pos);
    }
}