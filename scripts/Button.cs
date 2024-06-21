using Godot;
using System;

public partial class Button : Area2D
{
	public bool enabled = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}
	private void OnBodyEntered(Node body)
	{
		var enable = GetNode<Sprite2D>("enable");
		var disable = GetNode<Sprite2D>("disable");	

		enable.Visible = true;
		disable.Visible = false;

		enabled = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var overlappingAreas = GetOverlappingAreas();

		var overlappingBodies = GetOverlappingBodies();

		if (overlappingAreas.Count == 0 && overlappingBodies.Count == 0)
		{
			var enable = GetNode<Sprite2D>("enable");
			var disable = GetNode<Sprite2D>("disable");	

			enable.Visible = false;
			disable.Visible = true;

			enabled = false;
		}

	}
}
