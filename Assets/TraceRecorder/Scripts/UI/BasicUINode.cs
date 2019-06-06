using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasicUINode : MonoBehaviour {

    private void Awake()
    {
        
    }

    private void Init() {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var item in buttons)
        {
            item.onClick.AddListener(()=> {
                RegisterButtonEvent(item.name);
            });
        }
    }
    protected abstract void RegisterButtonEvent(string name);
} 
