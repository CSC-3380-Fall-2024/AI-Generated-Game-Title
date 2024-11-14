using Godot;
using Godot.NativeInterop;
using System;

public partial class Gun : Node2D
{
	[Export] PackedScene Bullet_scn;
	[Export] float Number_of_bullets = 1f;
	[Export] float Random_position_max = 1f;
	[Export] float Bullet_speed = 1000f;
	[Export] float Bullets_per_second = 5f;
	[Export] float Bullets_damage = 10f;
	[Export] float Bullet_hang_time = 1f;

	float fire_rate;

	float time_until_fire = 0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Random_position_max /= 100;
		fire_rate = 1 / Bullets_per_second;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("shoot") && time_until_fire > fire_rate)
		{
			for (int i = 0; i < Number_of_bullets; i++)
			{
				RigidBody2D bullet = Bullet_scn.Instantiate<RigidBody2D>();
				Bullet bulletInstance = bullet as Bullet;
				
				if (bulletInstance != null)
				{
					bulletInstance.Bullet_hang_time = Bullet_hang_time;
				}

		  	bullet.damage = (int)Bullets_damage;
				Random rnd = new Random();
				float Positive_or_negitive = 1;
				if (rnd.Next() % 2 == 1)
				{
					Positive_or_negitive = -1;
				}
				float Random_position = Random_position_max * Convert.ToSingle(rnd.NextDouble()) * Positive_or_negitive;
				bullet.Rotation = GlobalRotation + Random_position;
				bullet.GlobalPosition = GlobalPosition;
				bullet.LinearVelocity = bullet.Transform.X * Bullet_speed;
				GetTree().Root.AddChild(bullet);
			}
			time_until_fire = 0f;
		}
		else
		{
			time_until_fire += (float)delta;
		}
	}
}
