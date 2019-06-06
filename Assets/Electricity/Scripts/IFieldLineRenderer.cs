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
        void SetMinimumIntensity(float min);
        void SetMaximumIntensity(float max);
        void SetStart(Vector3 start);
        /// <summary>
        /// 渲染一次
        /// </summary>
        void RenderLine(IntensityFunction i_func, PotentialFunction p_func);
        void Show();
        void Hide();
        GameObject gameObject { get; }
    }
}