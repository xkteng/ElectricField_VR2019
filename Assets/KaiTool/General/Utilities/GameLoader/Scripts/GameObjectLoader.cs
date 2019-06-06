using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace KaiTool.Utilities
{
    public sealed class GameObjectLoader : MonoBehaviour, IGameObjectLoader
    {
        [SerializeField]
        private PrefabPathData m_prefabPathData;
        public GameObject LoadGameObject(string key, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (!m_prefabPathData.m_prefabPathDic.ContainsKey(key))
            {
                print("No such Key :" + key);
                return null;
            }
            var obj = m_prefabPathData.m_prefabPathDic[key];
            if (obj == null)
            {
                print("There is no such prefab at the path:" + key);
                return null;
            }
            return Instantiate(obj, position, rotation, parent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">预制件的键值</param>
        /// <param name="spawnPoint">基于SpawnPoint的位置和角度加载</param>
        /// <returns></returns>
        public GameObject LoadGameObject(string key, Transform spawnPoint)
        {
            if (spawnPoint == null)
            {
                throw new Exception("The spawnpoint can not be null.");
            }
            return LoadGameObject(key, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        }
    }
}