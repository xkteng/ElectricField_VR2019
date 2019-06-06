//***********************************************
//By Kai
//Description:渲染器
//
//
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Electricity
{
    public class ElectricsRenderer : MonoBehaviour, IElectricityRenderer
    {
        private const string FL_RENDERER_PATH = "Renderer/FieldLineRenderer";
         /// <summary>
        ///  保存所有电场线渲染器的List
        /// </summary>
        private List<IFieldLineRenderer> m_fieldRenderer_List = new List<IFieldLineRenderer>();
        /// <summary>
        /// 获取所有的电场线渲染器
        /// </summary>
        public List<IFieldLineRenderer> FieldRenderers
        {
            get
            {
                return m_fieldRenderer_List;
            }
        }
        /// <summary>
        /// 在带点体周围生成电场线渲染器
        /// </summary>
        /// <param name="eletrics">带电体</param>
        /// <param name="distance">起点与带电体的距离</param>
        /// <param name="times">从带电体向外延伸的电场线数目</param>
        /// <param name="delta">步进距离</param>
        /// <param name="max_Intensity">场强大小上限</param>
        /// <param name="min_Intensity">场强大小下限</param>
        public void CreateFieldLineRendersAroundPoints(IElectric[] eletrics, float distance, int times, float delta, float max_Intensity, float min_Intensity)
        {
            foreach (var e in eletrics)
            {
                var start_List = new List<Vector3>();
                start_List.Add(e.Position + Vector3.up * distance);
                start_List.Add(e.Position - Vector3.up * distance);
                for (int i = -times / 4 + 1; i < times / 4; i++)
                {
                    var u = ((float)i) / times * 360f;
                    for (int j = 0; j < times; j++)
                    {
                        var v = ((float)j) / times * 360f;
                        start_List.Add(e.Position + Quaternion.Euler(u, v, 0) * Vector3.forward * distance);
                    }
                }
                for (int i = 0; i < start_List.Count; i++)
                {
                    m_fieldRenderer_List.Add(CreateFieldLineRender(start_List[i], e.transform, delta,
                         max_Intensity, min_Intensity));
                }
            }
        }
        /// <summary>
        /// 删除所有渲染器
        /// </summary>
        public void DestroyAllRenders()
        {
            foreach (var item in m_fieldRenderer_List)
            {
                try
                {
                    DestroyImmediate(item.gameObject);
                }
                catch { }
            }
            m_fieldRenderer_List.Clear();
        }
        public void Show()
        {
            foreach (var item in m_fieldRenderer_List)
            {
                item.Show();
            }
        }

        public void Hide()
        {
            foreach (var item in m_fieldRenderer_List)
            {
                item.Hide();
            }
        }

        /// <summary>
        /// 渲染场线
        /// </summary>
        /// <param name="i_func">计算电场强度的委托</param>
        /// <param name="p_func">计算电场势能的委托</param>
        public void RenderFieldLines(IntensityFunction i_func, PotentialFunction p_func)
        {
            foreach (var item in m_fieldRenderer_List)
            {
                if (item == null)
                {

                }
                item.RenderLine(i_func, p_func);
            }
            //for (int i = 0; i < m_fieldRenderer_List.Count; i++)
            //{
            //    if (m_fieldRenderer_List[i] == null)
            //    {
            //        m_fieldRenderer_List.Remove(m_fieldRenderer_List[i]);
            //        i--;
            //        continue;
            //    }
            //    m_fieldRenderer_List[i].RenderLine(i_func,p_func);
            //}
        }

     

        /// <summary>
        /// 生成电场线渲染器
        /// </summary>
        /// <param name="startPoint">起点</param>
        /// <param name="parent">父物体</param>
        /// <param name="delta">步进距离</param>
        /// <param name="max_Intensity">场强最大值</param>
        /// <param name="min_intensity">场强最小值</param>
        /// <returns></returns>
        private IFieldLineRenderer CreateFieldLineRender(Vector3 startPoint, Transform parent, float delta,
            float max_Intensity, float min_intensity)
        {
            var obj = Resources.Load<GameObject>(FL_RENDERER_PATH);
            if (obj == null)
            {
                throw new System.Exception("Wrong Path :" + FL_RENDERER_PATH);
            }
            var renderer = Instantiate(obj, startPoint, Quaternion.identity, parent).GetComponent<IFieldLineRenderer>();
            renderer.SetDelta(delta);
            renderer.SetMaximumIntensity(max_Intensity);
            renderer.SetMinimumIntensity(min_intensity);
            return renderer;
        }
    }
}