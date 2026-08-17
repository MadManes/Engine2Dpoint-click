using Engine.Editor.Project;
using Engine.Scene.Validation;

namespace Engine.Editor.Loading;

public class EditorProjectSaver
{
    private readonly EditorProjectFileSaver _projectFileSaver;


    public EditorProjectSaver(
        EditorProjectFileSaver projectFileSaver)
    {
        _projectFileSaver = projectFileSaver;
    }


    public bool Save(
        EditorProject project)
    {
        try
        {
            EditorSceneSaver sceneSaver =
                new(
                    project.SceneDirectory
                );


            foreach(var document in project.Documents)
            {
                if(!document.IsDirty)
                    continue;

                ValidationResult result =
                    sceneSaver.Validate(
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

                sceneSaver.Save(
                    document
                );

                document.SaveCompleted();
            }


            if(project.IsProjectDirty)
            {
                if(project.FilePath == null)
                {
                    throw new InvalidOperationException(
                        "Cannot save project without a file path."
                    );
                }
            
                _projectFileSaver.Save(
                    project.Data,
                    project.FilePath
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