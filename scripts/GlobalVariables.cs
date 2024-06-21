using Godot;
using System;

public partial class GlobalVariables : Node2D
{
	
	// Variables you want to persist across scene reloads
	public int score = 0;
	public int health = 100;

	public int lives = 5;
	
	public int temp_score = 0;
	

	// Singleton instance
	private static GlobalVariables instance;

	// Ensure only one instance exists and return it
	public static GlobalVariables GetInstance()
	{
		if (instance == null)
		{
			instance = new GlobalVariables();
			instance.Name = "GlobalVariables"; // Set node name
			instance.Init(); // Initialize singleton
		}
		return instance;
	}

	// Initialize the singleton
	private void Init()
	{
		// Add the singleton to the scene tree if it's not already added
		if (!IsInsideTree())
			GetTree().Root.AddChild(this);
	}

	// Override _EnterTree to ensure singleton initialization in case it's added to the scene tree during runtime
	public override void _EnterTree()
	{
		base._EnterTree();
		if (instance == null)
			instance = this;
	}

	// Method to reset variables to their initial values
	public void ResetVariables()
	{
		temp_score = 0;
		score = 0;
		health = 100;
		lives = 5;
		// Add any other variables you want to reset here
	}
}
