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

	//public enum MoveDirection
	//{
	//	Up,
	//	Down,
	//	Right,
	//	Left
	//}
	public Direction moveDir;

	// Use this for initialization
	void Start ()
	{
		moveDir = Direction.Right;
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
						moveDir = currentTile.tileDirection;
					}

					isDirection = false;
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
					}
				}
				else //crossroad//object
				{
					currentTile.enableButtons();
				}
			}
			else
			{
				stopPawn();

				currentTile.addPawnToTile(this);

				if (currentTile.hasFood)
				{
					GameManager.instance.showObjectButtons("food");
				}
				else if (currentTile.hasInsuline)
				{
					GameManager.instance.showObjectButtons("insuline");
				}
				else
				{
					GameManager.instance.playerEndTurn();
				}
			}
		}
	}

	private void stopPawn()
	{
		lastRolled = rolledNumber;
		tilesMoved = 0;
		rolledNumber = 0;
		isMoving = false;
		isDirection = false;
	}

	private void pawnFinish()
	{
		stopPawn ();

		isFinished = true;

		GameManager.instance.playerEndTurn ();
		GameManager.instance.playerFinish (pawnID);
	}

	private Vector2 getNextCoordinates(Vector2 currentCoordinates)
	{
		Vector2 nextCoord = Vector2.zero;

		switch(moveDir)
		{
		case Direction.Up:
			nextCoord = new Vector2(currentTile.getCoordinates().x, currentTile.getCoordinates().y + 1);
			break;
		case Direction.Down:
			nextCoord = new Vector2(currentTile.getCoordinates().x, currentTile.getCoordinates().y - 1);
			break;
		case Direction.Left:
			nextCoord = new Vector2(currentTile.getCoordinates().x - 1, currentTile.getCoordinates().y);
			break;
		case Direction.Right:
			nextCoord = new Vector2(currentTile.getCoordinates().x + 1, currentTile.getCoordinates().y);
			break;
		}

		return nextCoord;
	}


	public void setMoveDir(Direction direction)
	{
		moveDir = direction;

		currentTile.disableButtons ();

		isDirection = true;
	}

	public void setMovePawn(int move)
	{
		currentTile.removePawnFromTile (this);

		tilesMoved = 0;
		rolledNumber = move;
		isMoving = true;
	}

    public void setColor(Color c)
    {
        gameObject.renderer.material.color = c;
    }
}
