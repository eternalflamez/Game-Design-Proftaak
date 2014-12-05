using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour
{
	[SerializeField]
	private int rolledNumber = 0;
	[SerializeField]
	private int lastRolled = 0;
	[SerializeField]
	private int tilesMoved = 0;

	[SerializeField]
	private bool isMoving = false;
	private bool isDirection = false;
	public bool isFinished = false;

	[SerializeField]
	private Tile currentTile;
	private Tile destinationTile;

	[SerializeField]
	private int pawnID = 0;

	public enum MoveDirection
	{
		Up,
		Down,
		Right,
		Left
	}
	public MoveDirection moveDir;

	// Use this for initialization
	void Start ()
	{
		moveDir = MoveDirection.Right;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isMoving && rolledNumber > 0)
		{
			if (rolledNumber > tilesMoved)
			{
				if (!currentTile.isSpecial || isDirection)
				{
					if (currentTile.tileType == Tile.TileType.Turn)
					{
						switch (currentTile.tileDirection)
						{
						case Tile.TileDirection.Down:
							moveDir = MoveDirection.Down;
							break;
						case Tile.TileDirection.Up:
							moveDir = MoveDirection.Up;
							break;
						case Tile.TileDirection.Left:
							moveDir = MoveDirection.Left;
							break;
						case Tile.TileDirection.Right:
							moveDir = MoveDirection.Right;
							break;
						}
					}

					destinationTile = BoardController.instance.findTileWithCord(getNextCoordinates(currentTile.getCoordinates()));

					if (destinationTile != null)
					{
						this.transform.position = destinationTile.transform.position;
						currentTile = destinationTile;
						tilesMoved++;

						if (destinationTile.tileType == Tile.TileType.Finish)
						{
							pawnFinish();
						}
						Debug.Log ("CurTile: " + currentTile.getCoordinates());
						Debug.Log ("DesTile: " + destinationTile.getCoordinates());
					}
					/*else
					{
						MoveDirection dir = currentTile.getMoveDirection();

						if (currentTile.tileDirection == Tile.TileDirection.UpDown)
						{
							GameManager.instance.enableButtons(true, true, false, false);
						}
						if (dir == MoveDirection.RightLeft)
						{
							GameManager.instance.enableButtons(false, false, true, true);
						}
					}*/
				}
			}
			else
			{
				lastRolled = rolledNumber;
				tilesMoved = 0;
				rolledNumber = 0;
				isMoving = false;
				isDirection = false;
			}
		}
	}

	void OnGUI()
	{
		if (!isMoving)
		{
			if (GUI.Button(new Rect(10, 10, 150, 100), "Roll Dice"))
			{
				rolledNumber = Random.Range(1,6);
				isMoving = true;
			}
		}

		if (currentTile.isSpecial && !isDirection)
		{
			if (currentTile.tileDirection == Tile.TileDirection.UpDown)
			{
				if (GUI.Button(new Rect(10, 110, 150, 100), "Move Up"))
				{
					moveDir = MoveDirection.Up;
					isDirection = true;
				}
			//}
			//if (currentTile.tileDirection == Tile.TileDirection.UpDown)
			//{
				if (GUI.Button(new Rect(10, 220, 150, 100), "Move Down"))
				{
					moveDir = MoveDirection.Down;
					isDirection = true;
				}
			}
			if (currentTile.tileDirection == Tile.TileDirection.RightLeft)
			{
				if (GUI.Button(new Rect(10, 110, 150, 100), "Move Left"))
				{
					moveDir = MoveDirection.Left;;
					isDirection = true;
				}
			//}
			//if (currentTile.tileDirection == Tile.TileDirection.Right || currentTile.tileDirection == Tile.TileDirection.RightLeft)
			//{
				if (GUI.Button(new Rect(10, 220, 150, 100), "Move Right"))
				{
					moveDir = MoveDirection.Right;
					isDirection = true;
				}
			}
		}

		if (isFinished)
		{
			GUI.Label(new Rect(10, 10, 500, 20), "You have finished");
		}
	}

	private void pawnFinish()
	{
		Debug.Log ("Finished");

		GameManager.instance.playerFinish (pawnID);

		isFinished = true;
		lastRolled = rolledNumber;
		tilesMoved = 0;
		rolledNumber = 0;
		isMoving = false;
		isDirection = false;
	}

	private Vector2 getNextCoordinates(Vector2 currentCoordinates)
	{
		Vector2 nextCoord = Vector2.zero;

		switch(moveDir)
		{
		case MoveDirection.Up:
			nextCoord = new Vector2(currentTile.getCoordinates().x, currentTile.getCoordinates().y + 1);
			break;
		case MoveDirection.Down:
			nextCoord = new Vector2(currentTile.getCoordinates().x, currentTile.getCoordinates().y - 1);
			break;
		case MoveDirection.Left:
			nextCoord = new Vector2(currentTile.getCoordinates().x - 1, currentTile.getCoordinates().y);
			break;
		case MoveDirection.Right:
			nextCoord = new Vector2(currentTile.getCoordinates().x + 1, currentTile.getCoordinates().y);
			break;
		}

		return nextCoord;
	}


	public void setMoveDir(string direction)
	{
		switch(direction)
		{
		case "up":
			moveDir = MoveDirection.Up;
			break;
		case "down":
			moveDir = MoveDirection.Down;
			break;
		case "right":
			moveDir = MoveDirection.Right;
			break;
		case "left":
			moveDir = MoveDirection.Left;
			break;
		}

		GameManager.instance.enableButtons(false, false, false, false);

		isDirection = true;
	}
	public void setMovePawn(int move)
	{
		tilesMoved = 0;
		rolledNumber = move;
		isMoving = true;
	}
}
