using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	[SerializeField]
	private int x;
	[SerializeField]
	private int y;

	public enum TileDirection
	{
		UpDown,
		RightLeft,
		Left,
		Right,
		Up,
		Down
	}
	
	public TileDirection tileDirection = TileDirection.Right;
	//[SerializeField]
	//private Pawn.MoveDirection tileDirection = Pawn.MoveDirection.Right;

	public enum TileType
	{
		Straight,
		Turn,
		CrossRoad,
		Finish
	}

	public TileType tileType = TileType.Straight;

	public bool isSpecial = false;

	public Vector2 getCoordinates()
	{
		return new Vector2 (x, y);
	}

	//public Pawn.MoveDirection getMoveDirection()
	//{
	//	return tileDirection;
	//}
}
