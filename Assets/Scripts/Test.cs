using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public FieldManager m_fieldManager;
    private void Awake()
    {
        m_fieldManager.StatusChanged +=OnStatusChanged;
    }
    private void OnStatusChanged()
    {
        print("Changed");
    }
}
