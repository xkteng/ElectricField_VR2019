using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.FSM
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(KaiTool_StateController controller);
    }
}

