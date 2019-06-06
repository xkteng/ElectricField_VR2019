using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader : MonoBehaviour
{
    private List<GameObject> m_cube_list = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            m_cube_list.Add(Load(new Vector3(1, 0, 0) * i * 2, Quaternion.identity));
        }
        DestroyImmediate(m_cube_list[2]);
    }

    private GameObject Load(Vector3 pos, Quaternion q)
    {
        var temp = Resources.Load<GameObject>("Objects/Cubes");
        return Instantiate(temp, pos, q);
    }
}
