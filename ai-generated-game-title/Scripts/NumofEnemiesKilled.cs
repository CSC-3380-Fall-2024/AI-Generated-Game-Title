using Godot;
using System;

public partial class NumofEnemiesKilled : Node
{
	[Export] public int numofenemieskilled = 0;
	[Export] PackedScene[] levels;
	Random rnd;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		numofenemieskilled = 0;
		rnd = new Random();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void LoadLevel(){
		GD.Print("trying to actually load level");
		GetTree().Root.GetNode("Game").GetNode("ActualGame").QueueFree();
		int index = rnd.Next(5);
		
		var instance = (Node2D)levels[index].Instantiate();
		GetTree().Root.GetNode("Game").AddChild((Node2D)instance);
	}
	
	void addLevelScene(Node2D level){
		GetTree().Root.GetNode("Game").AddChild(level);
	}
}
