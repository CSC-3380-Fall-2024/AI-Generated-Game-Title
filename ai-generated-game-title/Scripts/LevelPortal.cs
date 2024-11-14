using Godot;
using System;

public partial class LevelPortal : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RigidBody2D bod = (RigidBody2D)this.GetNode("LevelBody");
		bod.Connect("body_shape_entered", new Callable(this, "LoadLevelHere"));
		LoadLevelHere();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	void LoadLevelHere(){
		GD.Print("loading boss fight");
		
	}
}
