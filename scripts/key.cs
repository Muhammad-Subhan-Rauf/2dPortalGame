using Godot;
using System;


public partial class key : Area2D
{

	public bool key_collected = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}
	private void OnBodyEntered(Node body)
	{
		Player player = body.GetNodeOrNull<Player>($"../Player");
		

		if (body == player)
		{
			key_collected = true;
			GD.Print("Key Collected");
			
		}
		
	}

	


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
