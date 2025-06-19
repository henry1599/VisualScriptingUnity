namespace CharacterStudio
{
    public class LoadToolbarItem : ToolbarItem
    {
        public void OnLoadButtonClicked()
        {
            PopupManager.Instance?.PushPopup(new ShowPopupArg(ePopupType.Load_Your_Work));
        }
    }
}
