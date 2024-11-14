using Godot;
using System;

public partial class NumofEnemiesKilled : Node
{
	[Export] public int numofenemieskilled = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		numofenemieskilled = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
