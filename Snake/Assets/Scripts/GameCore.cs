using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
	private Gameboard gameBoard;
	private Snake playerSnake;

	//TODO: Currently we are tracking the snakebody directly in gamecore, this should be moved to its own seperate system to not convolute this class
	private List<WorldObject> snakeBodyList = new List<WorldObject>();

	private AppleSpawner appleSpawner;
	private DependencyResolver dependencyResolver;

	private void Awake()
	{
		// The dependencyResolver gives us easy access to important systems
		dependencyResolver = new DependencyResolver();
	}

	private void Start()
	{
		loadGame();
	}

	private void Update()
	{
		appleSpawner.Update();
	}

	private void loadGame()
	{
		var gameBoardInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Gameboard"));
		gameBoard = gameBoardInstance.GetComponent<Gameboard>();
		gameBoard.InitializeBoard(GameSettings.BoardWidth, GameSettings.BoardHeight);

		var snakeInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Snake"));
		playerSnake = snakeInstance.GetComponent<Snake>();
		playerSnake.SetStartPosition(GameSettings.BoardWidth / 2, GameSettings.BoardHeight / 2);
		playerSnake.snakeMovedEvent += onSnakeMoved;

		appleSpawner = new AppleSpawner();
	}


	private void onSnakeMoved(Vector2 newPosition)
	{
		CheckSnakeCollision(newPosition);
	}

	private void CheckSnakeCollision(Vector2 newPosition)
	{
		foreach (var bodyPart in snakeBodyList)
		{
			if (bodyPart.gridX == newPosition.x && bodyPart.gridY == newPosition.y)
			{
				gameOver();
			}
		}

		bool foundApple = appleSpawner.CheckCollision(newPosition);

		//Create a new bodypart every time snake moves
		createBodyPart((int)newPosition.x, (int)newPosition.y);

		//Removes the oldest body part, except for when we ate an apple
		if (!foundApple && snakeBodyList.Count > 1)
		{
			//Toggle of the bodypart from the board
			gameBoard.ToggleTexture(false, snakeBodyList[0].gridX, snakeBodyList[0].gridY);

			snakeBodyList.RemoveAt(0);
		}
	}

	private void gameOver()
	{
		playerSnake.KillSnake();

		//TODO: restartGame()
	}

	private void createBodyPart(int x, int y)
	{
		snakeBodyList.Add(new WorldObject(x, y));

		//The bodypart is currenly drawn directly on the board to avoid extra spriterenderers.
		//In a real probject the body would likely be made either by shaders or splines
		gameBoard.ToggleTexture(true, x, y);
	}
}
