using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.CommandPattern
{
    public abstract class KaiTool_BasicCommand 
    {
        internal  KaiTool_BasicCommand() {

        }
        internal abstract void Execute();
        internal abstract void Revoke();
    }
}