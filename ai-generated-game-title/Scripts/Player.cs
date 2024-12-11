using Godot;
using System;

public partial class Player : CharacterBody2D
{
	
	[Export] private int speed = 500;
	[Export] private int dashSpeed = 1500;
	bool canDash = true;
	bool dashing = false;
	
	[Export] private int MAX_HEALTH = 20;
	[Export] private int health;
	
	ProgressBar HealthBar;
	Timer DashTimer;
	Timer DashCooldown;
	AnimatedSprite2D moveanims;
	AudioStreamPlayer2D DashSound;
	AudioStreamPlayer2D HurtSound;
	GlobalVariables globalvars;
	
	public override void _Ready()
	{
		globalvars = (GlobalVariables)GetTree().Root.GetNode("Game").GetNode("GlobalVariables");
		InitHealth();
		DashTimer = GetNode<Timer>("DashTimer") as Timer;
		DashCooldown = GetNode<Timer>("DashCooldown") as Timer;
		DashSound = GetNode<AudioStreamPlayer2D>("DashSound") as AudioStreamPlayer2D;
		HurtSound = GetNode<AudioStreamPlayer2D>("HurtSound") as AudioStreamPlayer2D;
		moveanims = GetNode<AnimatedSprite2D>("MovementAnimations") as AnimatedSprite2D;
	}

	public void GetInput()
	{
		
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		if(inputDir[0] < 0){
			moveanims.SetAnimation("left");
		}
		else if (inputDir[0] > 0){
			moveanims.SetAnimation("right");
		}
		else if (inputDir[0] == 0 && inputDir[1] > 0){
			moveanims.SetAnimation("down");
		}
		else if (inputDir[0] == 0 && inputDir[1] < 0){
			moveanims.SetAnimation("up");
		}
		else{
			moveanims.SetAnimation("default");
		}
		
		if(Input.IsActionJustPressed("dash") && canDash){
			dashing = true;
			canDash = false;
			DashTimer.Start();
			DashCooldown.Start();
			DashSound.Play();
		}
		
		if(Input.IsActionJustPressed("ui_cancel")){
			GetTree().Quit();
		}
		
		if(dashing){
			Velocity = inputDir * dashSpeed;
		} else {
			Velocity = inputDir * speed;
		}
	}
	
	public void InitHealth(){
		health = globalvars.currenthealth;
		HealthBar = GetNode<ProgressBar>("CanvasLayer/HealthBar") as ProgressBar;
		HealthBar.MaxValue = 20;
		SetHealth();
	}
	
	public void SetHealth(){
		HealthBar.Value = health;
	}
	
	public void Damage(int amount){
		if(!dashing){
			HurtSound.Play();
			health -= amount;
			globalvars.currenthealth -= amount;
			SetHealth();
		}
	}
	
	public void Die(){
		GetTree().ChangeSceneToFile("res://Scenes/DeathScreen.tscn");
	}
	
	public void _on_dash_timer_timeout(){
		dashing = false;
	}
	
	public void _on_dash_cooldown_timeout(){
		canDash = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
		if(health <= 0){
			Die();
		}
	}
}
