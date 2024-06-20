using Godot;
using System;

public partial class PortalFollower : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var por = GetParent<Area2D>();

		GlobalRotation = 0;
	
		GlobalPosition = por.GlobalPosition;
		
		
	
	}

	public float calc_angle()
	{
		var Right = GetNode<RayCast2D>("rightCast");
		var Left = GetNode<RayCast2D>("leftCast");
		var Down = GetNode<RayCast2D>("downCast");
		var Up = GetNode<RayCast2D>("upCast");

		var rcol = Right.IsColliding();
		var ucol = Up.IsColliding();
		var lcol = Left.IsColliding();
		var dcol = Down.IsColliding();


		if (Right.GetCollider() == GetParent().GetNodeOrNull<Player>($"../Player"))
		{
			
			rcol = false;
		}
		if (Left.GetCollider() == GetParent().GetNodeOrNull<Player>($"../Player"))
		{
			lcol = false;
		}
		if (Up.GetCollider() == GetParent().GetNodeOrNull<Player>($"../Player"))
		{
			ucol = false;
		}
		if (Down.GetCollider() == GetParent().GetNodeOrNull<Player>($"../Player"))
		{
			dcol = false;
		}
		

		if (rcol)
		{
			if (dcol)
			{
				return (float)0.755;
			}
			else if (ucol)
			{
				return -(float)0.755;
			}
			else
			{
				return (float)0;
			}

		}
		if (lcol)
		{
			if (dcol)
			{
				return - (float)0.755;
			}
			else if (ucol)
			{
				return (float)0.755;
			}
			else
			{
				return (float)0;
			}

		}
		else if (dcol)
		{
			return (float)1.55;
		}
		else if (ucol)
		{
			return (float)1.55;
		}
		
	return 0;
		
	}


	public Vector2 calc_output(float new_x, float new_y)
	{
		var Right = GetNode<RayCast2D>("rightCast");
		var Left = GetNode<RayCast2D>("leftCast");
		var Down = GetNode<RayCast2D>("downCast");
		var Up = GetNode<RayCast2D>("upCast");

		var rcol = Right.IsColliding();
		var ucol = Up.IsColliding();
		var lcol = Left.IsColliding();
		var dcol = Down.IsColliding();

		
		if (Right.GetCollider() == GetParent().GetNodeOrNull<Player>($"../Player"))
		{
			
			rcol = false;
		}
		if (Left.GetCollider() == GetParent().GetNodeOrNull<Player>($"../Player"))
		{
			lcol = false;
		}
		if (Up.GetCollider() == GetParent().GetNodeOrNull<Player>($"../Player"))
		{
			ucol = false;
		}
		if (Down.GetCollider() == GetParent().GetNodeOrNull<Player>($"../Player"))
		{
			dcol = false;
		}

		GD.Print($"rcol:	{rcol}");
		GD.Print($"lcol:	{lcol}");
		GD.Print($"ucol:	{ucol}");
		GD.Print($"dcol:	{dcol}");
		GD.Print("______________________");

		if (rcol)
		{
			if (dcol)
			{
				 
				return new Vector2(new_x, new_y);
			}
			else if (ucol)
			{
				return new Vector2(new_x, new_y);
			}
			else
			{
				return new Vector2(new_x, new_y);
			}

		}
		if (lcol)
		{
			if (dcol)
			{
				return new Vector2(new_x, new_y);
			}
			else if (ucol)
			{
				return new Vector2(new_x, new_y);
			}
			else
			{
				return new Vector2(new_x, new_y);
			}

		}
		else if (dcol)
		{
			return new Vector2(new_x, new_y);
		}
		else if (ucol)
		{
			return new Vector2(new_x, new_y);
		}
		
	return new Vector2(new_x, new_y);
		
	}


}
