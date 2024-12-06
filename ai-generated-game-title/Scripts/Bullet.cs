using Godot;
using System;

public partial class Bullet : Area2D
{
	public bool enemyBullet = false;
	public bool playerBullet = false; 
	public float Bullet_hang_time = 1f; 
	public int Bullet_damage = 0; 
	public float Bullet_speed = 1200f; 
	public int Bullet_penetration = 0; 

	private int penetration_count = 0;

	public override void _Ready()
	{
 
		Timer timer = new Timer();
		AddChild(timer);
		timer.WaitTime = Bullet_hang_time;
		timer.Timeout += () => QueueFree(); // Free the bullet after the time expires
		timer.OneShot = true;
		timer.Start();

		this.BodyEntered += shootEnemy;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 movement = new Vector2(Mathf.Cos(GlobalRotation), Mathf.Sin(GlobalRotation)) * Bullet_speed;
		GlobalPosition += movement * (float)delta;
	}

	private void shootEnemy(Node body)
	{
	   
		if (enemyBullet && body is Enemy) {
			return;
		}
		if (playerBullet && body is Player){
			return;
		}

		if (body is Enemy enemy)
		{
			enemy.health -= Bullet_damage;
			if (enemy.health <= 0)
			{
				NumofEnemiesKilled numkilled = (NumofEnemiesKilled)GetTree().Root.GetNode("Game").GetNode("NumofEnemiesKilled");
				numkilled.numofenemieskilled += 1;
				enemy.QueueFree();
			}
		}
		
		if (body is Player player)
		{
			player.Damage(Bullet_damage);

		}
		
		if (penetration_count >= Bullet_penetration)
		{
			QueueFree(); 
		}
		else
		{
			penetration_count++; 
		}
	}
}
