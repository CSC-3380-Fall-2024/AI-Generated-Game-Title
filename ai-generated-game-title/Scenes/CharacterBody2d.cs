using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	[Export]
	private int speed = 500;
	private int dashSpeed = 1500;
	bool canDash = true;
	bool dashing = false;

	public void GetInput()
	{
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		if(Input.IsActionJustPressed("dash") && canDash){
			dashing = true;
			canDash = false;
			var dashTimer = GetNode<Timer>("DashTimer");
			var dashCooldown = GetNode<Timer>("DashCooldown");
		}
		
		if(dashing){
			Velocity = inputDir * dashSpeed;
		} else {
			Velocity = inputDir * speed;
		}
	}
	
	public void _on_dash_timer_timeout(){
		dashing = false;
	}
	
	public void _on_dash_cooldown_timeout(){
		canDash = true;
	}

	public override void _Process(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
