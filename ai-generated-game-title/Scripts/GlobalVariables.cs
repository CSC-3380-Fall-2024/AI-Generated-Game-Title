using Godot;
using System;

public partial class GlobalVariables : Node
{
	[Export] public int numofenemieskilled = 0;
	[Export] PackedScene[] levels;
	Random rnd;
	public Node2D currentlevel;
	public int currenthealth = 20;
	public int levelsbeaten = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		numofenemieskilled = 0;
		rnd = new Random();
		currentlevel = (Node2D)GetTree().Root.GetNode("Game").GetNode("ActualGame");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void LoadLevel(){
		GD.Print("trying to actually load level");
		levelsbeaten++;
		currentlevel.QueueFree();
		if (levelsbeaten < 10){
			int index = rnd.Next(6);
		
			var instance = (Node2D)levels[index].Instantiate();
			this.currentlevel = (Node2D)instance;
			CallDeferred("addLevelScene", (Node2D)instance);
		}
		else if (levelsbeaten == 10){
			var scene = (PackedScene)GD.Load("res://Scenes/Levels/BossBattle.tscn");
			var instance = (Node2D)scene.Instantiate();
			this.currentlevel = (Node2D)instance;
			CallDeferred("addLevelScene", (Node2D)instance);
		}
		else{
			var scene = (PackedScene)GD.Load("res://Scenes/WinScreen.tscn");
			var instance = (Control)scene.Instantiate();
			CallDeferred("addWinScene", (Control)instance);
		}
	}
	
	void addLevelScene(Node2D level){
		GetTree().Root.GetNode("Game").AddChild(level);
	}
	
	void addWinScene(Control level){
		GetTree().Rootw.AddChild(level);
	}
}
