using Godot;
using System;

public partial class entry_portal : Area2D
{
	public float xPos = -500;
	public float yPos = -500;
	public float direction = -1;

	public bool exitP = false;

	private float rotationSensitivity = 0.5f;

	public bool right_stop = true;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}


	private void OnBodyEntered(Node body)
	{	

		Player player = body.GetNodeOrNull<Player>($"../Player");
		exit_portal ep = GetNodeOrNull<exit_portal>($"../exit");
		if (player != null)
		{
			if (ep != null)
			{
				if (direction == 0 && ep.direction == 0)
				{
					if (ep.right_stop == true)
					{
						float new_x = ep.Get_x();
						float new_y = ep.Get_y();
						Vector2 current = new Vector2(new_x - 20, new_y);
						player.Position = current;
					}
					else
					{
						float new_x = ep.Get_x();
						float new_y = ep.Get_y();
						Vector2 current = new Vector2(new_x + 20, new_y);
						player.Position = current;
					}
				}
			}
			else
			{
				GD.Print("Exit portal not found");
			}
		}
		else
		{
			GD.Print("Player not found");
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		var rayRight = GetNode<RayCast2D>("RayRight");
		var rayLeft = GetNode<RayCast2D>("RayLeft");
		

		if (rayRight.IsColliding() && rayRight.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			direction = 0;
			right_stop = true;
		}
		else if (rayLeft.IsColliding() && rayLeft.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			direction = 0;
			right_stop = false;
		}

		if (Input.IsActionJustPressed("shoot"))
		{
			be_shot(delta);
			
		}
		else if(Input.IsActionJustPressed("left_click"))
		{
			Vector2 mousePosition = GetGlobalMousePosition();
			GD.Print(mousePosition);
			be_shot_mouse(delta);
		}

		
		xPos += 100 * (float)delta * direction;
		// Change the speed by altering the multiplier
		// Example: Move the scene upwards
		// yPos += 50 * delta; // Change the speed by altering the multiplier

		// Update the position
		Position = new Vector2(xPos, yPos);
	}

	public void be_shot(double delta)
	{
		var player = GetParent().GetNode<Player>("Player");
		var player_sprite = GetParent().GetNode<Player>("Player").GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (player_sprite.FlipH == true)
		{
			direction = -1;
		}
		else if (player_sprite.FlipH == false)
		{
			direction = 1;
		}
		else
		{
			direction = 1;
		}
		if (player == null)
		{
			GD.Print("Cannot Find Player");
		}
		xPos = player.Position.X;
		yPos = player.Position.Y - (float)15;

		

	}

	public void be_shot_mouse(double delta)
	{

		Vector2 mousePosition = GetGlobalMousePosition();
		

		

		Vector2 centerPosition = GetViewportRect().Size / 2;

            // Calculate the vector from the center to the mouse position
		Vector2 delt = (mousePosition - centerPosition) * rotationSensitivity;

		// Calculate the angle from the delta vector
		float angle = Mathf.Atan2(delt.Y, delt.X);

		// Apply rotation
		Rotation -= angle;

	}

	public float Get_x()
	{
		return xPos;
	}
	public float Get_y()
	{
		return yPos;
	}

	public void Set_x(float x)
	{
		xPos = x;
	}
	public void Set_y(float y)
	{
		yPos = y;
	}
}
