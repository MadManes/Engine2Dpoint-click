using Raylib_cs;

namespace Engine.Editor.UI;

public class InspectorLayout
{
    public const int Width = 260;
    public const int Y = 40;

    public const int FieldWidth = 160;

    public int X { get; private set; }

    public Rectangle NameField { get; private set; }
    public Rectangle TypeField { get; private set; }
    public Rectangle SpriteField { get; private set; }
    public Rectangle InteractableField { get; private set; }
    public Rectangle DoorField { get; }
    public Rectangle TargetSceneField { get; private set; }
    public Rectangle TargetSpawnField { get; private set; }
    

    public int LabelX => X + 20;
    public int FieldX => X + 80;

    public InspectorLayout()
    {
        Update(1280);

        DoorField =
            new Rectangle(
                FieldX,
                Y + 215,
                FieldWidth,
                30
            );
    }

    public void Update(int windowWidth)
    {
        X = windowWidth - Width;

        NameField =
            new Rectangle(
                FieldX,
                Y + 55,
                FieldWidth,
                30
            );

        TypeField =
            new Rectangle(
                FieldX,
                Y + 95,
                FieldWidth,
                30
            );

        SpriteField =
            new Rectangle(
                FieldX,
                Y + 135,
                FieldWidth,
                30
            );

        InteractableField =
            new Rectangle(
                FieldX,
                Y + 175,
                FieldWidth,
                30
            );

        TargetSceneField =
            new Rectangle(
                FieldX,
                Y + 255,
                FieldWidth,
                30
            );

        TargetSpawnField =
            new Rectangle(
                FieldX,
                Y + 295,
                FieldWidth,
                30
            );
    }

    public Rectangle TypeGenericOption =>
        new Rectangle(
            FieldX,
            Y + 125,
            FieldWidth,
            30
        );

    public Rectangle TypePlayerOption =>
        new Rectangle(
            FieldX,
            Y + 155,
            FieldWidth,
            30
        );



    public Rectangle GetSpriteOption(int index)
    {
        return new Rectangle(
            FieldX,
            Y + 165 + index * 30,
            FieldWidth,
            30
        );
    }



    public Rectangle InteractableYesOption =>
        new Rectangle(
            FieldX,
            Y + 205,
            FieldWidth,
            30
        );
    
    public Rectangle InteractableNoOption =>
        new Rectangle(
            FieldX,
            Y + 235,
            FieldWidth,
            30
        );

    
    public Rectangle DoorYesOption =>
        new Rectangle(
            FieldX,
            Y + 245,
            FieldWidth,
            30
        );
    
    public Rectangle DoorNoOption =>
        new Rectangle(
            FieldX,
            Y + 275,
            FieldWidth,
            30
        );

    public Rectangle GetTargetSceneOption(
        int index)
    {
        return new Rectangle(
            FieldX,
            Y + 285 + index * 30,
            FieldWidth,
            30
        );
    }

    public Rectangle GetTargetSpawnOption(
        int index)
    {
        return new Rectangle(
            FieldX,
            Y + 325 + index * 30,
            FieldWidth,
            30
        );
    }
}