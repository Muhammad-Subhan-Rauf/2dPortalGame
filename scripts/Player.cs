using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 130.0f;
	public const float JumpVelocity = -300.0f;

	public int health = 100;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	public float fairy_X = 30;
    private AnimatedSprite2D fairy;

	public bool facing_left = false;



    public override void _Ready()
    {
        var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        
        if (animationPlayer == null)
        {
            GD.PrintErr("AnimationPlayer node not found");
        }
        else
        {
            // Connect the signal in code if not done via the editor
            animationPlayer.Connect("animation_finished", new Callable(this, nameof(OnAnimationFinished)));
            
            // Start the first animation
            animationPlayer.Play("idle");
        }

    }
	private void OnAnimationFinished(string animName)
    {
		var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        if (animName == "flip")
        {
            // Start the next animation after "MoveFairy" ends
            animationPlayer.Play("idle_left");
        }
		else if (animName == "flip_back")
		{
			animationPlayer.Play("idle");
		}
    }

    public override void _PhysicsProcess(double delta)
	
	{
		Vector2 velocity = Velocity;

		var animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var fairy = GetNode<AnimatedSprite2D>("AnimatedSprite2D2");
		var fairy_animation = GetNode<AnimationPlayer>("AnimationPlayer");

		
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
			velocity.Y = JumpVelocity;
		}

		float direction = Input.GetAxis("move_left", "move_right");
	
		if (direction < 0)
		{
			animSprite.FlipH = true;
			fairy.FlipH = true;
			fairy_X = animSprite.Position.X - 30;
			if (facing_left == false)
			{
				fairy_animation.Play("flip");
			}
			facing_left = true;
			

		}
		else if (direction > 0)
		{
			animSprite.FlipH = false;
			fairy.FlipH = false;
			fairy_X = animSprite.Position.X + 30;
			if (facing_left == true)
			{
				fairy_animation.Play("flip_back");
			}
			facing_left = false;
		}
		

		fairy.Position = new Vector2(fairy_X, animSprite.Position.Y);
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
		MoveAndSlide();
	}

	
}
