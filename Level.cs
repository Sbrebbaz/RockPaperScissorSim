using Godot;
using RPS;
using System;
using System.Diagnostics;
using System.Drawing;
using static RPS.Utility;

public partial class Level : Node2D
{
	//public int ItemToSpawn = 20;
	public int ItemToSpawn = 100;

	private PackedScene rock = GD.Load<PackedScene>("res://Players/Rock/Rock.tscn");
	private PackedScene paper = GD.Load<PackedScene>("res://Players/Paper/Paper.tscn");
	private PackedScene scissor = GD.Load<PackedScene>("res://Players/Scissor/Scissors.tscn");

	private Label label;

	private Size ScreenSize = new Size();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		label = GetNode<Label>("CanvasLayer/Label");

		ScreenSize = new Size(
			(int)ProjectSettings.GetSetting("display/window/size/viewport_width"),
			(int)ProjectSettings.GetSetting("display/window/size/viewport_height")
			);

		RandomNumberGenerator generator = new RandomNumberGenerator();

		for (int i = 0; i < ItemToSpawn; i++)
		{
			Player toSpawn;
			PlayerType spawnType = (PlayerType)generator.RandiRange(0, 2);
			switch (spawnType)
			{
				//Rock
				default:
				case PlayerType.Rock:
					{
						Debug.WriteLine("SPAWNED ROCK");
						toSpawn = rock.Instantiate<Player>();
						break;
					}
				//Paper
				case PlayerType.Paper:
					{
						Debug.WriteLine("SPAWNED PAPER");
						toSpawn = paper.Instantiate<Player>();
						break;
					}
				//Scissor
				case PlayerType.Scissor:
					{
						Debug.WriteLine("SPAWNED SCISSOR");
						toSpawn = scissor.Instantiate<Player>();
						break;
					}
			}

			int randomX = generator.RandiRange(0, ScreenSize.Width);
			int randomY = generator.RandiRange(0, ScreenSize.Height);

			toSpawn.Type = spawnType;
			toSpawn.Position = new Vector2(randomX, randomY);
			AddChild(toSpawn);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bool first = true;
		bool allSame = true;
		PlayerType toCheck = PlayerType.Rock;

		for (int i = 0; i < GetChildCount(); i++)
		{
			Node child = GetChild(i);
			if (child is Player)
			{
				Player player = (Player)child;

				if (first)
				{
					toCheck = player.Type;
					first = false;
				}
				else if(toCheck != player.Type)
				{
					allSame = false;
					break;
				}
			}
		}

		if (allSame)
		{
			label.Text = toCheck.ToString() + " Wins";
		}
	}
}
