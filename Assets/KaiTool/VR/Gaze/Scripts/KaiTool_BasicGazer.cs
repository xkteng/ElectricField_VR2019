using KaiTool.Geometry;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.VR
{
    public class GazerUnityEvent : UnityEvent<KaiTool_BasicGazedPoint> { }
    public abstract class KaiTool_BasicGazer : MonoBehaviour
    {
        #region SERIALIZED_FIELDS
        [SerializeField]
        private float m_detectLength = 3f;
        [SerializeField]
        private Vector3 m_halfExtents = Vector3.one;
        [SerializeField]
        [HideInInspector]
        private Transform m_middlePoint;
        #endregion
        #region NONSERIALIZED_FIELDS
        public GazerUnityEvent m_enter = new GazerUnityEvent();
        public GazerUnityEvent m_stay = new GazerUnityEvent();
        public GazerUnityEvent m_exit = new GazerUnityEvent();

        //[SerializeField]
        private List<KaiTool_BasicGazedPoint> m_gazedList = new List<KaiTool_BasicGazedPoint>();
        #endregion
        #region PRIVATE&PROTECTED_METHODS
        protected virtual void OnEnter(KaiTool_BasicGazedPoint gazed)
        {
            m_enter.Invoke(gazed);
        }
        protected virtual void OnStay(KaiTool_BasicGazedPoint gazed)
        {
            m_stay.Invoke(gazed);
        }
        protected virtual void OnExit(KaiTool_BasicGazedPoint gazed)
        {
            m_exit.Invoke(gazed);
        }
        protected virtual void Awake()
        {

        }
        private void OnValidate()
        {
            if (m_middlePoint == null)
            {
                m_middlePoint = new GameObject("MiddlePoint").transform;
                m_middlePoint.SetParent(transform);
            }
            m_middlePoint.position = transform.position + transform.forward * m_detectLength / 2;
        }
        private void FixedUpdate()
        {
            Detect();
        }
        private void OnDrawGizmos()
        {
            var cube = new KaiTool_Box(m_middlePoint, new Vector3(m_halfExtents.x * 2, m_halfExtents.y * 2, (m_halfExtents.z + m_detectLength / 2) * 2));
            cube.DrawGizmos(Color.blue);
        }
        private void Detect()
        {
            var raycastHits = Physics.BoxCastAll(transform.position, m_halfExtents, transform.forward, transform.rotation, m_detectLength);
            if (raycastHits.Length > 0)
            {
                var temp_list = new List<KaiTool_BasicGazedPoint>();
                foreach (var item in raycastHits)
                {
                    var gazed = item.transform.GetComponent<KaiTool_BasicGazedPoint>();
                    if (gazed)
                    {
                        temp_list.Add(gazed);
                    }
                }
                var intersection = m_gazedList.Intersect(temp_list);
                var old_difference = m_gazedList.Except(intersection);
                var new_difference = temp_list.Except(intersection);
                foreach (var item in intersection)
                {
                    item.OnGazerStay(this);
                    OnStay(item);
                }
                foreach (var item in old_difference)
                {
                    item.OnGazerExit(this);
                    OnExit(item);
                }
                foreach (var item in new_difference)
                {
                    item.OnGazerEnter(this);
                    OnEnter(item);
                }
                m_gazedList = temp_list;
            }
            else
            {
                foreach (var item in m_gazedList)
                {
                    item.OnGazerExit(this);
                    OnExit(item);
                }
                m_gazedList.Clear();
            }
        }
        #endregion
    }
}