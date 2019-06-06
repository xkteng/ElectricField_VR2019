using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KaiTool.Json
{
    [Serializable]
    public class LoadData
    {
        public string m_key;
        public string m_value;
    }
    [Serializable]
    public class LoadDataAssemble
    {
        public LoadData[] m_loadDatas;
        public LoadDataAssemble(LoadData[] datas)
        {
            m_loadDatas = datas;
        }
    }
}
