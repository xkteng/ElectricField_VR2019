using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Electricity
{
    public interface IElectricityManager
    {
        void Refresh();
        void ShowFieldLine();
        void HideFieldLine();
        void Reset();
    }
}
