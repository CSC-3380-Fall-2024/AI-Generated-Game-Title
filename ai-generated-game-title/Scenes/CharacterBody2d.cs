using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    [Export]
    public int speed = 5;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        float tempSpeed = speed;
        Vector2 velocity = new Vector2();
        velocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down") * speed;

        MoveAndCollide(velocity);
    }
}
