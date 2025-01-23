namespace Player.wings
{

    using Godot;
    [GlobalClass, Tool]
    public partial class WingConfig : Resource
    {
        [Export] public Color debugColor;
        [Export] public bool displaySize;
        [Export] public Vector3 forcesModifiers;

        // Reference: https://eaglepubs.erau.edu/introductiontoaerospaceflightvehicles/chapter/airfoil-characteristics/
        [ExportGroup("AoA")]
        [Export] public Curve liftBasedOnAoA;
        [Export] public Curve dragBasedOnAoA;
        [Export] public Curve torqueBasedOnAoA;

        [ExportGroup("flaps")]
        [Export] public float flapSizePercentage = 0;
        [Export] public Vector3 flapModifierBasedOnAxis;
        [Export] public Curve flapsValueModifierBasedOnAoA;
        [Export] public Curve flapsModifierBasedOnFlapAngle;

    }
}