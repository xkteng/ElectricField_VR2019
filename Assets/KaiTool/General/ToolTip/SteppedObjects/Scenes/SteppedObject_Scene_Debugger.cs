using KaiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppedObject_Scene_Debugger : MonoBehaviour
{
    private void Start()
    {
        InitEvent();
        StepManager.Instance.StartFirstStep();
    }
    private void InitEvent()
    {
        StepManager.Instance.m_moveNext.AddListener(Print);
    }
    private void Print(int index)
    {
        print(string.Format("Step{0} is started.", index));
    }
}
