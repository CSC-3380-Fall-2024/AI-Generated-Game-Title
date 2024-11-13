using Godot;
using System;

public partial class Bullet : Area2D
{
	public int damage = 0;
	public float Bullet_speed = 1200f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += shootEnemy;
		Timer timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
	}
	public override void _PhysicsProcess(double delta) {
		Vector2 movement = new Vector2(Mathf.Cos(GlobalRotation), Mathf.Sin(GlobalRotation)) * Bullet_speed;
		GlobalPosition += movement * (float)delta;
	}
	private void shootEnemy(Node body) {
		if (body is Enemy enemy) {
			enemy.health -= damage;
			if (enemy.health <= 0) {
				enemy.QueueFree();
			}
		}
		QueueFree();
	}
}