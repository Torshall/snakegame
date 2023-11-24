using UnityEngine;

public struct Apple
{
	public GameObject gameObject;
	public int gridX;
	public int gridY;

	public Apple(int x, int y, GameObject goj)
	{
		gridX = x;
		gridY = y;
		gameObject = goj;
	}
}
