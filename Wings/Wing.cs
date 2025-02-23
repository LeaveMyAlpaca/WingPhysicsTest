

using Godot;
using Player.wings;
[Tool, GlobalClass]

public partial class Wing : Node3D
{
	[Export] public Vector2 size;
	//TODO could be pre calculated
	[Export] public Resource configResource;
	public Vector3 airVelocity;
	public Vector3 liftDirection;

	private const float wingDisplayHeight = .1f;
	public override void _Process(double delta)
	{

		if (configResource is not WingConfig config)
			return;
		DebugDraw3D.Config.LineHitColor = new(252, 85, 7, .4f);
		DebugDraw3D.DebugEnabled = true;
		if (config.displaySize)
			DebugDraw3D.DrawBox(GlobalPosition, Quaternion.FromEuler(GlobalRotation)/* + Quaternion */, new(size.X, wingDisplayHeight, size.Y), config.debugColor, is_box_centered: true);
		DebugDraw3D.DrawArrow(GlobalPosition, GlobalPosition + airVelocity, color: Colors.BlanchedAlmond, arrow_size: .1f);
		DebugDraw3D.DrawArrow(GlobalPosition, GlobalPosition + SurfaceDirectionVector, color: Colors.Black, arrow_size: .1f);
		DebugDraw3D.DrawArrow(GlobalPosition, GlobalPosition + liftDirection, color: Colors.DarkBlue, arrow_size: .1f);
		/* 			DebugDraw3D.DrawLine(GlobalPosition + Vector3.Up * .2f, GlobalPosition + CurrentLift, color: Colors.Brown);
		 */
		DebugDraw3D.DrawArrow(GlobalPosition, GlobalPosition + CurrentLift, color: Colors.Aqua, arrow_size: .1f);

		DebugDraw3D.DrawArrow(GlobalPosition, GlobalPosition + CurrentDrag, color: Colors.Red, arrow_size: .1f);
		DebugDraw3D.DrawArrow(GlobalPosition, GlobalPosition + CurrentTorque, color: Colors.Yellow, arrow_size: .1f);


		DebugDraw3D.Config.LineAfterHitColor = new(252, 85, 7, .4f);
	}
	public Vector3 SurfaceDirectionVector => Quaternion * Vector3.Back;
	public Vector3 LiftDirectionModifier = Vector3.Left * Mathf.DegToRad(90);
	public Vector3 localAirVelocity;
	[Export] public float flapAngle;
	public float angleOfAttack;
	public void CalculateForces(Vector3 airVelocity, float airDensity, Vector3 centreOfMassGlobalPosition, out Vector3 forces, out Vector3 torque)
	{

		/* if (configResource is not WingConfig config)
			throw new(); */

		this.airVelocity = airVelocity;
		Vector3 dragDirection = airVelocity.Normalized();

		liftDirection = /* Quaternion.FromEuler(LiftDirectionModifier) * dragDirection */ Basis.Y;

		float area = size.X * size.Y;
		float dynamicPressure = 0.5f * airDensity * airVelocity.LengthSquared();

		localAirVelocity = GlobalTransform.Basis.Inverse() * airVelocity;
		// https://en.wikipedia.org/wiki/Lift_(force)
		if (airVelocity != Vector3.Zero)
			angleOfAttack = Mathf.RadToDeg(Mathf.Atan2(localAirVelocity.Y, localAirVelocity.Z));
		else angleOfAttack = 0;
		CalculateCoefficients(angleOfAttack, flapAngle, out float liftC, out float dragC, out float torqueC);
		Vector3 lift = liftDirection * liftC * dynamicPressure * area;
		Vector3 drag = dragDirection * dragC * dynamicPressure * area;
		forces = lift + drag;

		torque = -Basis.Z * torqueC * dynamicPressure * area * size.Y;

		var relativePosition = GlobalPosition - centreOfMassGlobalPosition;

		torque += relativePosition.Cross(forces);

		CurrentLift = lift;
		CurrentDrag = drag;
		CurrentTorque = torque;
	}

	private void CalculateCoefficients(float angleOfAttack, float flapAngle, out float liftC, out float dragC, out float torqueC)
	{
		if (configResource is not WingConfig config)
			throw new();
		// we don't need higher AoA
		angleOfAttack = Mathf.Clamp(angleOfAttack, -90, 90);

		float flapModifier = config.flapsValueModifierBasedOnAoA.SampleBaked(angleOfAttack) * config.flapsModifierBasedOnFlapAngle.SampleBaked(flapAngle);
		liftC = config.liftBasedOnAoA.SampleBaked(angleOfAttack) * config.forcesModifiers.Y + config.flapModifierBasedOnAxis.Y * flapModifier;
		dragC = config.dragBasedOnAoA.SampleBaked(angleOfAttack) * config.forcesModifiers.Z + config.flapModifierBasedOnAxis.Z * flapModifier;
		torqueC = config.torqueBasedOnAoA.SampleBaked(angleOfAttack) * config.forcesModifiers.X + config.flapModifierBasedOnAxis.X * flapModifier;


		return;
	}
	public Vector3 CurrentLift;
	public Vector3 CurrentDrag;
	public Vector3 CurrentTorque;
}



