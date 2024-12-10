using Godot;
using System;

public partial class EnemySpawner: Node2D {
	
	[Export] PackedScene[] enemy_scn;
	[Export] float eps = 1f;
	[Export] public int numofenemytypes = 1;
	Random rnd;

	float spawn_rate;

	float time_until_spawn = 0;

	public override void _Ready() {
		spawn_rate = 1 / eps;
		rnd = new Random();
	}

	public override void _PhysicsProcess(double delta) {
		if (time_until_spawn > spawn_rate) {
			Spawn();
			time_until_spawn = 0;
		} else {
			time_until_spawn += (float)delta;
		}
	}

	private void Spawn() {
		int index = rnd.Next(numofenemytypes);
		Vector2 location = this.GlobalPosition;
		Enemy enemy = (Enemy)enemy_scn[index].Instantiate();
		enemy.GlobalPosition = location;
		this.GetParent().AddChild(enemy);
	}

}
