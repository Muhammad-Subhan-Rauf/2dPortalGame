using Godot;
using System;

public partial class game_over : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("attack"))
		{
			GetTree().ChangeSceneToFile("res://scenes/level_1.tscn");

			GlobalVariables globalVars = GlobalVariables.GetInstance();
			globalVars.ResetVariables();

		}
	}
}
