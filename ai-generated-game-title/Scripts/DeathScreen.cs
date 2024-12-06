using Godot;
using System;

public partial class DeathScreen : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void _on_menu_button_button_up(){
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}
	
	public void _on_quit_button_button_up(){
		GetTree().Quit();
	}
}
