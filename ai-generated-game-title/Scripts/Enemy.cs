using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	private Player Player;
	
	[Export] private float speed = 150f; 
	[Export] public int health = 10; 
	[Export] public PackedScene Bullet_scn; 
	[Export] public float ShootingInterval = 1.5f; 
	[Export] private float ChaseRange = 500f; 
	[Export] private float AttackRange = 300f;
	
	private float timeSinceLastShot = 0f; 

	public override void _Ready()
	{
		Player = GetParent().GetNode<Player>("Player");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Player != null)
		{
			float distanceToPlayer = GlobalPosition.DistanceTo(Player.GlobalPosition);

			if (distanceToPlayer <= ChaseRange)
			{
				if (distanceToPlayer <= AttackRange)
				{
					// Attack player if within attack range
					HandleAttack(delta);
				}
				else
				{
					// Chase the player if within chase range
					HandleChase(delta);
				}
			}
			else
			{
				// Stop moving if the player is out of range
				Velocity = Vector2.Zero;
			}
		}

		// Apply movement
		MoveAndSlide();
	}

	private void HandleChase(double delta)
	{
		Vector2 direction = (Player.GlobalPosition - GlobalPosition).Normalized();
		Velocity = direction * speed;
	}

	private void HandleAttack(double delta)
	{
		Velocity = Vector2.Zero;
		timeSinceLastShot += (float)delta;
		if (timeSinceLastShot >= ShootingInterval)
		{
			ShootAtPlayer();
			timeSinceLastShot = 0f;
		}
	}
	 private void ShootAtPlayer()
	{
	if (Bullet_scn != null)
	{
		Bullet bullet = (Bullet)Bullet_scn.Instantiate();
		GetParent().AddChild(bullet);
		bullet.GlobalPosition = GlobalPosition;
		bullet.GlobalRotation = (Player.GlobalPosition - GlobalPosition).Angle();
		bullet.enemyBullet = true;
		bullet.Bullet_damage = 1; // Damage dealt by the bullet
		bullet.Bullet_speed = 600f; // Speed of the bullet
		bullet.Bullet_penetration = 0; // Number of penetrations
	}
  }
}
