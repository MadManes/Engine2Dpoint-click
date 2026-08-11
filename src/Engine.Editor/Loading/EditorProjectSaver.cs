using Engine.Editor.Project;

namespace Engine.Editor.Loading;

public class EditorProjectSaver
{
    private readonly EditorSceneSaver _sceneSaver;


    public EditorProjectSaver(
        EditorSceneSaver sceneSaver)
    {
        _sceneSaver = sceneSaver;
    }

    public bool Save(
        EditorProject project)
    {
        try
        {
            foreach(var document in project.Documents)
            {
                if(!document.IsDirty)
                    continue;

                _sceneSaver.Save(
                    document
                );

                document.SaveCompleted();
            }            

            return true;
        }
        catch(Exception exception)
        {
            Console.WriteLine(
                $"Error saving project: {exception.Message}"
            );

            return false;
        }
    }


}
