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
	
		GlobalPosition = por.Position;
		
		
	
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


}
