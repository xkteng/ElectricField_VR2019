using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.FSM
{
    [CreateAssetMenu(menuName = "KaiTool/PluggableAI/Decision/LookDecision",
        fileName = "LookDecision")]

    public class LookDecision : Decision
    {
        public override bool Decide(KaiTool_StateController controller)
        {
            return true;
        }
    }
}

