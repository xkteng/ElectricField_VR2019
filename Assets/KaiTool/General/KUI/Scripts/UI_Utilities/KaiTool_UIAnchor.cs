using System;
using UnityEngine;
namespace KaiTool.UI
{
    [Flags]
    public enum AnchorDirection
    {
        NegativeXNegativeYNegativeZ = 1,
        NegativeXNegativeYZ = 2,
        XNegativeYZ = 4,
        XNegativeYNegativeZ = 8,
        NegativeXYNegativeZ = 16,
        NegativeXYZ = 32,
        XYZ = 64,
        XYNegativeZ = 128,
        Center = 256,
        X = 512,
        NegativeX = 1024,
        Y = 2048,
        NegativeY = 4096,
        Z = 8192,
        NegativeZ = 16384
    }
    public enum AnchorMode
    {
        ConstantPosition,
        RelocateWithParent
    }
    [Serializable]
    public class KaiTool_UIAnchor
    {
        [SerializeField]
        private AnchorMode anchorMode = AnchorMode.ConstantPosition;
        [SerializeField]
        private AnchorDirection m_anchorDirection;
        [SerializeField]
        private Vector3 m_relativePositon = Vector3.zero;

        public KaiTool_UIAnchor()
        {
            m_anchorDirection = AnchorDirection.Center;
            m_relativePositon = Vector3.zero;
        }
        public AnchorMode AnchorMode
        {
            get
            {
                return anchorMode;
            }

            set
            {
                anchorMode = value;
            }
        }
        public AnchorDirection AnchorDirection
        {
            get
            {
                return m_anchorDirection;
            }

            set
            {
                m_anchorDirection = value;
            }
        }

        public float X
        {
            get
            {
                return m_relativePositon.x;
            }

            set
            {
                m_relativePositon.x = value;
            }
        }

        public float Y
        {
            get
            {
                return m_relativePositon.y;
            }

            set
            {
                m_relativePositon.y = value;
            }
        }

        public float Z
        {
            get
            {
                return m_relativePositon.z;
            }

            set
            {
                m_relativePositon.z = value;
            }
        }
        public Vector3 RelativePostion
        {
            get
            {
                return m_relativePositon;
            }
            set
            {
                m_relativePositon = value;
            }
        }


    }
}