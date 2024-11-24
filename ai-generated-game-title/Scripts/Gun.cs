using Godot;
using Godot.NativeInterop;
using System;

public partial class Gun : Node2D
{
	[Export] PackedScene Bullet_scn;
	[Export] float Number_of_bullets = 1f;
	[Export] float Bullet_max_spread = 1f;
	[Export] float Bullet_speed = 1000f;
	[Export] float Bullets_per_second = 5f;
	[Export] int Bullet_damage = 10;
	[Export] float Bullet_hang_time = 1f;
	[Export] int Bullet_penetration = 0;

	float fire_rate;

	float time_until_fire = 0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Bullet_max_spread /= 100;
		fire_rate = 1 / Bullets_per_second;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{

		if (Input.IsActionJustPressed("shoot") && time_until_fire > fire_rate)
		{
			for (int i = 0; i < Number_of_bullets; i++)
			{
				Area2D bullet = Bullet_scn.Instantiate<Area2D>();
				// Sets how long the bullet goes before dissapearing and its damage
				Bullet bulletInstance = bullet as Bullet;
				if (bulletInstance != null)
				{
					bulletInstance.Bullet_hang_time = Bullet_hang_time;
					bulletInstance.Bullet_damage = Bullet_damage;
					bulletInstance.Bullet_penetration = Bullet_penetration;
					bulletInstance.Bullet_speed = Bullet_speed;
				}
				/*
				Creates a random number between -max spread and max spread to add inconsistancy to
				the bullet's spread.
				*/
				Random rnd = new Random();
				float Positive_or_negitive = 1;
				if (rnd.Next() % 2 == 1)
				{
					Positive_or_negitive = -1;
				}
				float Spread = Bullet_max_spread * Convert.ToSingle(rnd.NextDouble()) * 
				Positive_or_negitive;
				/*
				Adds the previous number's value to the current rotation of the gun to set the
				angle that the bullet is fired at.
				*/
				bullet.Rotation = GlobalRotation + Spread;
				//Sets the bullet's starting position to where the gun is.
				bullet.GlobalPosition = GlobalPosition;
				//Creates the Bullet baised on previous specifications.
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
