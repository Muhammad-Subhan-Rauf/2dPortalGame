using Godot;
using System;

public partial class door : Area2D
{

	public bool door_opened = false;
	public bool player_entered = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}
	private void OnBodyEntered(Node body)
	{
		Player player = body.GetNodeOrNull<Player>($"../Player");
		
		if (door_opened)
		{
			if (body == player)
			{
				GD.Print("Player has entered");
				player_entered = true;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void open_door()
	{
		var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite.Play("open");
	}
}
