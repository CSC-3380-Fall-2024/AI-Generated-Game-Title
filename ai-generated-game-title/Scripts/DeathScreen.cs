using Godot;
using System;

public partial class DeathScreen : Control
{
	AudioStreamPlayer2D DeathSound;
	
	public override void _Ready()
	{
		DeathSound = GetNode<AudioStreamPlayer2D>("DeathSound") as AudioStreamPlayer2D;
		DeathSound.Play();
	}

	public void _on_menu_button_button_up(){
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}
	
	public void _on_quit_button_button_up(){
		GetTree().Quit();
	}
}
