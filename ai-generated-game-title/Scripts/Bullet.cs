using Godot;
using System;

public partial class Bullet : RigidBody2D
{
	[Export] public float Bullet_hang_time = 1f;
	
	public override void _Ready()
	{
		Timer timer = new Timer();
		AddChild(timer);
		
		timer.WaitTime = Bullet_hang_time;
		timer.Timeout += () => QueueFree();
		timer.OneShot = true;
		timer.Start();
	}
}
