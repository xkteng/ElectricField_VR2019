using UnityEngine;
using KaiTool.Geometry;

namespace KaiTool.Machinery
{
    public abstract class KaiTool_BasicHingeJoint :KaiTool_BasicJoint
    {
        [Header("Hinge")]
        [SerializeField]
        protected KaiTool_Line m_aixs;
    }
}