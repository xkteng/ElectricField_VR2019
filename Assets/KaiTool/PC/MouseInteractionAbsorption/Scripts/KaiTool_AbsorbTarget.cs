using UnityEngine;
using System;

namespace KaiTool.PC.Absorption
{
    [RequireComponent(typeof(Rigidbody))]
    public class KaiTool_AbsorbTarget : KaiTool_BasicAbsorbTarget
    {
        protected override bool IsAnyMatchedAbsorberNearby(out IAbsorber nearestAbsorber)
        {
            nearestAbsorber = null;
            float nearestDistance = Mathf.Infinity;
            for (int i = 0; i < KaiTool_BasicAbsorber.s_absorberList.Count; i++)
            {
                var absorber_Temp = KaiTool_Absorber.s_absorberList[i];
                var dis_Temp = (absorber_Temp.transform.position - transform.position).magnitude;
                if (dis_Temp < m_absorptionRadius)
                {
                    if (absorber_Temp.AbsorbType == AbsorbType)
                    {
                        if (AbsorbName == absorber_Temp.AbsorbName)
                        {
                            if (dis_Temp < nearestDistance)
                            {
                                nearestAbsorber = absorber_Temp;
                                nearestDistance = dis_Temp;
                            }
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