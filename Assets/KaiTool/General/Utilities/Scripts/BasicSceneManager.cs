using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Utilities
{
    public abstract class BasicSceneManager : GameEventMonoBehaviour
    {
        [SerializeField]
        private SpawnPointData m_spawn;
        protected override void Awake()
        {
            base.Awake();
            SpawnObj();
        }
        private void SpawnObj()
        {
            foreach (var item in m_spawn.m_infos)
            {
                Manager.Instance.LoadGameObject(item.m_key,item.m_position,Quaternion.Euler(item.m_rotation),transform);
            }
        }
    }
}