using System.Collections;
using UnityEngine;

namespace KaiTool.Net
{
    public class TransformSynchronizedObject : BasicSynchronizedObject
    {
        //private Coroutine m_coroutine;
        //protected override void SubscribeEvent()
        //{
        //   EventManager.AddListener<Vector3, Vector3, int>(m_EventType, OnSynchronized);
        //}
        //protected override void UnsubscribeEvent()
        //{
        //   EventManager.RemoveListener<Vector3, Vector3, int>(m_EventType, OnSynchronized);
        //}
        //private void OnSynchronized(Vector3 pos, Vector3 euler, int id)
        //{
        //    if (id == m_agentID)
        //    {
        //        //if (m_tweener != null)
        //        //{
        //        //    m_tweener.Kill();
        //        //}
        //        //Vector3 pos_start = transform.position;
        //        //Vector3 euler_start = transform.eulerAngles;
        //        //var euler_x_start = euler_start.x;
        //        //var euler_y_start = euler_start.y;
        //        //var euler_z_start = euler_start.z;
        //        //var t = 0f;
        //        //m_tweener = DOTween.To(() => t, (value) => t = value, 1, time).OnUpdate(() =>
        //        //{
        //        //    transform.position = Vector3.Lerp(pos_start, pos, t);
        //        //    var x = Mathf.LerpAngle(euler_x_start, euler.x, t);
        //        //    var y = Mathf.LerpAngle(euler_y_start, euler.y, t);
        //        //    var z = Mathf.LerpAngle(euler_z_start, euler.z, t);
        //        //    transform.eulerAngles = new Vector3(x, y, z);
        //        //});
        //        var time = 1.0f / YH_Client .SYNC_RATE;
        //        if (m_coroutine != null)
        //        {
        //            StopCoroutine(m_coroutine);
        //        }
        //        m_coroutine = StartCoroutine(Interpolate_Enumerator(pos, euler, time, YH_Client.SUBSTEPS));
        //    }
        //}
        //private IEnumerator Interpolate_Enumerator(Vector3 pos, Vector3 euler, float duration, int substeps)
        //{
        //    var wait = new WaitForSeconds(duration / substeps);
        //    Vector3 pos_start = transform.position;
        //    Vector3 euler_start = transform.eulerAngles;
        //    var euler_x_start = euler_start.x;
        //    var euler_y_start = euler_start.y;
        //    var euler_z_start = euler_start.z;
        //    for (int i = 0; i < substeps; i++)
        //    {
        //        var t = ((float)i + 1) / substeps;
        //        transform.position = Vector3.Lerp(pos_start, pos, t);
        //        var x = Mathf.LerpAngle(euler_x_start, euler.x, t);
        //        var y = Mathf.LerpAngle(euler_y_start, euler.y, t);
        //        var z = Mathf.LerpAngle(euler_z_start, euler.z, t);
        //        transform.eulerAngles = new Vector3(x, y, z);
        //        yield return wait;
        //    }
        //    m_coroutine = null;
        //}
    }
}
