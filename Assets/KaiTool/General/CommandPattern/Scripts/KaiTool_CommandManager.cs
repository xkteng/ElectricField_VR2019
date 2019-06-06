#define Debug
using UnityEngine;
using System.Collections.Generic;
using KaiTool.Utilities;

namespace KaiTool.CommandPattern
{
    public sealed class KaiTool_CommandManager : Singleton<KaiTool_CommandManager>
    {
        [Header("CommandManager")]
        [SerializeField]
        private List<KaiTool_BasicCommand> m_commandList = new List<KaiTool_BasicCommand>();
        [SerializeField]
        private int m_maxRecordCount = 10;
        public void ExecuteCommand(KaiTool_BasicCommand command)
        {
            command.Execute();
            if (m_commandList.Count > 0)
            {
                var lastCommmand = m_commandList[m_commandList.Count - 1];
            }
            if (m_commandList.Count < 10)
            {

                m_commandList.Add(command);
            }
            else
            {
                m_commandList.RemoveAt(0);
                m_commandList.Add(command);
            }
        }
        public void RevokeCommand()
        {
            if (m_commandList.Count > 0)
            {
                var command = m_commandList[m_commandList.Count - 1];
                command.Revoke();
                m_commandList.Remove(command);
            }
        }
#if Debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                print("Revoke!");
                RevokeCommand();
            }
        }
#endif

    }
}