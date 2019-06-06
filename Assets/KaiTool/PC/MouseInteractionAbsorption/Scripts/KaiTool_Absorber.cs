using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.PC.Absorption
{
    public class KaiTool_Absorber : KaiTool_BasicAbsorber
    {
        [SerializeField]
        private EnumFixType m_fixType = EnumFixType.Kinematic;
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
                    try
                    {
                        m_absorbingTarget.Rigidbody.isKinematic = true;
                    }
                    catch
                    {

                    }
                    break;
                case EnumFixType.FixJoint:
                    break;
            }
        }
    }
}