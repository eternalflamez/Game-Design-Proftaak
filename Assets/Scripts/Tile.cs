using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	[SerializeField]
	private bool routeUp = false;
	[SerializeField]
	private bool routeDown = false;
	[SerializeField]
	private bool routeRight = false;
	[SerializeField]
	private bool routeLeft = false;

	public List<Pawn> pawnsOnTile = new List<Pawn>();
	
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
	public Direction tileDirection = Direction.Right;

	public enum TileType
	{
		Straight,
		Turn,
		CrossRoad,
		Finish
	}

	public TileType tileType = TileType.Straight;

	public bool isSpecial = false;
	public bool hasFood = false;
	public bool hasInsuline = false;

	void Start()
	{
		if (isSpecial)
		{
			//btn1.SetActive(false);
			//btn2.SetActive(false);
		}
	}

	public void addPawnToTile(Pawn pawn)
	{
		pawnsOnTile.Add (pawn);

		setPawnPositions ();
	}
	public void removePawnFromTile(Pawn pawn)
	{
		pawnsOnTile.Remove (pawn);

		setPawnPositions ();
	}

	public void setPawnPositions()
	{
		if (pawnsOnTile.Count > 1)
		{
			for (int index = 0; index < pawnsOnTile.Count; index++)
			{
				Vector3 newPosition = this.transform.position;

				switch (index)
				{
				case 0:
					newPosition = new Vector3((float)newPosition.x - 0.2925f, (float)newPosition.y + 0.3022f, (float)newPosition.z);
					break;
				case 1:
					newPosition = new Vector3((float)newPosition.x + 0.3005f, (float)newPosition.y - 0.2908f, (float)newPosition.z);
					break;
				case 2:
					newPosition = new Vector3((float)newPosition.x + 0.3005f, (float)newPosition.y + 0.3022f, (float)newPosition.z);
					break;
				case 3:
					newPosition = new Vector3((float)newPosition.x - 0.2925f, (float)newPosition.y - 0.2908f, (float)newPosition.z);
					break;
				}

				pawnsOnTile[index].gameObject.transform.position =  newPosition;
			}
		}
		else
		{
			if (pawnsOnTile.Count > 0)
			{
				pawnsOnTile[0].gameObject.transform.position = this.transform.position;
			}
		}
	}

	public void enableButtons()
	{
		GameManager.instance.showRouteButtons (routeUp, routeDown, routeRight, routeLeft);
		//btn1.SetActive(true);
		//btn2.SetActive(true);
	}
	public void disableButtons()
	{
		GameManager.instance.hideRouteButtons ();
		//btn1.SetActive(false);
		//btn2.SetActive(false);
	}

	public Vector2 getCoordinates()
	{
		return new Vector2 (x, y);
	}
}
