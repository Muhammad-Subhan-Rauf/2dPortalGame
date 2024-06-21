using Godot;
using System;

public partial class sword : Area2D
{
	public bool attack_start = false;
	public float attack_timer = 0.2f;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {       
		Connect("area_entered", new Callable(this, nameof(OnBodyEntered)));
		
		GlobalRotation = 0;
		
		var collider = GetNode<CollisionShape2D>("CollisionShape2D");
		Visible = false;
		attack_start = false;
		attack_timer = 0.2f;
		collider.Disabled = true;
    }
	private void OnBodyEntered(Node body)
	{
		var GM = GetNode<Node>($"../GameManager") as game_manager;
		Player player = GetNode<Player>($"../Player");

		if (body.GetType().Name == "enemy")
		{
			player.score += 100;
			GM.add_score(100);
			body.QueueFree();
		}
	}
	


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Player player = GetNode<Player>($"../Player");
		var animSprite = player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var sword_anim = GetNode<AnimationPlayer>("AnimationPlayer");
		var collider = GetNode<CollisionShape2D>("CollisionShape2D");

		GlobalPosition = player.GlobalPosition;
		if (Input.IsActionJustPressed("attack")){
			attack_start = true;
			Visible = true;
			if (animSprite.FlipH == true)
			{
				sword_anim.Play("attack_2");
			}
			else
			{
				sword_anim.Play("attack");
			}
		}

		if (attack_start)
		{
			attack_timer -= 1 * (float)delta;
			collider.Disabled = false;	

			
		}

		if (attack_timer <=0)
		{
			Visible = false;
			attack_start = false;
			attack_timer = 0.2f;
			collider.Disabled = true;
			GlobalRotation = 0;

			


		}
	}
}
