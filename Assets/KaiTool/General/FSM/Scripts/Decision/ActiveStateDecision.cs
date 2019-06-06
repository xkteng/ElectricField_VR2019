using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.FSM
{
    [CreateAssetMenu(menuName = "KaiTool/PluggableAI/Decision/ActiveStateDecision", 
        fileName = "ActiveStateDecision")]
    public class ActiveStateDecision : Decision
    {
        public override bool Decide(KaiTool_StateController controller)
        {
            return true;
        }
    }
}