﻿using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour
{
    [SerializeField]
    private int rolledNumber = 0;
    [SerializeField]
    private int tilesMoved = 0;

	//used for bloodsugar meter on crossroads
	private int tileswalked = 0;

    [SerializeField]
    private bool isMoving = false;
    private bool isDirection = false;
    public bool isFinished = false;
	private bool pawnWalking = false;

    [SerializeField]
    private Tile currentTile;
    private Tile destinationTile;

    public Direction moveDir;

    // Use this for initialization
    void Start()
    {
        moveDir = Direction.Right;
    }

    IEnumerator waitBeforeEndTurn()
    {
		//show bloodsugar before wait
		GameManager.instance.setBloodSugarMeter ();

        yield return new WaitForSeconds(InformationManager.instance.getTimerEndTurn());
		
        pawnStopOnTile();

        yield break;
    }
	IEnumerator waitOnSkip()
	{
		//show bloodsugar before wait
		GameManager.instance.setBloodSugarMeter ();

		yield return new WaitForSeconds(InformationManager.instance.getTimerEndTurn());

		GameManager.instance.enableBtnDice ();
		GameManager.instance.playerEndTurn ();
		
		yield break;
	}

    // Update is called once per frame
    void Update()
    {
		if (pawnWalking)
		{
			Vector3 destinationPos = new Vector3(destinationTile.transform.position.x, destinationTile.transform.position.y, -0.183f);
			this.transform.position = Vector3.Lerp (this.transform.position, destinationPos, 0.25f);

			if (0.1 > Vector3.Distance(this.transform.position, destinationPos))
			{
				this.transform.position = destinationPos;
				currentTile = destinationTile;
				tilesMoved++;
				tileswalked++;
				pawnWalking = false;
			}
		}
		else
		{
	        if (!GameManager.instance.ActivePlayer().skipsTurn)
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
								pawnWalking = true;
	                        }
	                    }
	                    else //crossroad//object
	                    {
							GameManager.instance.setInfoText("Nog " + (rolledNumber - tilesMoved) + " vak(ken).");
							GameManager.instance.ActivePlayer().walk(tileswalked * InformationManager.instance.getMinutesPerTile());
							tileswalked = 0;
							GameManager.instance.setBloodSugarMeter();

	                        currentTile.enableButtons();
	                    }
	                }
	                else
	                {
	                    stopPawn();

	                    currentTile.addPawnToTile(this);

	                    if (currentTile.hasInsuline)
	                    {
	                        GameManager.instance.ActivePlayer().addInsulinReserves(1);
							GameManager.instance.setInsulinMeter();
	                    }

	                    StartCoroutine("waitBeforeEndTurn");
	                }
	            }
	        }
	        else
	        {
				GameManager.instance.showPlayerPopUp("Speler " + GameManager.instance.ActivePlayer().getName() + " moet een beurt overslaan.");
				GameManager.instance.disableBtnDice();
	            GameManager.instance.ActivePlayer().skipsTurn = false;

				StartCoroutine("waitOnSkip");
	        }
		}
    }

    private void pawnStopOnTile()
    {
        GameManager.instance.showFoodPnl(currentTile.hasFood);
    }

    public void startCoroutine(string routine)
    {
        StartCoroutine("waitBeforeEndTurn");
    }

    private void stopPawn()
    {
		GameManager.instance.ActivePlayer().walk(tileswalked * InformationManager.instance.getMinutesPerTile());

		tileswalked = 0;
        tilesMoved = 0;
        rolledNumber = 0;
        isMoving = false;
        isDirection = false;
    }

    private void pawnFinish()
    {
        stopPawn();

        isFinished = true;

        GameManager.instance.playerEndTurn();
    }

    private Vector2 getNextCoordinates(Vector2 currentCoordinates)
    {
        Vector2 nextCoord = Vector2.zero;

        switch (moveDir)
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

    public void setCurrentTile(Tile tile)
    {
        currentTile = tile;
    }
    public void setMoveDir(Direction direction)
    {
        moveDir = direction;

        currentTile.disableButtons();

        isDirection = true;
    }

    public void setMovePawn(int move)
    {
        currentTile.removePawnFromTile(this);

        tilesMoved = 0;
        rolledNumber = move;
        isMoving = true;
    }

    public void setColor(Color c)
    {
        gameObject.renderer.material.color = c;
    }

    public Vector3 getPosition()
    {
        return this.transform.position;
    }

    public Color getColor()
    {
        return gameObject.renderer.material.color;
    }
}