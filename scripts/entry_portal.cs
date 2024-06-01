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

	public bool is_shot = false;

	public Vector2 mouse_click_pos = new Vector2(0, 0);

	public Vector2 last_known_new_pos = new Vector2(0, 0);

	public bool Portal_stuck = false;

	public int fly_time = 0;
	
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
				if (direction == 0 && ep.direction == 0 && ep.Portal_stuck == false && Portal_stuck == false)
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
		Player player = GetNodeOrNull<Player>($"../Player");
		var rayRight = GetNode<RayCast2D>("RayRight");
		var rayLeft = GetNode<RayCast2D>("RayLeft");
		var rayRight2 = GetNode<RayCast2D>("RayRight2");
		var rayLeft2 = GetNode<RayCast2D>("RayLeft2");
		var rayDown = GetNode<RayCast2D>("RayDown");
		var rayUp = GetNode<RayCast2D>("RayUp");
		var rightDist = GetNode<RayCast2D>("RightDistance");
		var leftDist = GetNode<RayCast2D>("LeftDistance");

		

		if ((rayRight.IsColliding() || rayRight2.IsColliding()) && (rayLeft.IsColliding() || rayLeft2.IsColliding()))
		{
			
			if (fly_time <= 4)
			{
				
				Portal_stuck = true;
			}
			
		}
		else if ((rayRight.IsColliding() || rayRight2.IsColliding()) && (rayUp.IsColliding() || rayDown.IsColliding()))
		{
			
			if (fly_time <= 4)
			{
				
				Portal_stuck = true;
			}
		}
		else if ((rayLeft.IsColliding() || rayLeft2.IsColliding()) && (rayLeft.IsColliding() || rayLeft2.IsColliding()))
		{
			
			if (fly_time <= 4)
			{
				
				Portal_stuck = true;
			}
		}
		else
		{
			Portal_stuck = false;
		}


		if (rayRight.IsColliding() && rayRight.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			direction = 0;
			right_stop = true;
			is_shot = false;
			
		}
		else if (rayLeft.IsColliding() && rayLeft.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			direction = 0;
			right_stop = false;
			is_shot = false;
			

		}
		if (rayRight2.IsColliding() && rayRight2.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			direction = 0;
			right_stop = true;
			is_shot = false;
		}
		else if (rayLeft2.IsColliding() && rayLeft2.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			direction = 0;
			right_stop = false;
			is_shot = false;
			
		}
		if (rayDown.IsColliding() && rayDown.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			direction = 0;
			right_stop = true;
			is_shot = false;
			
		}
		else if (rayUp.IsColliding() && rayUp.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			direction = 0;
			right_stop = false;
			is_shot = false;
			
		}

		if (rightDist.IsColliding() && !leftDist.IsColliding())
		{
			
			right_stop = true;
		}
		else if (!rightDist.IsColliding() && leftDist.IsColliding())
		{
			
			right_stop = false;
		}
		

		if(Input.IsActionJustPressed("left_click"))
		{
			Vector2 mousePosition = GetGlobalMousePosition();
			mouse_click_pos = mousePosition;
			last_known_new_pos = UpdatePosition(player.Position,mouse_click_pos,100,(float)delta);
			be_shot_mouse(delta);
			fly_time = 0;
		}
		
		
		if (is_shot == true)
		{	
			Vector2 new_pos = last_known_new_pos;
			xPos += new_pos.X;
			yPos += new_pos.Y;
			fly_time += 1;
			//xPos += 100 * (float)delta * direction;
		}
		
		
		
		// Change the speed by altering the multiplier
		// Example: Move the scene upwards
		// yPos += 50 * delta; // Change the speed by altering the multiplier

		// Update the position
		Position = new Vector2(xPos, yPos);
	}



	public void be_shot_mouse(double delta)
	{
		Player player = GetNodeOrNull<Player>($"../Player");

		Vector2 mousePosition = GetGlobalMousePosition();


		

		if (mousePosition.X <= player.Position.X)
		{
			direction = -1;
			xPos = player.Position.X - 30;
		}
		else if (mousePosition.X > player.Position.X)
		{
			direction = 1;
			xPos = player.Position.X + 30;
		}
		
		else
		{
			direction = 1;
			xPos = player.Position.X +30;
		}
		if (player == null)
		{
			GD.Print("Cannot Find Player");
		}
		GD.Print($"{last_known_new_pos}  :  {player.Position}  :  {mousePosition}");
		yPos = player.Position.Y - (float)15;



		is_shot = true;
		
		// Calculate the angle from the delta vector
		float angle = GetAngle(player.Position,mousePosition);

		

		// Apply rotation

		Rotation = angle;

	}

	static float GetAngle(Vector2 point1, Vector2 point2)
    {
        // Calculate differences
        float a = point1.X - point2.X;
        float b = point1.Y - point2.Y;

        // Calculate arctangent and convert to float
        float arctanValue = (float)Math.Atan(b / a);
        return arctanValue;
    }



	private Vector2 UpdatePosition(Vector2 current, Vector2 target, float speed, float delta)
    {
        // Calculate the direction vector
        Vector2 dir = (target - current).Normalized();
		Vector2 new_pos = current;
		new_pos = dir*speed*delta;
		
		return new_pos;
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
