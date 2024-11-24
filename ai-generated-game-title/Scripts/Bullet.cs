using Godot;
using System;

public partial class Bullet : Area2D
{
	public bool enemyBullet = false; // Flag to differentiate player and enemy bullets
	public float Bullet_hang_time = 1f; // Lifespan of the bullet
	public int Bullet_damage = 0; // Damage caused by the bullet
	public float Bullet_speed = 1200f; // Speed of the bullet
	public int Bullet_penetration = 0; // Maximum objects the bullet can penetrate

	private int penetration_count = 0; // Tracks how many objects the bullet has penetrated

	public override void _Ready()
	{
		// Set up a timer to delete the bullet after its lifespan
		Timer timer = new Timer();
		AddChild(timer);
		timer.WaitTime = Bullet_hang_time;
		timer.Timeout += () => QueueFree(); // Free the bullet after the time expires
		timer.OneShot = true;
		timer.Start();

		// Connect the collision signal
		this.BodyEntered += OnBodyEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Move the bullet based on its rotation and speed
		Vector2 movement = new Vector2(Mathf.Cos(GlobalRotation), Mathf.Sin(GlobalRotation)) * Bullet_speed;
		GlobalPosition += movement * (float)delta;
	}

	private void OnBodyEntered(Node body)
	{
		// Prevent enemy bullets from damaging other enemies
		if (enemyBullet && body is Enemy)
			return;

		// Damage the player if hit by an enemy bullet
		if (enemyBullet && body is Player player)
		{
			player.Damage(Bullet_damage); // Use the Damage method from Player script
			QueueFree(); // Destroy the bullet after hitting the player
			return;
		}

		// Handle collision with enemies for player bullets
		if (!enemyBullet && body is Enemy enemy)
		{
			enemy.health -= Bullet_damage;
			if (enemy.health <= 0)
			{
				// Optional: Update the kill count if the enemy is destroyed
				NumofEnemiesKilled numkilled = (NumofEnemiesKilled)GetTree().Root.GetNode("Game").GetNode("NumofEnemiesKilled");
				numkilled.numofenemieskilled += 1;
				enemy.QueueFree();
			}
		}

		// Handle bullet penetration logic
		if (penetration_count >= Bullet_penetration)
		{
			QueueFree(); // Destroy the bullet if it exceeds penetration limit
		}
		else
		{
			penetration_count++; // Increment the penetration count
		}
	}
}
