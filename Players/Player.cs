using Godot;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using static RPS.Utility;

public partial class Player : Node2D
{
	private RandomNumberGenerator randomNumberGenerator;
	private Size ScreenSize = new Size();
	private double SecondsBeforeUpdate;
	private double UpdateEverySecond = 0.1;
	public PlayerType Type;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		randomNumberGenerator = new RandomNumberGenerator();
		ScreenSize = new Size(
			(int)ProjectSettings.GetSetting("display/window/size/viewport_width"),
			(int)ProjectSettings.GetSetting("display/window/size/viewport_height")
			);
		SecondsBeforeUpdate = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SecondsBeforeUpdate += delta;

		if (SecondsBeforeUpdate > UpdateEverySecond)
		{
			Vector2 newPosition = Position;

			int horizontalMovementValue = randomNumberGenerator.RandiRange(1, 3);
			int horizontalMovementDirection = randomNumberGenerator.RandiRange(0, 2);
			switch (horizontalMovementDirection)
			{
				case 0:
					{
						//UP
						newPosition.X -= horizontalMovementValue;
						break;
					}
				case 1:
					{
						//STAY

						break;
					}
				case 2:
					{
						//DOWN
						newPosition.X += horizontalMovementValue;
						break;
					}
			}

			int verticalMovementValue = randomNumberGenerator.RandiRange(1, 5);
			int verticalMovementDirection = randomNumberGenerator.RandiRange(0, 2);
			switch (verticalMovementDirection)
			{
				case 0:
					{
						//LEFT
						newPosition.Y -= verticalMovementValue;
						break;
					}
				case 1:
					{
						//STAY

						break;
					}
				case 2:
					{
						//RIGHT
						newPosition.Y += verticalMovementValue;
						break;
					}
			}

			if (newPosition.X < 0)
			{
				newPosition.X = 0;
			}
			if (newPosition.X > ScreenSize.Width)
			{
				newPosition.X = ScreenSize.Width;
			}

			if (newPosition.Y < 0)
			{
				newPosition.Y = 0;
			}
			if (newPosition.Y > ScreenSize.Height)
			{
				newPosition.Y = ScreenSize.Height;
			}

			Position = newPosition;
		}
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		Debug.WriteLine(body);
		if (body is Player)
		{
			Player external = (Player)body;

			if (
				(Type == PlayerType.Rock && external.Type == PlayerType.Paper) ||
				(Type == PlayerType.Paper && external.Type == PlayerType.Scissor) ||
				(Type == PlayerType.Scissor && external.Type == PlayerType.Rock)
				)
			{
				QueueFree();
			}
		}
	}
}
