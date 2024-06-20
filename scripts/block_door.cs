using Godot;
using System;

public partial class block_door : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var coll = GetNode<CollisionShape2D>("CollisionShape2D");

	}

	public void open_door()
	{
		var anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var coll = GetNode<CollisionShape2D>("CollisionShape2D");

		anim.Play("opening");
		coll.Disabled = true;
	}
	public void close_door()
	{
		var anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var coll = GetNode<CollisionShape2D>("CollisionShape2D");

		anim.Play("closing");
		coll.Disabled = false;
	}
}
