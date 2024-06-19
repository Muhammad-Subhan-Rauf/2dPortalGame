using Godot;
using System;

public partial class key : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}
	private void OnBodyEntered(Node body)
	{
		Player player = body.GetNodeOrNull<Player>($"../Player");
		var rootNode = GetTree().Root.GetChild(0) as level_1;

		if (body == player)
		{
			rootNode.key_collected = true;
			QueueFree();
		}
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
