namespace Engine.Editor.UI;

public interface IEditorPopup
{
    bool IsOpen { get; }

    PopupResult Update();

    void Draw();

    void Close();
}