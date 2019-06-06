using System.Collections;
using UnityEngine;
using KaiTool.Utilities;
using VRTK;
using KaiTool.PC.Absorption;

namespace KaiTool.VR.Absorption
{


    public class KaiTool_VR_GenerializedAbsorber : KaiTool_VR_BasicAbsorber
    {
        [SerializeField]
        protected EnumFixType m_fixType;
        protected override void Init()
        {
            base.Init();
            InitEvent();
        }
        private void InitEvent()
        {

        }
        protected override void Fix()
        {
            switch (m_fixType)
            {
                case EnumFixType.Kinematic:
                    m_absorbingTarget.Rigidbody.isKinematic = true;
                    break;
                case EnumFixType.FixJoint:
                    break;
            }
        }
        protected override void UnFix()
        {
            switch (m_fixType)
            {
                case EnumFixType.Kinematic:
                    m_absorbingTarget.Rigidbody.isKinematic = false;
                    break;
                case EnumFixType.FixJoint:
                    break;
            }
        }
    }
}