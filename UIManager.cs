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
	[ExportGroup("LocalAVelocity")]
	[Export] LineEdit LocalAVelocityX;
	[Export] LineEdit LocalAVelocityY;
	[Export] LineEdit LocalAVelocityZ;
	[ExportGroup("Lift")]
	[Export] LineEdit LiftX;
	[Export] LineEdit LiftY;
	[Export] LineEdit LiftZ;
	[ExportGroup("Torque")]
	[Export] LineEdit TorqueX;
	[Export] LineEdit TorqueY;
	[Export] LineEdit TorqueZ;


	[ExportGroup("Single")]
	[Export] LineEdit flapAngle;
	[Export] Label AoA;
	[ExportGroup("Ref.")]
	[Export] WingsManager wingsManager;
	[Export] Wing wing;

	public override void _Process(double delta)
	{
		AoA.Text = Math.Round(wing.angleOfAttack, 1).ToString();
		if (float.TryParse(flapAngle.Text, out float angle))
			wing.flapAngle = angle;

		wind();
		rot();
		AVelocity();
		LocalAVelocity();

		Lift();
		Torque();
	}
	private void Lift()
	{
		LiftX.Text = Math.Round(wing.CurrentLift.X, 1).ToString();
		LiftY.Text = Math.Round(wing.CurrentLift.Y, 1).ToString();
		LiftZ.Text = Math.Round(wing.CurrentLift.Z, 1).ToString();
	}
	private void Torque()
	{
		TorqueX.Text = Math.Round(wing.CurrentTorque.X, 1).ToString();
		TorqueY.Text = Math.Round(wing.CurrentTorque.Y, 1).ToString();
		TorqueZ.Text = Math.Round(wing.CurrentTorque.Z, 1).ToString();
	}
	private void AVelocity()
	{
		AVelocityX.Text = Math.Round(wing.airVelocity.X, 1).ToString();
		AVelocityY.Text = Math.Round(wing.airVelocity.Y, 1).ToString();
		AVelocityZ.Text = Math.Round(wing.airVelocity.Z, 1).ToString();
	}
	private void LocalAVelocity()
	{
		LocalAVelocityX.Text = Math.Round(wing.localAirVelocity.X, 1).ToString();
		LocalAVelocityY.Text = Math.Round(wing.localAirVelocity.Y, 1).ToString();
		LocalAVelocityZ.Text = Math.Round(wing.localAirVelocity.Z, 1).ToString();
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
