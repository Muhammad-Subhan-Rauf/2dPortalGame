using Godot;
using System;

public partial class pick_block : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public bool picked_by_player = false;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Player player = GetNodeOrNull<Player>($"../Player");
		var player_sprite = player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var right = GetNode<RayCast2D>("right");
		var left = GetNode<RayCast2D>("left");

		if ((right.IsColliding() && right.GetCollider() == player) || (left.IsColliding() && left.GetCollider() == player))
		{	
			
			
			if(Input.IsActionJustPressed("pick_block"))
			{
				if (!picked_by_player)
				{
					picked_by_player = true;
				}
				else
				{
					picked_by_player = false;
				}
					
			}
			
			
		}

		if (picked_by_player)
		{
			if (player_sprite.FlipH == false)
			{
				GlobalPosition = new Vector2(player.GlobalPosition.X+18,player.GlobalPosition.Y);
			}
			else
			{
			GlobalPosition = new Vector2(player.GlobalPosition.X-18,player.GlobalPosition.Y);
			}
			Position = GlobalPosition;
			
			
		}
		
		
	}
}
