//***********************************************
//By Kai
//Description��
//
//
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Electricity
{
    public interface IElectricityRenderer
    {
        /// <summary>
        /// 所有场线渲染器
        /// </summary>
        List<IFieldLineRenderer> FieldRenderers
        {
            get;
        }
        /// <summary>
        /// 在Electrics周围创建场线渲染器
        /// </summary>
        /// <param name="eletrics">电荷们</param>
        /// <param name="delta">步进距离</param>
        /// <param name="max_Intensity">最大场强</param>
        /// <param name="min_Intensity">最小场强</param>
        void CreateFieldLineRendersAroundPoints(IElectric[] eletrics, float distance, int times, float delta,
            float max_Intensity, float min_Intensity);
        /// <summary>
        /// 删除所有的渲染器
        /// </summary>
        void DestroyAllRenders();
        /// <summary>
        /// 渲染一次
        /// </summary>
        void RenderFieldLines(IntensityFunction i_func, PotentialFunction p_func);

        void Show();
        void Hide();
    }
}