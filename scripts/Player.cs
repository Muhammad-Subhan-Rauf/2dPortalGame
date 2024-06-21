using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 130.0f;
	public const float JumpVelocity = -200.0f;

	public int health = 100;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	
   

	public bool facing_left = false;



	public bool only_once = false;
	public Vector2 reset_pos = new Vector2(0, 0);


	public int lives = 5;
	public int score = 0;
	
	public override void _Ready()
	{       
	}

	public override void _PhysicsProcess(double delta)
	
	{


		Vector2 currentPosition = GlobalPosition;

		if (!only_once)
		{
			only_once = true;
			reset_pos = currentPosition;
		}




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
		

		
		if (Input.IsActionJustPressed("reset_player")){
			
			currentPosition = reset_pos;
			GlobalPosition = reset_pos;
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
		
		
	}

	public Vector2 update_fairy(double delta, float distance, Vector2 player, Vector2 mousePos)
	{
	
		Vector2 direction = (mousePos - player).Normalized();
		
		Vector2 newPosition = player + (direction * distance)  ;		

		return newPosition;

	}

	public void loose_life()
	{
		GlobalVariables globalVars = GlobalVariables.GetInstance();

		var GM = GetNode<Node>($"../GameManager") as game_manager;
		GM.loose_life();
		

		globalVars.lives -= 1;
		globalVars.score -= 40;
		lives = globalVars.lives;
		score = globalVars.score;


		GlobalPosition = reset_pos;
		GetTree().ReloadCurrentScene();
	}

	public void update_HUD()
	{	
		GlobalVariables globalVars = GlobalVariables.GetInstance();

		var cam = GetNode<Camera2D>("Camera2D");
		var canvas = cam.GetNode<CanvasLayer>("CanvasLayer");
		var life_label = canvas.GetNode<Label>("lives");
		var score_label = canvas.GetNode<Label>("score");

		life_label.Text = globalVars.lives.ToString();
		score_label.Text = globalVars.score.ToString();
	}
	

	
}
