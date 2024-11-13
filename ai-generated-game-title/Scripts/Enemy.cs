using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	Player Player;
	[Export] float speed = 150f;
	[Export] public int health = 10;


	public override void _Ready(){
		Player = (Player)GetTree().Root.GetNode("Game").GetNode("Player");
	}

	public override void _Process(double delta){

	}
	public override void _PhysicsProcess(double delta) {
		if(Player != null)  {
			Vector2 direction = (Player.GlobalPosition - GlobalPosition).Normalized();
			Velocity = direction * speed;
			} else {
				Velocity = Vector2.Zero;
		}
		MoveAndSlide();
	}

}
