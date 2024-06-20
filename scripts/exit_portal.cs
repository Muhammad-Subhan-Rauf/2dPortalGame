using Godot;
using System;

public partial class exit_portal : Area2D
{
	public float xPos = -500;
	public float yPos = -500;
	public float direction = -1;

	public bool get_shot = false;

	public bool right_stop = true;

	public bool is_shot = false;

	public Vector2 mouse_click_pos = new Vector2(0, 0);

	public Vector2 last_known_new_pos = new Vector2(0, 0);

	public bool Portal_stuck = false;

	public int fly_time = 0;

	public Vector2 default_pos = new Vector2(-500,-500);

	public float portal_time = (float)0.5; 
	public bool portal_entered = false;

	public bool angle_set = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}

	public Vector2 give_vector(float new_x,float new_y)
	{
		PortalFollower follower = GetNodeOrNull<PortalFollower>($"../exit/follow");
		return follower.calc_output(new_x,new_y);
	}
	private void OnBodyEntered(Node body)
	{	

		Player player = body.GetNodeOrNull<Player>($"../Player");
		
		entry_portal ep = GetNodeOrNull<entry_portal>($"../entry");

		PortalFollower follower = GetNodeOrNull<PortalFollower>($"../exit/follow");

		if (player != null)
		{
			if (ep != null)
			{
				if (direction == 0 && ep.direction == 0 && ep.Portal_stuck == false && Portal_stuck == false)
				{
					if (portal_entered == false && ep.portal_entered==false)
					{
						if (ep.right_stop == true)
						{
							float new_x = ep.Get_x();
							float new_y = ep.Get_y();
							Vector2 current = ep.give_vector(new_x,new_y);
							player.Position = current;
							portal_entered = true;
						}
						else
						{
							float new_x = ep.Get_x();
							float new_y = ep.Get_y();
							Vector2 current = ep.give_vector(new_x,new_y);
							player.Position = current;
							portal_entered = true;
						}
					}
				}
			}
			else
			{
				GD.Print("Entry portal not found");
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
		PortalFollower follower = GetNodeOrNull<PortalFollower>($"../exit/follow");


		var player_sprite = player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var fairy = player.GetNode<AnimatedSprite2D>("AnimatedSprite2D2");

		var fairy_1 = fairy.GetNode<RayCast2D>("r1");
		var fairy_2 = fairy.GetNode<RayCast2D>("r2");
		var fairy_3 = fairy.GetNode<RayCast2D>("r3");
		var fairy_4 = fairy.GetNode<RayCast2D>("r4");


		
		var rayRight = GetNode<RayCast2D>("RayRight");
		var rayLeft = GetNode<RayCast2D>("RayLeft");
		var rayRight2 = GetNode<RayCast2D>("RayRight2");
		var rayLeft2 = GetNode<RayCast2D>("RayLeft2");
		var rayDown = GetNode<RayCast2D>("RayDown");
		var rayUp = GetNode<RayCast2D>("RayUp");
		var rightDist = GetNode<RayCast2D>("RightDistance");
		var leftDist = GetNode<RayCast2D>("LeftDistance");



		if (portal_entered == true)
		{
			portal_time -= 1*(float)delta;
		}
		if (portal_time <= 0)
		{
			portal_entered = false;
			portal_time = (float)0.5;
		}




		if ((rayRight.IsColliding() || rayRight2.IsColliding()) && (rayLeft.IsColliding() || rayLeft2.IsColliding()))
		{
			if (rayRight.GetCollider() == player || rayRight2.GetCollider() == player || rayLeft.GetCollider() == player || rayLeft2.GetCollider() == player)
			{
			}
			else if (fly_time <= 4)
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
			if (!angle_set)
			{
				Rotation = follower.calc_angle();
				angle_set = true;
			}
			
		}
		else if (rayUp.IsColliding() && rayUp.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			direction = 0;
			right_stop = false;
			is_shot = false;
			if (!angle_set)
			{
				Rotation = -follower.calc_angle();
				angle_set = true;
			}
		}

		if (rightDist.IsColliding() && !leftDist.IsColliding() && rightDist.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			//Rotation = (float)0;
			right_stop = true;
			
			
			if (!angle_set)
			{
				Rotation = follower.calc_angle();
				angle_set = true;
			}
		
			
			
			
		}
		else if (!rightDist.IsColliding() && leftDist.IsColliding() && leftDist.GetCollider() != GetParent().GetNode<Player>("Player"))
		{
			
			if (!angle_set)
			{
				Rotation = follower.calc_angle();
				angle_set = true;
			}
	
			
			right_stop = false;
			
			
			
		}



		if(Input.IsActionJustPressed("right_click"))
		{
			Vector2 mousePosition = GetGlobalMousePosition();
			mouse_click_pos = mousePosition;
			
			
			last_known_new_pos = UpdatePosition(fairy.GlobalPosition,mouse_click_pos,100,(float)delta);

			if (!fairy_1.IsColliding() && !fairy_2.IsColliding() && !fairy_3.IsColliding() && !fairy_4.IsColliding())
			{
				be_shot_mouse(delta);
			}
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


		

		if (Portal_stuck == false)
		{
			Position = new Vector2(xPos, yPos);
		}
		else
		{
			xPos = default_pos.X;
			yPos = default_pos.Y;
			direction = 1;
			Position = default_pos;
		}


		if(Input.IsActionJustPressed("reset_portals"))
		{
			xPos = default_pos.X;
			yPos = default_pos.Y;
			Position = default_pos;
			direction = 1;
		}
	}



	public void be_shot_mouse(double delta)
	{
		Player player = GetNodeOrNull<Player>($"../Player");
		var player_sprite = player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var fairy = player.GetNode<AnimatedSprite2D>("AnimatedSprite2D2");

		Vector2 mousePosition = GetGlobalMousePosition();

		
		angle_set = false;
		
		if (mousePosition.X <= player.Position.X - 30)
		{
			direction = -1;
			xPos = fairy.GlobalPosition.X;
		}
		else if (mousePosition.X > player.Position.X - 30)
		{
			direction = 1;
			xPos = fairy.GlobalPosition.X;
		}
		
		else
		{
			direction = 1;
			xPos = fairy.GlobalPosition.X;
		}



		if (player == null)
		{
			GD.Print("Cannot Find Player");
		}
		
		yPos = fairy.GlobalPosition.Y ;




		is_shot = true;
		
		// Calculate the angle from the delta vector
		float angle;

		
		
		angle = GetAngle(fairy.GlobalPosition,mousePosition);
		
		

		

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
