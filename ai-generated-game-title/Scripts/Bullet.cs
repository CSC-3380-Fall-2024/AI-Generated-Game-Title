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
	int penetration_count = 0;
	public bool poisounous = false;
	public bool explosive = false;
	public bool burning = false;
	public bool freezing = false;
	public int bounces = 0;
	GlobalVariables globalvars;
	KillstoNextLevel killsneeded;
	
	public override void _Ready()
	{
 		globalvars = (GlobalVariables)GetTree().Root.GetNode("Game").FindChild("GlobalVariables");
		killsneeded = (KillstoNextLevel)GetTree().Root.GetNode("Game").FindChild("KillsToNextLevel");
		Timer timer = new Timer();
		AddChild(timer);
		timer.WaitTime = Bullet_hang_time;
		timer.Timeout += () => QueueFree(); // Free the bullet after the time expires
		timer.OneShot = true;
		timer.Start();

		this.BodyEntered += Shoot;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 movement = new Vector2(Mathf.Cos(GlobalRotation), Mathf.Sin(GlobalRotation)) * Bullet_speed;
		GlobalPosition += movement * (float)delta;
	}

	private void Shoot(Node body)
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
				
				globalvars.numofenemieskilled += 1;
				killsneeded = (KillstoNextLevel)globalvars.currentlevel.GetNode("KillsToNextLevel");
				enemy.QueueFree();
				if (globalvars.numofenemieskilled >= killsneeded.killstoNextLevel){
					globalvars.numofenemieskilled = 0;
					globalvars.CallDeferred("LoadLevel");
				}
				
			}
		} else if (body is Player player)
		{
			player.Damage(Bullet_damage);
		} else {
			QueueFree();
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
