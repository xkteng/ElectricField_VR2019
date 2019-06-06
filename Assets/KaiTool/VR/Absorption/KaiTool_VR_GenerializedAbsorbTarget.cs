//#define Debug
using KaiTool.PC.Absorption;
using KaiTool.Utilities;
using UnityEngine;

namespace KaiTool.VR.Absorption
{
    public class KaiTool_VR_GenerializedAbsorbTarget : KaiTool_VR_BasicAbsorbTarget
    {
        protected override bool IsAnyMatchedAbsorberNearby(out IAbsorber nearestAbsorber)
        {
            nearestAbsorber = null;
            float nearestDistance = Mathf.Infinity;
            for (int i = 0; i < KaiTool_VR_BasicAbsorber.s_absorberList.Count; i++)
            {
                var absorber_Temp = KaiTool_VR_BasicAbsorber.s_absorberList[i];
                var dis = (absorber_Temp.transform.position - transform.position).magnitude;
                var angle = Vector3.Angle(absorber_Temp.transform.forward, transform.forward);
                if (dis < m_distanceLimit && angle < m_angularLimit && absorber_Temp.AbsorbType == AbsorbType)
                {
                    if (AbsorbName == absorber_Temp.AbsorbName)
                    {
                        if (dis < nearestDistance)
                        {
                            nearestAbsorber = absorber_Temp;
                            nearestDistance = dis;
                        }
                    }
                }
            }
            if (nearestAbsorber == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}