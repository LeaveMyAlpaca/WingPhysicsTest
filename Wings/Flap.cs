using Godot;
using System;

public partial class Flap : Node3D
{
	[Export] Wing wing;

	public override void _Process(double delta)
	{
		RotationDegrees = Vector3.Right * wing.flapAngle;
	}
}
