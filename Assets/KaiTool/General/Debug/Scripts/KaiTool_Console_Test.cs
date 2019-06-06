using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Debugger
{
    public class KaiTool_Console_Test : MonoBehaviour
    {
        private void Start()
        {
            print("Print a text.");
            Debug.Log("Debug log a text.");
        }

    }
}