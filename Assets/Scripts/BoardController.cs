using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardController : MonoBehaviour
{
	public static BoardController instance;

	public GameObject[] boardPieces;

	// Use this for initialization
	void Start ()
	{
		boardPieces = GameObject.FindGameObjectsWithTag ("BoardPiece");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		instance = this;
	}

	public Tile findTileWithCord(Vector2 coordinates)
	{
		for (int index = 0; index < boardPieces.Length; index++)
		{
			Tile pieceController = (Tile)boardPieces[index].GetComponent("Tile");

			if (pieceController.getCoordinates() == coordinates)
			{
				return pieceController;
			}
		}

		return null;
	}
}