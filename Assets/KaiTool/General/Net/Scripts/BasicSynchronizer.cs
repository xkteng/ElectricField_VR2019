using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Net
{
    //public abstract class BasicSynchronizer : MonoBehaviour
    //{
    //    private static List<BasicSynchronizer> s_synchronizerList = new List<BasicSynchronizer>();

    //    [SerializeField]
    //    protected int m_playerID = -1;
    //    [SerializeField]
    //    protected EventType m_eventType = EventType.Null;
    //    [SerializeField]
    //    private bool m_isInited = false;
    //    public virtual void Init(int id, EventType type)
    //    {
    //        m_isInited = true;
    //        m_playerID = id;
    //        m_eventType = type;
    //    }
    //    public static void SychronizeAll()
    //    {
    //        for (int i = 0; i < s_synchronizerList.Count; i++)
    //        {
    //            if (s_synchronizerList[i].m_isInited)
    //            {
    //                s_synchronizerList[i].Sync();
    //            }
    //        }
    //    }
    //    protected abstract void Sync();
    //    protected virtual void OnEnable()
    //    {
    //        s_synchronizerList.Add(this);
    //    }
    //    protected virtual void OnDisable()
    //    {
    //        s_synchronizerList.Remove(this);
    //    }
    //    protected virtual void OnDestroy()
    //    {
    //        s_synchronizerList.Remove(this);
    //    }
    //}
}