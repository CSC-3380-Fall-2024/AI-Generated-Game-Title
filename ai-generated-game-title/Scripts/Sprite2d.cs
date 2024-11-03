using Godot;
using System;

public partial class Sprite2d : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	[Export]
	public int speed = 500;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Position += Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down") * speed * Convert.ToSingle(delta);
	}
}
