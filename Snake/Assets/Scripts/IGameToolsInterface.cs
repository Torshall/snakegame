using UnityEngine;

public interface IGameTools : IDependecyInterface
{
	public void DestroyGameobject(GameObject gameObject);
	public GameObject CreateGameobject(int gridX, int gridY, string path);

}
