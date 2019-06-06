using KaiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace KaiTool.Json
{
    public class KaiTool_JsonReader
    {
        public static Dictionary<string, string> GetDictionary(string path)
        {
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var sr = new StreamReader(fs);
            var str = sr.ReadToEnd();
            var assemble = JsonUtility.FromJson<LoadDataAssemble>(str);
            var datas = assemble.m_loadDatas;
            var dictionary = new Dictionary<string, string>();
            for (int i = 0; i < datas.Length; i++)
            {
                dictionary.Add(datas[i].m_key, datas[i].m_value);
            }
            return dictionary;
        }

    }
}