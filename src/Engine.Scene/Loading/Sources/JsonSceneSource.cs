using System.Text.Json;
using System.Text.Json.Serialization;
using Engine.Scene.Definitions;

namespace Engine.Scene.Loading.Sources;

public class JsonSceneSource : ISceneSource
{
    private readonly string _folder;
    private readonly JsonSerializerOptions _options;
    
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
            return Directory
                .GetFiles(
                    _folder,
                    "*.json")
                .Select(
                    file => Path.GetFileNameWithoutExtension(file)!
                )
                .ToList();
        }
    }


    public SceneData Load(string name)
    {
        string path =
            Path.Combine(
                _folder,
                $"{name}.json"
            );


        if (!File.Exists(path))
        {
            throw new FileNotFoundException(
                $"Scene '{name}' not found."
            );
        }


        string json =
            File.ReadAllText(path);



        //Console.WriteLine(path);
        //Console.WriteLine("----------------");
        //Console.WriteLine(json);
        //Console.WriteLine("----------------");


        SceneData? data =
            JsonSerializer.Deserialize<SceneData>(
                json,
                _options
            );

        Console.WriteLine($"Name: '{data?.Name}'");
        Console.WriteLine($"SpawnPoints: {data?.SpawnPoints.Count}");
        Console.WriteLine($"Entities: {data?.EntityCollection.Entities.Count}");

        if (data == null)
        {
            throw new InvalidOperationException(
                "Invalid scene data."
            );
        }

        
        return data;
    }
    
}