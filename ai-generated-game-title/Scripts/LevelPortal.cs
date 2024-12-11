using Godot;
using System;

public partial class LevelPortal : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RigidBody2D bod = (RigidBody2D)this.GetNode("PortalBody");
		bod.Connect("sleeping_state_changed", new Callable(this, "loadNextLevel"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	void loadNextLevel(){
		GD.Print("door touched");
		RigidBody2D bod = (RigidBody2D)this.GetNode("PortalBody");
		bod.SetSleeping(true);
		GlobalVariables numkilled = (GlobalVariables)GetTree().Root.GetNode("Game").GetNode("GlobalVariables");
		if(numkilled.numofenemieskilled >= 5){
			GD.Print("trying to actually load level");
			GetTree().Root.GetNode("Game").GetNode("ActualGame").QueueFree();
			var scene = (PackedScene)GD.Load("res://Scenes/Levels/DesertLevel1.tscn");
			var instance = (Node2D)scene.Instantiate();
			CallDeferred("addNextLevelScene", (Node2D)instance);
		}
		
	}
	
	void addNextLevelScene(Node2D level){
		level.Name = "ActualGame";
		GetTree().Root.GetNode("Game").AddChild(level);
	}
}
