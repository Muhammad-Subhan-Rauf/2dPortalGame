using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 130.0f;
	public const float JumpVelocity = -300.0f;

	public int health = 100;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	
   

	public bool facing_left = false;



    public override void _Ready()
    {
        var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        
        if (animationPlayer == null)
        {
            GD.Print("AnimationPlayer node not found");
        }
        

    }
	// private void OnAnimationFinished(string animName)
    // {
	// 	var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

    //     if (animName == "flip")
    //     {
    //         // Start the next animation after "MoveFairy" ends
    //         animationPlayer.Play("idle_left");
    //     }
	// 	else if (animName == "flip_back")
	// 	{
	// 		animationPlayer.Play("idle");
	// 	}
    // }

    public override void _PhysicsProcess(double delta)
	
	{


		Vector2 currentPosition = GlobalPosition;






		Vector2 velocity = Velocity;

		var animSprite = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var collider = this.GetNode<CollisionShape2D>("CollisionShape2D");
		var fairy = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D2");


		
		if (fairy == null)
		{
			GD.Print("Fairy Does not Exist");
		}
		
		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
			animSprite.Play("jump");			
		}
		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor()){
			GD.Print(animSprite.Position);
			GD.Print(collider.Position);
			GD.Print(this.Position.DistanceTo(animSprite.Position));
			velocity.Y = JumpVelocity;
		}

		float direction = Input.GetAxis("move_left", "move_right");
	
		if (direction < 0)
		{
			animSprite.FlipH = true;
		}
		else if (direction > 0)
		{
			animSprite.FlipH = false;
		
		}
		
		
		if (IsOnFloor()){
			if(direction == 0){
				animSprite.Play("idle");
			}
			else{
				animSprite.Play("run");
			}
		}

		velocity.X = direction * Speed;

		Velocity = velocity;
		
		entry_portal ep = GetNodeOrNull<entry_portal>($"../entry");
		
		MoveAndSlide();
		

		
		
		fairy.GlobalPosition = update_fairy(delta, 30, currentPosition, GetGlobalMousePosition());
		
		if (fairy.GlobalPosition.X <= Position.X)
		{
			fairy.FlipH = true;
		}
		else
		{
			fairy.FlipH = false;
		}
		
		
		GD.Print(fairy.GlobalPosition);
	}

	public Vector2 update_fairy(double delta, float distance, Vector2 player, Vector2 mousePos)
	{
	
        Vector2 direction = (mousePos - player).Normalized();
		
        Vector2 newPosition = player + (direction * distance)  ;
		GD.Print("____________________________");
		GD.Print($"Player:	{player}");
		GD.Print($"Direction:	{direction * distance}");
		GD.Print($"New Position:	{newPosition}");
		GD.Print("____________________________");
		

		return newPosition;

	}

	
}
