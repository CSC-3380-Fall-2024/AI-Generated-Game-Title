using Godot;
using System;

public partial class Gun : Node2D
{
	[Export] PackedScene Bullet_scn;
	[Export] float Bullets_per_second = 5f;
	[Export] float Bullets_damage = 10f;

	float fire_rate;

	float time_until_fire = 0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fire_rate = 1 / Bullets_per_second;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("shoot") && time_until_fire > fire_rate)
		{
			Bullet bullet = Bullet_scn.Instantiate<Bullet>();
			bullet.damage = (int)Bullets_damage;

			bullet.Rotation = GlobalRotation;
			bullet.GlobalPosition = GlobalPosition;

			GetTree().Root.AddChild(bullet);

			time_until_fire = 0f;
		}
		else
		{
			time_until_fire += (float)delta;
		}
	}
}
