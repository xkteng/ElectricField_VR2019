//***********************************************
//By Kai
//Description:Scene0 Event
//
//
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.Utilities
{

    public class LoadShapeEvent : UnityEvent<string> { }
    public class Scene0_Event
    {
       
        public static LoadShapeEvent LoadShape = new LoadShapeEvent();
        public static UnityEvent ClearGameObject = new UnityEvent();
    }
}