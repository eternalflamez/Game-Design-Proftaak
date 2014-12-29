using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardController : MonoBehaviour
{
	public static BoardController instance;

	public GameObject[] boardPieces;

	[SerializeField]
	private Tile startTile;

	// Use this for initialization
	void Start ()
	{
		boardPieces = GameObject.FindGameObjectsWithTag ("BoardPiece");

		startTile.setPawnPositions ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		instance = this;
	}

	public Tile getStartTile()
	{
		return startTile;
	}

	/// <summary>
	/// Returns tile with coordinates if no tile found returns null
	/// </summary>
	/// <returns>The tile with cord.</returns>
	/// <param name="coordinates">Coordinates.</param>
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