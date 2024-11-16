using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void _on_start_button_button_up(){
		GetTree().ChangeSceneToFile("res://Scenes/game.tscn");
	}
	
	public void _on_quit_button_button_up(){
		GetTree().Quit();
	}
}
