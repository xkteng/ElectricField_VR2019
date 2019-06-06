using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR2019
{
    public interface IGameObjectLoader 
    {
        GameObject LoadGameObject(string name,Vector3 position,Quaternion rotation,Transform parent);
    }
}
