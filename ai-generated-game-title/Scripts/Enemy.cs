using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	Player Player;
	[Export] float speed = 150f;
	[Export] public int health = 10;
	[Export] public PackedScene[] Bullet_scn;
	[Export] public int numofbullettypes = 1;

	[Export] public float ShootingInterval = 1.5f; // seconds
	private float timeSinceLastShot = 0f;
	Random rnd;

	public override void _Ready()
	{
		Player = (Player)this.GetParent().GetNode("Player");
		rnd = new Random();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Player != null)
		{
			Vector2 direction = (Player.GlobalPosition - GlobalPosition).Normalized();
			Velocity = direction * speed;
			
			timeSinceLastShot += (float)delta;
			if (timeSinceLastShot >= ShootingInterval)
			{
				ShootAtPlayer();
				timeSinceLastShot = 0f;
			}
		}
		else
		{
			Velocity = Vector2.Zero;
		}
		MoveAndSlide();
	}

	private void ShootAtPlayer()
	{
		if (Bullet_scn != null && Player != null)
		{
			int bulletnum = rnd.Next(numofbullettypes);
			Bullet bullet = (Bullet)Bullet_scn[bulletnum].Instantiate();
			GetParent().AddChild(bullet);
			bullet.GlobalPosition = GlobalPosition;
			bullet.GlobalRotation = (Player.GlobalPosition - GlobalPosition).Angle();
			bullet.enemyBullet = true;
			bullet.Bullet_damage = 1; 
			bullet.Bullet_speed = 600f; 
			bullet.Bullet_penetration = 0; 
		}
	}
}
