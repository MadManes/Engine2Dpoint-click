using System.Text.Json;
using System.Text.Json.Serialization;
using Engine.Scene.Definitions;
using Engine.Scene.Serialization;

namespace Engine.Scene.Loading.Sources;

public class JsonSceneSource : ISceneSource
{
    private readonly string _folder;
    private readonly JsonSerializerOptions _options = 
        new()
        {
            PropertyNameCaseInsensitive = true
        };
    
    public JsonSceneSource(
        string folder)
    {
        _folder = folder;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        _options.Converters.Add(
            new JsonStringEnumConverter()
        );
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

                SceneData? data =
                    JsonSerializer.Deserialize<SceneData>(
                        json,
                        _options
                    );

                if (data != null)
                {
                    ids.Add(data.Id);
                }
            }
            
            return ids;
        }
    }


    public SceneData Load(string id)
    {
        string[] files =
            Directory.GetFiles(
                _folder,
                $"*.json"
            );
        
        foreach (string file in files)
        {
            string json =
                File.ReadAllText(file);

            SceneData? data =
                JsonSerializer.Deserialize<SceneData>(
                    json,
                    _options
                );

            if (data != null)
            {
                Console.WriteLine(
                    $"Archivo: {file} - Id: {data.Id} - Name: {data.Name}"
                );

                if(data.Id == id)
                {
                    return data;
                }
            }            
            
        }


        throw new FileNotFoundException(
            $"Scene '{id}' not found."
        );
        
    }
    
}