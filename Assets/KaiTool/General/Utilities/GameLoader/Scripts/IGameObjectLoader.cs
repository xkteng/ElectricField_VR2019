//***********************************************
//By Kai
//Description: GameObjectLoader 接口
//
//
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Utilities
{
    public interface IGameObjectLoader
    {
        GameObject LoadGameObject(string key, Vector3 position, Quaternion rotation, Transform parent);
        GameObject LoadGameObject(string key, Transform parent);
    }
}