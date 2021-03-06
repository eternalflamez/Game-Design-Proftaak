﻿using UnityEngine;
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
					newPosition = new Vector3((float)newPosition.x - 0.2925f, (float)newPosition.y + 0.3022f, -0.183f);
					break;
				case 1:
					newPosition = new Vector3((float)newPosition.x + 0.3005f, (float)newPosition.y - 0.2908f, -0.183f);
					break;
				case 2:
					newPosition = new Vector3((float)newPosition.x + 0.3005f, (float)newPosition.y + 0.3022f, -0.183f);
					break;
				case 3:
					newPosition = new Vector3((float)newPosition.x - 0.2925f, (float)newPosition.y - 0.2908f, -0.183f);
					break;
				}

				pawnsOnTile[index].gameObject.transform.position =  newPosition;
			}
		}
		else
		{
			if (pawnsOnTile.Count > 0)
			{
				Vector3 newPosition = new Vector3(this.transform.position.x, this.transform.position.y, -0.183f);
				pawnsOnTile[0].gameObject.transform.position = newPosition;
			}
		}
	}

	public void enableButtons()
	{
		GameManager.instance.showRouteButtons (routeUp, routeDown, routeRight, routeLeft);
	}
	public void disableButtons()
	{
		GameManager.instance.hideRouteButtons ();
	}

	public Vector2 getCoordinates()
	{
		return new Vector2 (x, y);
	}
}
