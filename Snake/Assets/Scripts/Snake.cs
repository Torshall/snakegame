using System;
using UnityEngine;

public class Snake : MonoBehaviour
{
	private const float stepTime = 0.5f;
	private Vector2 gridPosition;
	private Vector2 moveDirection = Vector2.up;
	private float moveTimer;
	private bool isAlive = true;

	public Action<Vector2> snakeMovedEvent;

	public void SetStartPosition(int x, int y)
	{
		gridPosition = new Vector2(x, y);
		updatePosition();
	}

	private void Update()
	{
		if (!isAlive)
		{
			return;
		}

		PlayerInput();

		//Force snake to move
		moveTimer += Time.deltaTime;
		if (moveTimer > stepTime)
		{
			moveSnake();
		}
	}

	public void KillSnake()
	{
		isAlive = false;
	}

	private void moveSnake()
	{
		gridPosition += moveDirection;
		moveTimer = 0;

		updatePosition();
	}

	private void checkOutOfBounds()
	{
		if (gridPosition.x >= GameSettings.BoardWidth)
		{
			gridPosition.x = 0;
		}
		else if (gridPosition.x < 0)
		{
			gridPosition.x = GameSettings.BoardWidth - 1;
		}

		if (gridPosition.y >= GameSettings.BoardHeight)
		{
			gridPosition.y = 0;
		}
		else if (gridPosition.y < 0)
		{
			gridPosition.y = GameSettings.BoardHeight - 1;
		}
	}

	private void updatePosition()
	{
		checkOutOfBounds();

		//Notify listeners
		snakeMovedEvent?.Invoke(gridPosition);

		//Update world position
		transform.position = GameTools.GridToWorldPosition((int)gridPosition.x, (int)gridPosition.y);
	}

	private void PlayerInput()
	{
		bool didMove = false;
		if (Input.GetKeyDown(KeyCode.A))
		{
			moveDirection = Vector2.left;
			didMove = true;
		}

		if (Input.GetKeyDown(KeyCode.W))
		{
			moveDirection = Vector2.up;
			didMove = true;
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			moveDirection = Vector2.down;
			didMove = true;
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			moveDirection = Vector2.right;
			didMove = true;
		}

		if (didMove)
		{
			moveSnake();
		}
	}
}
