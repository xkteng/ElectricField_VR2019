using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.FSM
{
    [CreateAssetMenu(menuName = "KaiTool/PluggableAI/Decision/ScanDecision",
     fileName = "ScanDecision")]
    public class ScanDecision : Decision
    {
        public override bool Decide(KaiTool_StateController controller)
        {
            return true;
        }
    }
}

