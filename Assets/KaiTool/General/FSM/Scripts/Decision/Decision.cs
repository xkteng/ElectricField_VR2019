using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.FSM
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(KaiTool_StateController controller);
    }
}

