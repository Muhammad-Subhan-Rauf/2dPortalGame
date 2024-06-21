using Godot;
using System;

public partial class level_1 : Node2D
{

	public bool key_collected = false;
	public bool door_opened = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Player player = GetNode<Player>("Player");
		player.update_HUD();

		if (player.dead == true)
		{
			GetTree().ReloadCurrentScene();
		}
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
			GetTree().ChangeSceneToFile("res://scenes/level_2.tscn");
		}

	}
}
