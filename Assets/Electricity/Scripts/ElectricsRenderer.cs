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
        /// <param name="eletrics">所有的电荷</param>
        /// <param name="startDistance">起始绘制点和电荷的距离</param>
        /// <param name="minDistance">绘制点和电荷的最短距离</param>
        /// <param name="maxDistance">绘制点和电荷的最大距离</param>
        /// <param name="times">单个电荷生成的电场线根数</param>
        /// <param name="delta">步进长度</param>
        public void CreateFieldLineRendersAroundPoints(IElectric[] eletrics, float startDistance,float minDistance,float maxDistance, int times, float delta)
        {
            var ele_transList = new List<Transform>();
            foreach (var e in eletrics)
            {
                ele_transList.Add(e.transform);
                var start_List = new List<Vector3>();
                start_List.Add(e.Position + Vector3.up * startDistance);
                start_List.Add(e.Position - Vector3.up * startDistance);
                for (int i = -times / 4 + 1; i < times / 4; i++)
                {
                    var u = ((float)i) / times * 360f;
                    for (int j = 0; j < times; j++)
                    {
                        var v = ((float)j) / times * 360f;
                        start_List.Add(e.Position + Quaternion.Euler(u, v, 0) * Vector3.forward * startDistance);
                    }
                }
                for (int i = 0; i < start_List.Count; i++)
                {
                    m_fieldRenderer_List.Add(CreateFieldLineRender(start_List[i], e.transform, delta,
                         /*max_Intensity, min_Intensity,*/minDistance,maxDistance,e, ele_transList));
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
        public void RenderFieldLines(IntensityFunction i_func)
        {
            foreach (var item in m_fieldRenderer_List)
            {
                if (item == null)
                {

                }
                item.RenderLine(i_func);
            }
        }

       /// <summary>
       /// 生产单根电场线绘制器
       /// </summary>
       /// <param name="startPoint">起点</param>
       /// <param name="parent">父物体</param>
       /// <param name="delta">步进值</param>
       /// <param name="minDistance">最小距离</param>
       /// <param name="maxDistance">最大距离</param>
       /// <param name="genertateElectic">生成该电场线的点电荷</param>
       /// <param name="electricsList">所有的点电荷</param>
       /// <returns></returns>
        private IFieldLineRenderer CreateFieldLineRender(Vector3 startPoint, Transform parent, float delta,
           float minDistance,float maxDistance,IElectric genertateElectic, List<Transform> electricsList)
        {
            var obj = Resources.Load<GameObject>(FL_RENDERER_PATH);
            if (obj == null)
            {
                throw new System.Exception("Wrong Path :" + FL_RENDERER_PATH);
            }
            var renderer = Instantiate(obj, startPoint, Quaternion.identity, parent).GetComponent<IFieldLineRenderer>();
            renderer.SetDelta(delta);
            //renderer.SetMaximumIntensity(max_Intensity);
            //renderer.SetMinimumIntensity(min_intensity);
            renderer.SetMinDistance(minDistance);
            renderer.SetMaxDistance(maxDistance);
            renderer.SetGenertateElectric(genertateElectic.transform);
            renderer.SetSign( Mathf.Sign(genertateElectic.Quantity));
            renderer.SetElectricsList(electricsList);
            return renderer;
        }
    }
}