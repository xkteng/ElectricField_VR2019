using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Trace_DataPlayer : MonoBehaviour
{
    [SerializeField]
    private string m_path = "/Data/xx";
    private void Awake()
    {
        var list = ReadData();
        print(list.Count);
        foreach (var item in list)
        {
            print(item.m_position);
        }
    }
    public List<Trace_Data> ReadData()
    {
        var path = Application.streamingAssetsPath + m_path;
        List<Trace_Data> list;
        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            var bf = new BinaryFormatter();
            list = (List<Trace_Data>)bf.Deserialize(fs);
        }
        return list;
    }
}
