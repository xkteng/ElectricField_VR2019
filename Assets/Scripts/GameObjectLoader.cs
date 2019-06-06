using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR2019
{
    public class GameObjectLoader : SerializedMonoBehaviour, IGameObjectLoader
    {
        [SerializeField]
        private Dictionary<string, GameObject> m_prefabDic = new Dictionary<string, GameObject>();
        public GameObject LoadGameObject(string name, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (m_prefabDic.ContainsKey(name))
            {
                if (m_prefabDic[name]!=null)
                {
                    var temp = Instantiate(m_prefabDic[name],position,rotation,parent);
                    return temp;
                }
                else
                {
                    throw new System.Exception("The corresponding gameobject is null.");
                }
            }
            else
            {
                throw new System.Exception("There is no such key in the dictionary. key = "+name);
            }
        }
    }
}
