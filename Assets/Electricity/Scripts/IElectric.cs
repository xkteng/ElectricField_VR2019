//***********************************************
//By Kai
//Description：带电体的接口
//
//
//***********************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Electricity
{
    public interface IElectric : ICloneable
    {
        /// <summary>
        /// 带电量
        /// </summary>
        float Quantity { get; }
        /// <summary>
        /// 位置
        /// </summary>
        Vector3 Position { get; }
        /// <summary>
        /// 获取电势
        /// </summary>
        /// <param name="pos">空间中的位置</param>
        /// <returns></returns>
        float GetPotential(Vector3 pos);
        /// <summary>
        /// 获取电场强度
        /// </summary>
        /// <param name="pos">空间当中位置</param>
        /// <returns></returns>
        Vector3 GetIntensity(Vector3 pos);
        /// <summary>
        /// 状态改变事件
        /// </summary>
        UnityEvent StatusChanged { get; }
        /// <summary>
        /// 判断是否相等
        /// </summary>
        /// <param name="electric"></param>
        /// <returns></returns>
        bool IsEqual(IElectric electric);

        Transform transform { get; }
    }
}