using Godot;

public partial class WingsManager : Node3D
{
    [Export] public float gravity;
    [Export] public Wing[] wings = null;

    [Export] public Vector3 wind;
    public override void _PhysicsProcess(double delta)
    {
        CalculateAerodynamicForces(wind, 1.2f, out Vector3 forces, out Vector3 torque);

    }

    private void CalculateAerodynamicForces(Vector3 wind, float airDensity, out Vector3 forces, out Vector3 torque)
    {
        forces = new();
        torque = new();
        foreach (var surface in wings)
        {
            surface.CalculateForces(wind, airDensity, GlobalPosition, out Vector3 _forces, out Vector3 _torque);
            forces += _forces;
            torque += _torque;
        }
    }
}