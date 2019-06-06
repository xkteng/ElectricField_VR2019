using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TraceData_Recorder : MonoBehaviour
{
    [SerializeField]
    private List<Trace_Data> m_dataList = new List<Trace_Data>();
    [SerializeField]
    private float m_interval = 0.1f;

    private bool m_isRecording = false;
    private bool m_isDirty = false;
    private float m_timer = 0f;

    private Coroutine m_recordingCoroutine;
    public void Record()
    {
        if (m_recordingCoroutine != null)
        {
            StopCoroutine(m_recordingCoroutine);
        }
        m_recordingCoroutine = StartCoroutine(RecordEnumerator());
    }
    public void Pause()
    {
        m_isRecording = false;
    }
    public void Resume()
    {
        m_isRecording = true;
    }
    public void SaveData()
    {
        var now = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        using (var fs = new FileStream(Application.streamingAssetsPath + "/Data/TraceData" + now , FileMode.CreateNew, FileAccess.Write))
        {
            var bf = new BinaryFormatter();
            bf.Serialize(fs, m_dataList);
        }
    }
    private IEnumerator RecordEnumerator()
    {
        var wait = new WaitForSeconds(m_interval);
        m_isRecording = true;
        m_isDirty = true;
        while (true)
        {
            if (m_isRecording)
            {
                var data = new Trace_Data(m_timer, transform.position, transform.eulerAngles);
                m_dataList.Add(data);
                m_timer += m_interval;
            }
            yield return wait;
        }
    }

}
