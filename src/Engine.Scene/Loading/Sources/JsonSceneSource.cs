using Engine.Scene.Definitions;
using Engine.Scene.Serialization;
using Engine.Scene.Validation;

namespace Engine.Scene.Loading.Sources;

public class JsonSceneSource : ISceneSource
{
    private readonly string _folder;
    private readonly SceneSerializer _serializer;
    private readonly ISceneValidator _validator;

    public JsonSceneSource(
        string folder,
        SceneSerializer serializer,
        ISceneValidator validator)
    {
        _folder = folder;
        _serializer = serializer;
        _validator = validator;
    }


    public IReadOnlyCollection<string> Names
    {
        get
        {
            List<string> ids = new();

            foreach (string file in Directory.GetFiles(_folder, "*.json"))
            {
                string json =
                    File.ReadAllText(file);

                try
                {
                    SceneData data =
                        Validate(
                            _serializer.Deserialize(json)
                        );
                
                    ids.Add(data.Id);
                }
                catch (SceneValidationException)
                {
                    // Ignorar escenas inválidas.
                }
            }
                
            return ids;
        }
    }

    private SceneData Validate(SceneData data)
    {
        ValidationResult result =
            _validator.Validate(data);

        if (!result.IsValid)
        {
            throw new SceneValidationException(result);
        }

        return data;
    }


    public SceneData Load(string id)
    {
        foreach (string file in Directory.GetFiles(_folder, "*.json"))
        {
            string json =
                File.ReadAllText(file);

            SceneData data =
                Validate(
                    _serializer.Deserialize(json)
                );

            if (data.Id == id)
            {
                return data;
            }
        }

        throw new FileNotFoundException(
            $"Scene '{id}' not found."
        );
    }
}