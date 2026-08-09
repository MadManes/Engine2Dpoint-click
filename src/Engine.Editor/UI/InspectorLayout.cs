using Raylib_cs;

namespace Engine.Editor.UI;

public class InspectorLayout
{
    public const int Width = 260;

    public const int X = 1020;
    public const int Y = 40;

    public Rectangle NameField { get; }
    public Rectangle TypeField { get; }

    public int LabelX => X + 20;
    public int FieldX => X + 80;

    public InspectorLayout()
    {
        NameField =
            new Rectangle(
                FieldX,
                Y + 55,
                160,
                30
            );

        TypeField =
            new Rectangle(
                FieldX,
                Y + 95,
                160,
                30
            );
    }

    public Rectangle TypeGenericOption =>
        new Rectangle(
            FieldX,
            Y + 125,
            160,
            30
        );

    public Rectangle TypePlayerOption =>
        new Rectangle(
            FieldX,
            Y + 155,
            160,
            30
        );
}