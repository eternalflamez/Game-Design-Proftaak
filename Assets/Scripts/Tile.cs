using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	[SerializeField]
	private int x;
	[SerializeField]
	private int y;

	[SerializeField]
	private GameObject btn1;
	[SerializeField]
	private GameObject btn2;

	public enum TileDirection
	{
		UpDown,
		RightLeft,
		UpLeft,
		UpRight,
		DownLeft,
		DownRight,
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
	public bool hasObject = false;

	void Start()
	{
		if (isSpecial)
		{
			btn1.SetActive(false);
			btn2.SetActive(false);
		}
	}

	public void enableButtons()
	{
		btn1.SetActive(true);
		btn2.SetActive(true);
	}
	public void disableButtons()
	{
		btn1.SetActive(false);
		btn2.SetActive(false);
	}

	public Vector2 getCoordinates()
	{
		return new Vector2 (x, y);
	}

	//public Pawn.MoveDirection getMoveDirection()
	//{
	//	return tileDirection;
	//}
}
