using Engine.Editor.Project;
using Engine.Scene.Validation;

namespace Engine.Editor.Loading;

public class EditorProjectSaver
{
    private readonly EditorSceneSaver _sceneSaver;
    private readonly EditorProjectFileSaver _projectFileSaver;


    public EditorProjectSaver(
        EditorSceneSaver sceneSaver,
        EditorProjectFileSaver projectFileSaver)
    {
        _sceneSaver = sceneSaver;
        _projectFileSaver = projectFileSaver;
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

                ValidationResult result =
                    _sceneSaver.Validate(
                        document
                    );

                if(!result.IsValid)
                {
                    throw new SceneValidationException(
                        result
                    );
                }
            }


            foreach(var document in project.Documents)
            {
                if(!document.IsDirty)
                    continue;

                _sceneSaver.Save(
                    document
                );

                document.SaveCompleted();
            }


            if(project.IsProjectDirty)
            {
                _projectFileSaver.Save(
                    project.Data
                );

                project.SaveProjectCompleted();
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