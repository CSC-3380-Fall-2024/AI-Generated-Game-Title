using Godot;
using System;

public partial class WinScreen : Control
{
	AudioStreamPlayer2D WinSound;
	
	public override void _Ready()
	{
		WinSound = GetNode<AudioStreamPlayer2D>("WinSound") as AudioStreamPlayer2D;
		WinSound.Play();
	}

	public void _on_menu_button_button_up(){
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}
	
	public void _on_quit_button_button_up(){
		GetTree().Quit();
	}
}
