using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Trace_Data
{
    public float m_time;
    public Vector3 m_position;
    public Vector3 m_euler;
    public Trace_Data(float time, Vector3 position, Vector3 euler)
    {
        m_time = time;
        m_position = position;
        m_euler = euler;
    }

}
