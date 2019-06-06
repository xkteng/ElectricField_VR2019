namespace KaiTool.UI
{
    interface IUIObject
    {
        void Show();
        void Hide();
    }
    public struct UIObjectEventArgs {
        public float m_duration;
    }
}