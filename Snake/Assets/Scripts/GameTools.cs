using UnityEngine;

public class GameTools : MonoBehaviour, IGameTools
{
	void Start()
	{
		// We add GameTools to the DependencyResolver so we can create and destroy gameobjects from none MonoBehaviour classes
		DependencyResolver.AddDependency(this);
	}

	/// <summary>
	/// Converts our gridposition to a normalized world position to keep the game board centered
	/// </summary>
	public static Vector3 GridToWorldPosition(int x, int y)
	{
		int centeredX = x - (GameSettings.BoardWidth / 2);
		int centeredY = y - (GameSettings.BoardHeight / 2);

		return new Vector3(centeredX * GameSettings.TileSize, centeredY * GameSettings.TileSize, 0);
	}

	public GameObject CreateGameobject(int gridX, int gridY, string path)
	{
		return Instantiate(Resources.Load<GameObject>(path), GridToWorldPosition(gridX, gridY), Quaternion.identity);
	}

	public void DestroyGameobject(GameObject gameObject)
	{
		Destroy(gameObject);
	}
}
