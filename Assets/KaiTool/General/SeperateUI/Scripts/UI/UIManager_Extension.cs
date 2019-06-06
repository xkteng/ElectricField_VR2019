//***********************************************
//By Kai
//Description：UIManager拓展
//
//
//***********************************************
using SeperateUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Utilities
{
    public class UIManager_Extension : UIManager, IUIController
    {
        public void OnPlayerLoadGameLevel_Start(LevelName levelName, bool useLoadingPanel)
        {
            CloseAllPanel();
            if (useLoadingPanel)
            {
                OpenPanel<Loading_Panel>();
            }
        }
        public void OnPlayerLoadGameLevel_End(LevelName levelName)
        {
            ClosePanel<Loading_Panel>();
            switch (levelName)
            {
                case LevelName.Entrance:
                    OpenPanel<Entrance_Panel>();
                    break;
                case LevelName.Scene0:
                    OpenPanel<Scene0_Panel>();
                    break;
                case LevelName.Scene1:
                    OpenPanel<Scene1_Panel>();
                    break;
                case LevelName.None:
                    break;
                default:
                    break;
            }
        }

        public void OnLoadingPanelOpened()
        {
            OpenPanel<Loading_Panel>();
        }

        public void OnLoadingPanelClosed()
        {
            ClosePanel<Loading_Panel>();
        }
    }
}