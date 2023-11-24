using UnityEngine;

public class Gameboard : MonoBehaviour
{
	public GameObject grassTilePrefab;
	public Sprite grassTexture;
	public Sprite snakebodyTexture;

	private SpriteRenderer[,] boardTiles;

	/// Creates the gameboard
	/// In a real project we would preferably create a RenderTexture to reduce unnecessary SpriteRenders
	public void InitializeBoard(int boardWidth, int boardHeight)
	{
		boardTiles = new SpriteRenderer[boardWidth, boardHeight];

		for (int x = 0; x < boardWidth; x++)
		{
			for (int y = 0; y < boardHeight; y++)
			{
				Vector3 position = GameTools.GridToWorldPosition(x, y);

				//Randomize a 90 degree rotation
				Quaternion randomRotation = Quaternion.Euler(0, 0, 90 * Random.Range(0, 5));

				var gameOject = Instantiate(grassTilePrefab, position, randomRotation, this.transform);

				//Store the spriterenders in grid
				boardTiles[x, y] = gameOject.GetComponent<SpriteRenderer>();
			}
		}
	}

	/// Toggles between empty grass and bodypart
	public void ToggleTexture(bool showSnakeBody, int x, int y)
	{
		boardTiles[x, y].sprite = showSnakeBody ? snakebodyTexture : grassTexture;
	}

}
