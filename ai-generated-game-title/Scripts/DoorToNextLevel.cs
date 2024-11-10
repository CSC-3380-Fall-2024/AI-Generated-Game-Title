using Godot;
using System;

public partial class DoorToNextLevel : Node2D
{
	// Called when the node enters the scene tree for the first time.
	//an array that will hold all of the possible scenes that can be loaded
	[Export] public PackedScene[] nextScene;
	
	
	public override void _Ready()
	{
		StaticBody2D bod = this.GetNode<StaticBody2D>("Body");
		bod.Connect("mouse_entered", new Callable(this, "LoadNextLevel"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	void LoadNextLevel(){
		
		GetTree().ChangeSceneToFile("res://Scenes/testscene2.tscn");
	}
}
