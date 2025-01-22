using Godot;
using System;

public partial class UIManager : CanvasLayer
{
	[ExportGroup("Wind")]

	[Export] LineEdit WindX;
	[Export] LineEdit WindY;
	[Export] LineEdit WindZ;
	[ExportGroup("Rotation")]
	[Export] LineEdit rotX;
	[Export] LineEdit rotY;
	[Export] LineEdit rotZ;
	[ExportGroup("AVelocity")]
	[Export] LineEdit AVelocityX;
	[Export] LineEdit AVelocityY;
	[Export] LineEdit AVelocityZ;
	[ExportGroup("Lift")]
	[Export] LineEdit LiftX;
	[Export] LineEdit LiftY;
	[Export] LineEdit LiftZ;
	[ExportGroup("Output")]
	[Export] Label AoA;
	[ExportGroup("Ref.")]
	[Export] WingsManager wingsManager;
	[Export] Wing wing;

	public override void _Process(double delta)
	{
		AoA.Text = Math.Round(wing.angleOfAttack, 1).ToString();

		wind();
		rot();
		AVelocity();
		Lift();

	}
	private void Lift()
	{
		LiftX.Text = wing.CurrentLift.X.ToString();
		LiftY.Text = wing.CurrentLift.Y.ToString();
		LiftZ.Text = wing.CurrentLift.Z.ToString();
	}
	private void AVelocity()
	{
		AVelocityX.Text = wing.airVelocity.X.ToString();
		AVelocityY.Text = wing.airVelocity.Y.ToString();
		AVelocityZ.Text = wing.airVelocity.Z.ToString();
	}
	private void wind()
	{
		if (!float.TryParse(WindX.Text, out float X)) return;
		if (!float.TryParse(WindY.Text, out float Y)) return;
		if (!float.TryParse(WindZ.Text, out float Z)) return;

		wingsManager.wind = new(X, Y, Z);
	}

	private void rot()
	{
		if (!float.TryParse(rotX.Text, out float X)) return;
		if (!float.TryParse(rotY.Text, out float Y)) return;
		if (!float.TryParse(rotZ.Text, out float Z)) return;

		wing.RotationDegrees = new(X, Y, Z);
	}

}
