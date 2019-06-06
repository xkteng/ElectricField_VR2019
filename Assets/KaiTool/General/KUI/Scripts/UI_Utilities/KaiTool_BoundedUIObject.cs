using UnityEngine;
using KaiTool.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KaiTool.UI
{
    [Flags]
    public enum ScaleChangingDirection
    {
        X = 1, Y = 2, Z = 4
    }
    public enum BoundaryScaleMode
    {
        ConstantSize,
        ScaleWithParent
    }
    public struct BoundedUIObjectEventArgs
    {
        public Dictionary<BoundaryDirection, float> m_resizeArgsDic;
        public BoundedUIObjectEventArgs(Dictionary<BoundaryDirection, float> dic)
        {
            m_resizeArgsDic = dic;
        }
    }
    [ExecuteInEditMode]
    public class KaiTool_BoundedUIObject : KaiTool_BasicUIObject
    {
        [Header("Size")]
        [SerializeField]
        private BoundaryScaleMode m_boundaryScaleMode = BoundaryScaleMode.ConstantSize;
        [SerializeField]
        private bool m_isScalingWithBoundary = false;
        [SerializeField]
        [EnumFlags]
        private ScaleChangingDirection m_scaleChangingDirection;
        [Header("Anchor")]
        [SerializeField]
        private KaiTool_UIAnchor m_anchor = new KaiTool_UIAnchor();
        [Header("Boundary")]
        [SerializeField]
        private KaiTool_BoundedUIObject m_UIParent;
        [SerializeField]
        private KaiTool_Boundary m_boudary = new KaiTool_Boundary(new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
        private KaiTool_Boundary m_lastBoundary = null;
        #region Handle
        [Header("Handle")]
        [SerializeField]
        private KaiTool_UIBoundaryHandle m_forwardHandle;
        [SerializeField]
        private KaiTool_UIBoundaryHandle m_backwardHandle;
        [SerializeField]
        private KaiTool_UIBoundaryHandle m_leftwardHandle;
        [SerializeField]
        private KaiTool_UIBoundaryHandle m_rightwardHandle;
        [SerializeField]
        private KaiTool_UIBoundaryHandle m_upwardHandle;
        [SerializeField]
        private KaiTool_UIBoundaryHandle m_downwardHandle;
        [SerializeField]
        private KaiTool_UIBoundaryHandle[] m_verticeHandles = new KaiTool_UIBoundaryHandle[8];
        #endregion
        [Header("Mesh")]
        [SerializeField]
        private Transform m_meshObj;
        public Action<System.Object, BoundedUIObjectEventArgs> m_boundaryRelocated;
        public Action<System.Object, BoundedUIObjectEventArgs> m_boundaryResized;
        public KaiTool_BoundedUIObject ParentUIObject
        {
            get
            {
                return m_UIParent;
            }
        }
        protected override void Init()
        {
            base.Init();
            InitEvent();
        }
        private void InitEvent()
        {
            if (m_forwardHandle)
            {
                m_forwardHandle.PositionChanged += (sender, e) =>
                {
                    OnRendered(GetEventArgsByHandles(BoundaryDirection.Forward));
                };
            }
            if (m_backwardHandle)
            {
                m_backwardHandle.PositionChanged += (sender, e) =>
                {
                    OnRendered(GetEventArgsByHandles(BoundaryDirection.Backward));
                };
            }
            if (m_leftwardHandle)
            {
                m_leftwardHandle.PositionChanged += (sender, e) =>
                {
                    OnRendered(GetEventArgsByHandles(BoundaryDirection.Leftward));
                };
            }
            if (m_rightwardHandle)
            {
                m_rightwardHandle.PositionChanged += (sender, e) =>
                {
                    OnRendered(GetEventArgsByHandles(BoundaryDirection.Rightward));
                };
            }
            if (m_upwardHandle)
            {
                m_upwardHandle.PositionChanged += (sender, e) =>
                {
                    OnRendered(GetEventArgsByHandles(BoundaryDirection.Upward));
                };
            }
            if (m_downwardHandle)
            {
                m_downwardHandle.PositionChanged += (sender, e) =>
                {
                    OnRendered(GetEventArgsByHandles(BoundaryDirection.Downward));

                };
            }

            for (int i = 0; i < m_verticeHandles.Length; i++)
            {
                if (m_verticeHandles[i])
                {
                    m_verticeHandles[i].PositionChanged += (sender, e) =>
                    {
                        //Do something
                    };
                }
            }
        }
        private void Start()
        {
            m_lastBoundary = (KaiTool_Boundary)m_boudary.Clone();
            if (m_UIParent == null)
            {
                OnRendered(GetEventArgsByHandles());
            }
        }
        private BoundedUIObjectEventArgs GetEventArgsByHandles(BoundaryDirection direction)
        {
            var dic = new Dictionary<BoundaryDirection, float>();
            float temp = 0f;
            switch (direction)
            {
                case BoundaryDirection.Forward:
                    temp = Vector3.Dot((m_forwardHandle.Position - m_boudary.Center.position), transform.forward);
                    dic.Add(BoundaryDirection.Forward, temp / m_boudary.Forward);
                    break;
                case BoundaryDirection.Backward:
                    temp = Vector3.Dot((m_backwardHandle.Position - m_boudary.Center.position), -transform.forward);
                    dic.Add(BoundaryDirection.Backward, temp / m_boudary.Backward);
                    break;
                case BoundaryDirection.Leftward:
                    temp = Vector3.Dot((m_leftwardHandle.Position - m_boudary.Center.position), -transform.right);
                    dic.Add(BoundaryDirection.Leftward, temp / m_boudary.Leftward);
                    break;
                case BoundaryDirection.Rightward:
                    temp = Vector3.Dot((m_rightwardHandle.Position - m_boudary.Center.position), transform.right);
                    dic.Add(BoundaryDirection.Rightward, temp / m_boudary.Rightward);
                    break;
                case BoundaryDirection.Upward:
                    temp = Vector3.Dot((m_upwardHandle.Position - m_boudary.Center.position), transform.up);
                    dic.Add(BoundaryDirection.Upward, temp / m_boudary.Upward);
                    break;
                case BoundaryDirection.Downward:
                    temp = Vector3.Dot((m_downwardHandle.Position - m_boudary.Center.position), -transform.up);
                    dic.Add(BoundaryDirection.Downward, temp / m_boudary.Downward);
                    break;
            }
            var args = new BoundedUIObjectEventArgs(dic);
            return args;
        }
        private BoundedUIObjectEventArgs GetEventArgsByHandles()
        {
            var dic = new Dictionary<BoundaryDirection, float>();
            float temp = 0f;
            if (m_forwardHandle)
            {
                temp = Vector3.Dot((m_forwardHandle.Position - m_boudary.Center.position), transform.forward);
                dic.Add(BoundaryDirection.Forward, temp / m_boudary.Forward);
            }
            if (m_backwardHandle)
            {
                temp = Vector3.Dot((m_backwardHandle.Position - m_boudary.Center.position), -transform.forward);
                dic.Add(BoundaryDirection.Backward, temp / m_boudary.Backward);
            }
            if (m_leftwardHandle)
            {
                temp = Vector3.Dot((m_leftwardHandle.Position - m_boudary.Center.position), -transform.right);
                dic.Add(BoundaryDirection.Leftward, temp / m_boudary.Leftward);
            }
            if (m_rightwardHandle)
            {
                temp = Vector3.Dot((m_rightwardHandle.Position - m_boudary.Center.position), transform.right);
                dic.Add(BoundaryDirection.Rightward, temp / m_boudary.Rightward);
            }
            if (m_upwardHandle)
            {
                temp = Vector3.Dot((m_upwardHandle.Position - m_boudary.Center.position), transform.up);
                dic.Add(BoundaryDirection.Upward, temp / m_boudary.Upward);
            }
            if (m_downwardHandle)
            {
                temp = Vector3.Dot((m_downwardHandle.Position - m_boudary.Center.position), -transform.up);
                dic.Add(BoundaryDirection.Downward, temp / m_boudary.Downward);
            }
            var args = new BoundedUIObjectEventArgs(dic);
            return args;
        }
        private void OnRendered(BoundedUIObjectEventArgs e)
        {
            ResizeBoundaryScale(e);
            RelocatePosition(e);
            ResetMeshObj();
            if (m_boundaryScaleMode == BoundaryScaleMode.ScaleWithParent)
            {
                var count = transform.childCount;
                for (int i = 0; i < count; i++)
                {
                    var obj = transform.GetChild(i).GetComponent<KaiTool_BoundedUIObject>();
                    if (obj != null)
                    {
                        obj.OnRendered(e);
                    }
                }
            }
            m_lastBoundary = (KaiTool_Boundary)m_boudary.Clone();
        }
        private void ResetMeshObj()
        {
            if (m_isScalingWithBoundary)
            {
                if (m_lastBoundary != null && m_meshObj != null)
                {
                    m_meshObj.transform.position = m_boudary.GeometricalCenter;
                    float xlocalScale, ylocalScale, zlocalScale;
                    if ((m_scaleChangingDirection & ScaleChangingDirection.X) != 0)
                    {
                        xlocalScale = m_meshObj.localScale.x * ((m_boudary.Leftward + m_boudary.Rightward) / (m_lastBoundary.Leftward + m_lastBoundary.Rightward));
                    }
                    else
                    {
                        xlocalScale = m_meshObj.localScale.x;
                    }
                    if ((m_scaleChangingDirection & ScaleChangingDirection.Y) != 0)
                    {
                        ylocalScale = m_meshObj.localScale.y * ((m_boudary.Upward + m_boudary.Downward) / (m_lastBoundary.Upward + m_lastBoundary.Downward));
                    }
                    else
                    {
                        ylocalScale = m_meshObj.localScale.y;
                    }
                    if ((m_scaleChangingDirection & ScaleChangingDirection.Z) != 0)
                    {
                        zlocalScale = m_meshObj.localScale.z * ((m_boudary.Forward + m_boudary.Backward) / (m_lastBoundary.Forward + m_lastBoundary.Backward));
                    }
                    else
                    {
                        zlocalScale = m_meshObj.localScale.z;
                    }
                    m_meshObj.localScale = new Vector3(xlocalScale, ylocalScale, zlocalScale);
                }
            }
        }
        private void RelocatePosition(BoundedUIObjectEventArgs e)
        {
            if (m_UIParent != null)
            {
                // var vec = m_anchor.RelativePostion;
                ResetRelativePosition(e);
                Vector3 pos;
                switch (m_anchor.AnchorDirection)
                {
                    case AnchorDirection.Center:
                        pos = m_UIParent.transform.position + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                    case AnchorDirection.NegativeXNegativeYNegativeZ:
                        pos = m_UIParent.m_boudary.GetVertex(0) + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                    case AnchorDirection.NegativeXNegativeYZ:
                        pos = m_UIParent.m_boudary.GetVertex(1) + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                    case AnchorDirection.XNegativeYZ:
                        pos = m_UIParent.m_boudary.GetVertex(2) + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                    case AnchorDirection.XNegativeYNegativeZ:
                        pos = m_UIParent.m_boudary.GetVertex(3) + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                    case AnchorDirection.NegativeXYNegativeZ:
                        pos = m_UIParent.m_boudary.GetVertex(4) + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                    case AnchorDirection.NegativeXYZ:
                        pos = m_UIParent.m_boudary.GetVertex(5) + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                    case AnchorDirection.XYZ:
                        pos = m_UIParent.m_boudary.GetVertex(6) + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                    case AnchorDirection.XYNegativeZ:
                        pos = m_UIParent.m_boudary.GetVertex(7) + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                    default:
                        pos = m_UIParent.transform.position + m_UIParent.transform.rotation * m_anchor.RelativePostion;
                        transform.position = pos;
                        break;
                }
            }
            if (e.m_resizeArgsDic != null)
            {
                if (e.m_resizeArgsDic.Count != 0)
                {
                    if (m_boundaryRelocated != null)
                    {
                        m_boundaryRelocated(this, e);
                    }
                }
            }
        }
        private void ResetRelativePosition(BoundedUIObjectEventArgs e)
        {

            if (m_anchor.AnchorMode == AnchorMode.ConstantPosition)
            {
                //Do nothing
            }
            if (m_anchor.AnchorMode == AnchorMode.RelocateWithParent)
            {
                var dic = e.m_resizeArgsDic;
                var anchorDirection = m_anchor.AnchorDirection;
                if (dic != null)
                {
                    if (dic.ContainsKey(BoundaryDirection.Forward))
                    {
                        if ((anchorDirection & (AnchorDirection.Center | AnchorDirection.NegativeXNegativeYZ
                            | AnchorDirection.XNegativeYZ | AnchorDirection.NegativeXYZ | AnchorDirection.XYZ | AnchorDirection.Z)) != 0)
                        {
                            m_anchor.Z = dic[BoundaryDirection.Forward] * m_anchor.Z;
                        }
                    }
                    if (dic.ContainsKey(BoundaryDirection.Backward))
                    {
                        if ((anchorDirection & (AnchorDirection.NegativeXNegativeYNegativeZ | AnchorDirection.XNegativeYNegativeZ
                            | AnchorDirection.NegativeXYNegativeZ | AnchorDirection.XYNegativeZ | AnchorDirection.NegativeZ)) != 0)
                        {
                            m_anchor.Z = dic[BoundaryDirection.Backward] * m_anchor.Z;
                        }
                    }
                    if (dic.ContainsKey(BoundaryDirection.Leftward))
                    {
                        if ((anchorDirection & (AnchorDirection.Center | AnchorDirection.NegativeXNegativeYNegativeZ
                            | AnchorDirection.NegativeXYNegativeZ | AnchorDirection.NegativeXNegativeYZ | AnchorDirection.NegativeXYZ | AnchorDirection.NegativeX)) != 0)
                        {
                            m_anchor.X = dic[BoundaryDirection.Leftward] * m_anchor.X;
                        }
                    }
                    if (dic.ContainsKey(BoundaryDirection.Rightward))
                    {
                        if ((anchorDirection & (AnchorDirection.XNegativeYNegativeZ | AnchorDirection.XYNegativeZ
                            | AnchorDirection.XNegativeYZ | AnchorDirection.XYZ | AnchorDirection.X)) != 0)
                        {
                            m_anchor.X = dic[BoundaryDirection.Rightward] * m_anchor.X;
                        }
                    }
                    if (dic.ContainsKey(BoundaryDirection.Upward))
                    {
                        if ((anchorDirection & (AnchorDirection.Center | AnchorDirection.NegativeXYNegativeZ
                            | AnchorDirection.NegativeXYZ | AnchorDirection.XYNegativeZ | AnchorDirection.XYZ | AnchorDirection.Y)) != 0)
                        {
                            m_anchor.Y = dic[BoundaryDirection.Upward] * m_anchor.Y;
                        }
                    }
                    if (dic.ContainsKey(BoundaryDirection.Downward))
                    {
                        if ((anchorDirection & (AnchorDirection.NegativeXNegativeYNegativeZ | AnchorDirection.NegativeXNegativeYZ
                            | AnchorDirection.XNegativeYNegativeZ | AnchorDirection.XNegativeYZ | AnchorDirection.NegativeY)) != 0)
                        {
                            m_anchor.Y = dic[BoundaryDirection.Downward] * m_anchor.Y;
                        }
                    }
                }
            }
        }
        private void ResizeBoundaryScale(BoundedUIObjectEventArgs e)
        {
            if (m_boundaryScaleMode == BoundaryScaleMode.ScaleWithParent)
            {
                if (m_lastBoundary != null)
                {
                    var dic = e.m_resizeArgsDic;
                    if (dic != null)
                    {
                        if (dic.ContainsKey(BoundaryDirection.Forward))
                        {
                            m_boudary.Forward *= dic[BoundaryDirection.Forward];
                        }
                        if (dic.ContainsKey(BoundaryDirection.Backward))
                        {
                            m_boudary.Backward *= dic[BoundaryDirection.Backward];
                        }
                        if (dic.ContainsKey(BoundaryDirection.Leftward))
                        {
                            m_boudary.Leftward *= dic[BoundaryDirection.Leftward];
                        }
                        if (dic.ContainsKey(BoundaryDirection.Rightward))
                        {
                            m_boudary.Rightward *= dic[BoundaryDirection.Rightward];
                        }
                        if (dic.ContainsKey(BoundaryDirection.Upward))
                        {
                            m_boudary.Upward *= dic[BoundaryDirection.Upward];
                        }
                        if (dic.ContainsKey(BoundaryDirection.Downward))
                        {
                            m_boudary.Downward *= dic[BoundaryDirection.Downward];
                        }
                    }


                }
                if (m_boundaryResized != null)
                {
                    m_boundaryResized(this, e);
                }
            }
        }
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if (m_isDrawGizmos)
            {
                m_boudary.DrawGizmos(m_gizmosColor);
            }
        }
        public override void Reset()
        {
            base.Reset();
            m_boudary.Center = transform;
            RestParent();
        }
        protected override void OnTransformParentChanged()
        {
            base.OnTransformParentChanged();
            RestParent();
        }
        protected override void OnValidate()
        {
            base.OnValidate();
            // var args = new BoundedUIObjectEventArgs();
            // print("Onvalidate");
            OnRendered(GetEventArgsByHandles());
        }
        private void RestParent()
        {
            var transformParent = transform.parent;
            if (transformParent)
            {
                m_UIParent = transformParent.GetComponentInParent<KaiTool_BoundedUIObject>();
            }
            else
            {
                m_UIParent = null;
            }
        }

    }
}