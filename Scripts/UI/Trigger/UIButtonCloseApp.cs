namespace EnigmaCore.UI
{
    public class UIButtonCloseApp : CUIButton
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            ClickEvent += OnClickEvent;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ClickEvent -= OnClickEvent;
        }

        void OnClickEvent()
        {
            EApplication.Quit();
        }
    }
}