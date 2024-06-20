using Godot;
using System;

public partial class level_2 : Node2D
{
	public bool door_closed1 = true;
	public bool door_closed2 = true;

	public bool key_collected = false;
	public bool door_opened = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		door_functions();
		door_and_key();


	}
	public void door_and_key()
	{
		var door = GetNode<Area2D>("door") as door;
		var door_collider = door.GetNode<CollisionShape2D>("CollisionShape2D");
		var kkey = GetNodeOrNull<Area2D>("key") as key;

		if (kkey != null)
		{
			if (kkey.key_collected)
				{
					if (!door_opened)
					{
						door.open_door();
						door_collider.Disabled = false;
						door.door_opened = true;
						door_opened = true;
					}
					kkey.QueueFree();
				}
		}
	
		if (door.player_entered == true)
		{
			GD.Print("Player has entered");
			GetTree().ChangeSceneToFile("res://scenes/level_1.tscn");
		}

	}
	public void door_functions()
	{
		var block_door1 = GetNode<StaticBody2D>("block_door") as block_door;
		var button1 = GetNode<Area2D>("Button") as Button;

		var block_door2 = GetNode<StaticBody2D>("block_door2") as block_door;
		var button2 = GetNode<Area2D>("Button2") as Button;

		if (button1.enabled)
		{	
			if (door_closed1)
			{
				block_door1.open_door();
				door_closed1 = false;
			}
			
		}
		else
		{
			if (!door_closed1)
			{
				block_door1.close_door();
				door_closed1 = true;
			}
			
		}


		if (button2.enabled)
		{	
			if (door_closed2)
			{
				block_door2.open_door();
				door_closed2 = false;
			}
			
		}
		else
		{
			if (!door_closed2)
			{
				block_door2.close_door();
				door_closed2 = true;
			}
			
		}
	}
}
