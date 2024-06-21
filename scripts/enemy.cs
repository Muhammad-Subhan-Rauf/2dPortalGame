using Godot;
using System;

public partial class enemy : Area2D
{
	// Called when the node enters the scene tree for the first time.
	
	float Speed = 60f;
	float Passive_speed = 30f;
	public override void _Ready()
    {       
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
    }
	private void OnBodyEntered(Node body)
	{
		Player player = GetNode<Player>($"../Player");
		if (body == player)
		{
			
			player.loose_life();
			player.update_HUD();
		}
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		var sh_right = GetNode<RayCast2D>("short_right");
		var sh_left = GetNode<RayCast2D>("short_left");
		var right = GetNode<RayCast2D>("right");
		var left = GetNode<RayCast2D>("left");

		var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		Player player = GetNode<Player>($"../Player");

		if (right.IsColliding())
        {
            var collider = right.GetCollider();
            if (collider is Player)
            {
                MoveTowardsPlayerOnXAxis((float)delta, player);
				sprite.Play("run");
				sprite.FlipH = false;
            }
			else
			{
				sprite.Play("idle");
				passive_mode((float)delta);
				
			}
        }
		

		if (left.IsColliding())
        {
            var collider = left.GetCollider();

            if (collider is Player)
            {
                MoveTowardsPlayerOnXAxis((float)delta, player);
				sprite.Play("run");
				sprite.FlipH = true;
            }
			else
			{
				sprite.Play("idle");
				passive_mode((float)delta);
			}
        }

		if (!left.IsColliding() && !right.IsColliding())
		{
			sprite.Play("idle");
			passive_mode((float)delta);
		}
		
		

	}

	private void passive_mode(float delta)
	{
		var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var sh_right = GetNode<RayCast2D>("short_right");
		var sh_left = GetNode<RayCast2D>("short_left");
		
		if (sprite.FlipH == true)
		{
			var new_x = Passive_speed * delta * -1;   
			GlobalPosition += new Vector2(new_x, 0);	
		
		}
		else if (sprite.FlipH == false)
		{
			var new_x = Passive_speed * delta * 1;   
			GlobalPosition += new Vector2(new_x, 0);	
		
		}

		if (sh_right.IsColliding())
		{
			sprite.FlipH = true;
		}
		else if (sh_left.IsColliding())
		{
			sprite.FlipH = false;
		}
	}
	private void MoveTowardsPlayerOnXAxis(float delta, Player player)
    {
        // Calculate the direction vector from the current position to the player's position
        float directionX = player.GlobalPosition.X - GlobalPosition.X;
        
        // Normalize the direction to get a unit vector
        directionX = directionX > 0 ? 1 : -1;

        // Move the object towards the player only on the x-axis
        GlobalPosition += new Vector2(directionX * Speed * delta, 0);
    }
}
