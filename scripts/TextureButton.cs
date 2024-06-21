using Godot;
using System;

public partial class TextureButton : Godot.TextureButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("pressed", new Callable(this, nameof(OnPressed)));
	}
	
	public void OnPressed()
	{
		GD.Print("Button pressed!");
		GetTree().ChangeSceneToFile("res://scenes/level_1.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
