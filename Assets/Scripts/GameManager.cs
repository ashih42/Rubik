using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public static GameManager Instance
	{
		get { return GameManager.instance; }
	}

	[SerializeField] private GameObject rubiksCubePrefab;
	[SerializeField] private GameObject rubiksCubeGO;
	[SerializeField] private FaceSelector[] selectors;
	[SerializeField] private Solver solver;
	[SerializeField] private MoveManager moveManager;
	[SerializeField] private InputField initialSequenceInputField;

	[SerializeField] private List<Cubie> allCubies;
	private int materialIndex;

	public bool IsBusy { get; set; }
	public GameObject RubiksCubeGO { get { return this.rubiksCubeGO; } }

	private void Awake()
	{
		if (GameManager.instance == null)
		{
			GameManager.instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(gameObject);
	}

	private void Start()
	{
		this.materialIndex = 0;
		this.IsBusy = false;
		this.FindAllCubies();
	}

	void Update()
	{
		if (!this.IsBusy)
			this.HandleKeyInput();
	}

	private void HandleKeyInput()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			if (Input.GetKeyDown(KeyCode.R))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveR_());
			else if (Input.GetKeyDown(KeyCode.L))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveL_());
			else if (Input.GetKeyDown(KeyCode.F))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveF_());
			else if (Input.GetKeyDown(KeyCode.B))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveB_());
			else if (Input.GetKeyDown(KeyCode.U))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveU_());
			else if (Input.GetKeyDown(KeyCode.D))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveD_());
		}
		else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
		{
			if (Input.GetKeyDown(KeyCode.R))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveR2());
			else if (Input.GetKeyDown(KeyCode.L))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveL2());
			else if (Input.GetKeyDown(KeyCode.F))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveF2());
			else if (Input.GetKeyDown(KeyCode.B))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveB2());
			else if (Input.GetKeyDown(KeyCode.U))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveU2());
			else if (Input.GetKeyDown(KeyCode.D))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveD2());
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.R))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveR());
			else if (Input.GetKeyDown(KeyCode.L))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveL());
			else if (Input.GetKeyDown(KeyCode.F))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveF());
			else if (Input.GetKeyDown(KeyCode.B))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveB());
			else if (Input.GetKeyDown(KeyCode.U))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveU());
			else if (Input.GetKeyDown(KeyCode.D))
				this.moveManager.AddMove(MoveFactory.Instance.CreateMoveD());
			else if (Input.GetKeyDown(KeyCode.Backspace))
				this.moveManager.UndoLastMove();
		}
	}

	public void Solve()
	{
		if (!this.IsBusy)
		{
			this.moveManager.Reset();
			List<AMove> moves = this.solver.Solve();
			StartCoroutine(this.RunMovesCoroutine(moves));
		}
	}

	public void Reset()
	{
		if (!this.IsBusy)
		{
			// delete old RubiksCube
			Destroy(this.rubiksCubeGO);

			// clear old triggered Cubies in all Selectors
			foreach (ASelector selector in this.selectors)
				selector.Reset();

			// clear cached moves in MoveManager
			this.moveManager.Reset();

			// create new RubiksCube
			this.rubiksCubeGO = Instantiate(this.rubiksCubePrefab);
			this.FindAllCubies();
			this.UpdateCubieMaterials();

			// parse initial sequence in input field
			this.ParseInitialSequence();
		}
	}

	private void FindAllCubies()
	{
		this.allCubies.Clear();
		foreach (Transform child in this.rubiksCubeGO.transform)
		{
			Cubie cubie = child.GetComponent<Cubie>();
			this.allCubies.Add(cubie);
		}
	}

	public void ToggleMaterials()
	{
		this.materialIndex = (this.materialIndex + 1) % 2;
		this.UpdateCubieMaterials();
	}

	private void UpdateCubieMaterials()
	{
		foreach (Cubie cubie in this.allCubies)
			cubie.SetMaterials(this.materialIndex);
	}

	private void ParseInitialSequence()
	{
		List<AMove> initialMoves = new List<AMove>();

		string[] symbols = this.initialSequenceInputField.text.Split(new char[] { ' ' });
		symbols = symbols.Where(x => !string.IsNullOrEmpty(x)).ToArray();

		try
		{
			foreach (string symbol in symbols)
				initialMoves.Add(MoveFactory.Instance.CreateMoveFromSymbol(symbol));

			this.solver.Reset(initialMoves);
			StopAllCoroutines();
			StartCoroutine(this.RunMovesCoroutine(initialMoves));
		}
		catch (MoveException e)
		{
			this.initialSequenceInputField.text = e.Message;
		}
	}

	private IEnumerator RunMovesCoroutine(List<AMove> moves)
	{
		foreach (AMove move in moves)
		{
			// delay a few frames for Selectors to detect collisions on newly instantiated Cubies
			for (int i = 0; i < 3; i++)
				yield return new WaitForEndOfFrame();
			while (this.IsBusy)
				yield return new WaitForEndOfFrame();
			this.moveManager.DoMove(move);
		}
	}
}
