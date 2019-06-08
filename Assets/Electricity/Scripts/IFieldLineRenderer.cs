using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Electricity
{
    public delegate Vector3 IntensityFunction(Vector3 pos);
    public delegate float PotentialFunction(Vector3 pos);
    public interface IFieldLineRenderer
    {
        /// <summary>
        /// 设置增量
        /// </summary>
        /// <param name="delta"></param>
        void SetDelta(float delta);
        /// <summary>
        /// 设置最小距离
        /// </summary>
        /// <param name="minDistance"></param>
        void SetMinDistance(float minDistance);
        /// <summary>
        /// 设置最大距离
        /// </summary>
        /// <param name="maxDistance"></param>
        void SetMaxDistance(float maxDistance);
        /// <summary>
        /// 设置所有电荷位置
        /// </summary>
        /// <param name="ElectricsPos"></param>
        void SetElectricsList(List<Transform> ElectricsList);
        /// <summary>
        /// 设置生成电荷的位置
        /// </summary>
        /// <param name="electric"></param>
        void SetGenertateElectric(Transform electric);
        /// <summary>
        /// 设置电荷符号
        /// </summary>
        /// <param name="sign"></param>
        void SetSign(float sign);
        /// <summary>
        /// 设置绘制起点
        /// </summary>
        /// <param name="start"></param>
        void SetStart(Vector3 start);
        /// <summary>
        /// 渲染一次
        /// </summary>
        void RenderLine(IntensityFunction i_func);
        /// <summary>
        /// 出现
        /// </summary>
        void Show();
        /// <summary>
        /// 消失
        /// </summary>
        void Hide();
        /// <summary>
        /// 获取对应Gameobject
        /// </summary>
        GameObject gameObject { get; }
    }
}