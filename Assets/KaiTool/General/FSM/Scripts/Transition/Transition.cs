using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.FSM
{

    [Serializable]
    public class Transition
    {
        public Decision m_decition;
        public State m_trueState;
        public State m_falseState;
    }
}

