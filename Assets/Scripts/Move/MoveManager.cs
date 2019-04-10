using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
	[SerializeField] Solver solver;
	[SerializeField] private Text movesLogText;

	private bool isInstantRotation;
	private Stack<AMove> moves;

	void Start()
	{
		this.isInstantRotation = false;
		this.moves = new Stack<AMove>();
		this.DisplayAllMoves();
	}

	public void Reset()
	{
		this.moves.Clear();
		this.DisplayAllMoves();
	}

	public void ToggleInstantRotation()
	{
		this.isInstantRotation = !this.isInstantRotation;
	}

	public void DoMove(AMove move)
	{
		if (this.isInstantRotation)
			move.DoInstantly();
		else
			move.DoGradually();
	}

	public void UndoMove(AMove move)
	{
		if (this.isInstantRotation)
			move.UndoInstantly();
		else
			move.UndoGradually();
	}

	public void AddMove(AMove move)
	{
		this.solver.DoMove(move);
		this.DoMove(move);
		this.moves.Push(move);
		this.DisplayAllMoves();
	}

	public void UndoLastMove()
	{
		if (this.moves.Count != 0)
		{
			AMove move = this.moves.Pop();
			this.solver.UndoMove(move);
			this.UndoMove(move);
			this.DisplayAllMoves();
		}
	}

	private void DisplayAllMoves()
	{
		this.movesLogText.text = "Moves: " + this.moves.Count + "\n\n";
		Stack<AMove> movesReverseStack = new Stack<AMove>();
		foreach (AMove move in moves)
			movesReverseStack.Push(move);
		foreach (AMove move in movesReverseStack)
			this.movesLogText.text += move.ToSymbol() + " ";
	}
}
