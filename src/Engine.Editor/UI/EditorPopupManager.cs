namespace Engine.Editor.UI;

public class EditorPopupManager
{
    private IEditorPopup? _currentPopup;

    public bool IsOpen =>
        _currentPopup != null &&
        _currentPopup.IsOpen;

    public void Open(
        IEditorPopup popup)
    {
        _currentPopup = popup;
    }

    public PopupResult Update()
    {
        if(_currentPopup == null)
            return PopupResult.None;

        PopupResult result =
            _currentPopup.Update();

        if(result != PopupResult.None)
        {
            _currentPopup = null;
        }

        return result;
    }

    public void Draw()
    {
        if(_currentPopup == null)
            return;

        _currentPopup.Draw();
    }

    public void Close()
    {
        if(_currentPopup == null)
            return;

        _currentPopup.Close();

        _currentPopup = null;
    }
}