using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Solver : MonoBehaviour
{
	private static readonly int[,] AFFECTED_CUBIES =
	{
		{  0,  1,  2,  3,  0,  1,  2,  3 },   // U
		{  4,  7,  6,  5,  4,  5,  6,  7 },   // D
		{  0,  9,  4,  8,  0,  3,  5,  4 },   // F
		{  2, 10,  6, 11,  2,  1,  7,  6 },   // B
		{  3, 11,  7,  9,  3,  2,  6,  5 },   // L
		{  1,  8,  5, 10,  1,  0,  4,  7 },   // R
	};

	private static readonly int[] APPLICABLE_MOVES = { 0, 262143, 259263, 74943, 74898 };

	private const int FORWARD = 1;
	private const int BACKWARD = 2;

	/* These 40 integers represent the entire Rubik's cube state:
	 * 0 - 11:	edge positions		(UF UR UB UL DF DR DB DL FR FL BR BL)	{0, ..., 11}
	 * 12 - 19:	corner positions	(UFR URB UBL ULF DRF DFL DLB DBR)		{12, ..., 19}
	 * 20 - 31:	edge orientations	(UF UR UB UL DF DR DB DL FR FL BR BL)	{0, 1}
	 * 32 - 39:	corner orientations	(UFR URB UBL ULF DRF DFL DLB DBR)		{0, 1, 2}
	 */
	private static readonly int[] GOAL_STATE = {
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
	};

	private int[] currentState;

	[SerializeField] private Text movesLogText;

	void Start()
	{
		this.Reset(new List<AMove>());
	}

	public void DoMove(AMove move)
	{
		this.currentState = this.ApplyMove(move.ToInt(), this.currentState);
	}

	public void UndoMove(AMove move)
	{
		int inverseMove = this.GetInverseMove(move.ToInt());
		this.currentState = this.ApplyMove(inverseMove, this.currentState);
	}

	public void Reset(List<AMove> initialMoves)
	{
		this.currentState = (int[]) GOAL_STATE.Clone();
		foreach (AMove move in initialMoves)
			this.currentState = this.ApplyMove(move.ToInt(), this.currentState);
	}

	/* Thistlewaite's Algorithm */
	public List<AMove> Solve()
	{
		int phase = 0;
		List<AMove> moves = new List<AMove>();
		this.movesLogText.text = "[Solver]";

		while (++phase < 5)
		{
			this.movesLogText.text += "\nPhase " + phase + ": ";
			int[] currentID = this.GetID(this.currentState, phase);
			int[] goalID = this.GetID(GOAL_STATE, phase);

			if (currentID.SequenceEqual(goalID))
				continue;

			LinkedList<int> moveInts = this.DoBFS(phase, currentID, goalID);

			foreach (int moveInt in moveInts)
			{
				// mutate the current cube representation with this move
				this.currentState = this.ApplyMove(moveInt, this.currentState);

				// add and log this move
				AMove moveObj = MoveFactory.Instance.CreateMoveFromInt(moveInt);
				moves.Add(moveObj);
				this.movesLogText.text += moveObj.ToSymbol() + " ";
			}
		}
		this.LogAllMoves(moves);
		return moves;
	}

	/* Bidirectional Breadth First Search */
	private LinkedList<int> DoBFS(int phase, int[] currentID, int[] goalID)
	{
		// Initialize queue and dictionaries
		Queue<int[]> q = new Queue<int[]>();
		q.Enqueue(this.currentState);
		q.Enqueue(GOAL_STATE);

		Dictionary<int[], int[]> predecessor = new Dictionary<int[], int[]>(new MyIntArrayComparer());
		Dictionary<int[], int> direction = new Dictionary<int[], int>(new MyIntArrayComparer());
		Dictionary<int[], int> lastMove = new Dictionary<int[], int>(new MyIntArrayComparer());
		direction[currentID] = FORWARD;
		direction[goalID] = BACKWARD;

		while (true)
		{
			// Get next state from queue, find its ID and direction
			int[] oldState = q.Dequeue();
			int[] oldID = this.GetID(oldState, phase);
			int oldDir = direction[oldID];

			for (int move = 0; move < 18; move++)
			{
				// only try the allowed moves in the current phase
				if ((APPLICABLE_MOVES[phase] & (1 << move)) > 0)
				{
					// generate a new state from the old state
					int[] newState = this.ApplyMove(move, oldState);
					int[] newID = this.GetID(newState, phase);
					int newDir = 0;
					direction.TryGetValue(newID, out newDir);

					// if we have already found this new state from the other direction, then we can construct a full path
					if (newDir != 0 && newDir != oldDir)
					{
						// swap directions if necessary
						if (oldDir == BACKWARD)
						{
							int[] tempID = newID;
							newID = oldID;
							oldID = tempID;
							move = this.GetInverseMove(move);
						}

						// build a linked list for the moves found in this phase
						LinkedList<int> moveInts = new LinkedList<int>();
						moveInts.AddFirst(move);

						// traverse backward to beginning state
						while (!oldID.SequenceEqual(currentID))
						{
							moveInts.AddFirst(lastMove[oldID]);
							oldID = predecessor[oldID];
						}

						// traverse forward to goal state
						while (!newID.SequenceEqual(goalID))
						{
							moveInts.AddLast(this.GetInverseMove(lastMove[newID]));
							newID = predecessor[newID];
						}

						return moveInts;
					}

					// if we have not seen this new state before, add it to queue and dictionaries
					if (newDir == 0)
					{
						q.Enqueue(newState);
						direction[newID] = oldDir;
						lastMove[newID] = move;
						predecessor[newID] = oldID;
					}
				}
			}
		}
	}
	
	private void LogAllMoves(List<AMove> moves)
	{
		this.movesLogText.text += "\n\nMoves: " + moves.Count + "\n\n";
		foreach (AMove move in moves)
			this.movesLogText.text += move.ToSymbol() + " ";
	}

	private class MyIntArrayComparer : IEqualityComparer<int[]>
	{
		public bool Equals(int[] x, int[] y)
		{
			return x.SequenceEqual(y);
		}

		public int GetHashCode(int[] obj)
		{
			int result = 17;
			for (int i = 0; i < obj.Length; i++)
			{
				unchecked { result = result * 23 + obj[i]; }
			}
			return result;
		}
	}

	/* Move		int	-> inverse int
	 * U		0	-> 2
	 * U2		1	-> 1
	 * U'		2	-> 0
	 * D		3	-> 5
	 * D2		4	-> 4
	 * D'		5	-> 3
	 * F		6	-> 8
	 * F2		7	-> 7
	 * F'		8	-> 6
	 * B		9	-> 11
	 * B2		10	-> 10
	 * B'		11	-> 9
	 * L		12	-> 14
	 * L2		13	-> 13
	 * L'		14	-> 12
	 * R		15	-> 17
	 * R2		16	-> 16
	 * R'		17	-> 15
	 */
	private int GetInverseMove(int move)
	{
		return move + 2 - 2 * (move % 3);
	}

	private int[] ApplyMove(int move, int[] state)
	{
		int turns = move % 3 + 1;
		int face = move / 3;
		state = (int[]) state.Clone();

		while (turns-- > 0)
		{
			int[] oldState = (int[]) state.Clone();
			for (int i = 0; i < 8; i++)
			{
				int isCorner = Convert.ToInt32(i > 3);
				int target = AFFECTED_CUBIES[face, i] + isCorner * 12;
				int killer = AFFECTED_CUBIES[face, (i & 3) == 3 ? i - 3 : i + 1] + isCorner * 12; ;
				int orientationDelta = (i < 4) ? Convert.ToInt32(face > 1 && face < 4) :
					(face < 2) ? 0 : 2 - (i & 1);
				state[target] = oldState[killer];
				state[target + 20] = oldState[killer + 20] + orientationDelta;
				if (turns == 0)
					state[target + 20] %= 2 + isCorner;
			}
		}
		return state;
	}

	/* Get a subset of the current cube state representation relevant for the current phase */
	private int[] GetID(int[] state, int phase)
	{
		int[] result;

		switch (phase)
		{
			// Phase 1: Edge orientations
			case 1:
				result = new int[12];
				Array.Copy(state, 20, result, 0, 12);
				break;
			// Phase 2: Corner orientations, E slice edges
			case 2:
				result = new int[8];
				Array.Copy(state, 31, result, 0, 8);
				for (int e = 0; e < 12; e++)
					result[0] |= (state[e] / 8) << e;
				break;
			// Phase 3: Edge slices M and S, corner tetrads, overall parity
			case 3:
				result = new int[3] { 0, 0, 0 };
				for (int e = 0; e < 12; e++)
					result[0] |= ((state[e] > 7) ? 2 : (state[e] & 1)) << (2 * e);
				for (int c = 0; c < 8; c++)
					result[1] |= ((state[c + 12] - 12) & 5) << (3 * c);
				for (int i = 12; i < 20; i++)
					for (int j = i + 1; j < 20; j++)
						result[2] ^= Convert.ToInt32(state[i] > state[j]);
				break;
			// Phase 4: Everything
			default:
				result = state;
				break;
		}
		return result;
	}
}
