using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner
{
	private float spawnTimer;
	private float spawnTime = 5;

	private List<Apple> appleList = new List<Apple>();

	public void Update()
	{
		//Always have atleast one apple on the board
		if (appleList.Count == 0)
		{
			spawnApple();
			return;
		}

		spawnTimer += Time.deltaTime;
		if (spawnTimer > spawnTime)
		{
			spawnTimer -= spawnTime;

			spawnApple();
		}
	}

	public bool CheckCollision(Vector2 newPosition)
	{
		for (int i = 0; i < appleList.Count; ++i)
		{
			if (appleList[i].gridX == newPosition.x && appleList[i].gridY == newPosition.y)
			{
				destroyApple(i);
				return true;
			}
		}

		return false;
	}

	private void spawnApple()
	{
		//TODO: We should also check the board for blocked tiles. 
		int randX = Random.Range(0, GameSettings.BoardWidth);
		int randY = Random.Range(0, GameSettings.BoardHeight);

		//TODO: Apples should be cached and recycled and not be created at runtime
		var gameObject = DependencyResolver.GetDependecy<GameTools>().CreateGameobject(randX, randY, "Prefabs/Apple");
		appleList.Add(new Apple(randX, randY, gameObject));
	}

	private void destroyApple(int index)
	{
		//TODO: Apples should be cached and recycled and not be destroyed at runtime
		DependencyResolver.GetDependecy<GameTools>().DestroyGameobject(appleList[index].gameObject);
		appleList.RemoveAt(index);
	}
}
