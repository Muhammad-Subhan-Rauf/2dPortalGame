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
		

		var Right = GetNode<RayCast2D>("rightCast");
		var Left = GetNode<RayCast2D>("leftCast");
		var Down = GetNode<RayCast2D>("downCast");
		var Up = GetNode<RayCast2D>("upCast");

		var rcol = Right.IsColliding();
		var ucol = Up.IsColliding();
		var lcol = Left.IsColliding();
		var dcol = Down.IsColliding();

	

		if (rcol)
		{
			if (dcol)
			{
				GD.Print("D-R Corner");
			}
			else if (ucol)
			{
				GD.Print("U-R Corner");
			}
			else
			{
				GD.Print("R");
			}

		}
		if (lcol)
		{
			if (dcol)
			{
				GD.Print("D-L Corner");
			}
			else if (ucol)
			{
				GD.Print("U-L Corner");
			}
			else
			{
				GD.Print("L");
			}

		}
		else if (dcol)
		{
			GD.Print("D");
		}
		else if (ucol)
		{
			GD.Print("U");
		}
		else
		{
			GD.Print("_______________");
		}


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
		else
		{
			GD.Print("_______________");
		}
	return 0;
		
	}


}
