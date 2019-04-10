using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFactory : MonoBehaviour
{
	private static MoveFactory instance;

	public static MoveFactory Instance
	{
		get { return MoveFactory.instance; }
	}

	[SerializeField] private FaceSelector rightFaceSelector;
	[SerializeField] private FaceSelector leftFaceSelector;
	[SerializeField] private FaceSelector frontFaceSelector;
	[SerializeField] private FaceSelector backFaceSelector;
	[SerializeField] private FaceSelector upFaceSelector;
	[SerializeField] private FaceSelector downFaceSelector;

	private void Awake()
	{
		if (MoveFactory.instance == null)
		{
			MoveFactory.instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(gameObject);
	}

	public AMove CreateMoveFromInt(int value)
	{
		switch (value)
		{
			case 0:
				return this.CreateMoveU();
			case 1:
				return this.CreateMoveU2();
			case 2:
				return this.CreateMoveU_();
			case 3:
				return this.CreateMoveD();
			case 4:
				return this.CreateMoveD2();
			case 5:
				return this.CreateMoveD_();
			case 6:
				return this.CreateMoveF();
			case 7:
				return this.CreateMoveF2();
			case 8:
				return this.CreateMoveF_();
			case 9:
				return this.CreateMoveB();
			case 10:
				return this.CreateMoveB2();
			case 11:
				return this.CreateMoveB_();
			case 12:
				return this.CreateMoveL();
			case 13:
				return this.CreateMoveL2();
			case 14:
				return this.CreateMoveL_();
			case 15:
				return this.CreateMoveR();
			case 16:
				return this.CreateMoveR2();
			case 17:
				return this.CreateMoveR_();
			default:
				throw new MoveException("Invalid int: " + value);
		}
	}

	public AMove CreateMoveFromSymbol(string symbol)
	{
		switch (symbol)
		{
			case "R":
				return this.CreateMoveR();
			case "R2":
				return this.CreateMoveR2();
			case "R'":
				return this.CreateMoveR_();
			case "L":
				return this.CreateMoveL();
			case "L2":
				return this.CreateMoveL2();
			case "L'":
				return this.CreateMoveL_();
			case "F":
				return this.CreateMoveF();
			case "F2":
				return this.CreateMoveF2();
			case "F'":
				return this.CreateMoveF_();
			case "B":
				return this.CreateMoveB();
			case "B2":
				return this.CreateMoveB2();
			case "B'":
				return this.CreateMoveB_();
			case "U":
				return this.CreateMoveU();
			case "U2":
				return this.CreateMoveU2();
			case "U'":
				return this.CreateMoveU_();
			case "D":
				return this.CreateMoveD();
			case "D2":
				return this.CreateMoveD2();
			case "D'":
				return this.CreateMoveD_();
			default:
				throw new MoveException("Invalid move: " + symbol);
		}
	}

	public AMove CreateMoveR()
	{
		return new MoveR(this.rightFaceSelector);
	}

	public AMove CreateMoveR2()
	{
		return new MoveR2(this.rightFaceSelector);
	}

	public AMove CreateMoveR_()
	{
		return new MoveR_(this.rightFaceSelector);
	}

	public AMove CreateMoveL()
	{
		return new MoveL(this.leftFaceSelector);
	}

	public AMove CreateMoveL2()
	{
		return new MoveL2(this.leftFaceSelector);
	}

	public AMove CreateMoveL_()
	{
		return new MoveL_(this.leftFaceSelector);
	}

	public AMove CreateMoveF()
	{
		return new MoveF(this.frontFaceSelector);
	}

	public AMove CreateMoveF2()
	{
		return new MoveF2(this.frontFaceSelector);
	}

	public AMove CreateMoveF_()
	{
		return new MoveF_(this.frontFaceSelector);
	}

	public AMove CreateMoveB()
	{
		return new MoveB(this.backFaceSelector);
	}

	public AMove CreateMoveB2()
	{
		return new MoveB2(this.backFaceSelector);
	}

	public AMove CreateMoveB_()
	{
		return new MoveB_(this.backFaceSelector);
	}

	public AMove CreateMoveU()
	{
		return new MoveU(this.upFaceSelector);
	}

	public AMove CreateMoveU2()
	{
		return new MoveU2(this.upFaceSelector);
	}

	public AMove CreateMoveU_()
	{
		return new MoveU_(this.upFaceSelector);
	}

	public AMove CreateMoveD()
	{
		return new MoveD(this.downFaceSelector);
	}

	public AMove CreateMoveD2()
	{
		return new MoveD2(this.downFaceSelector);
	}

	public AMove CreateMoveD_()
	{
		return new MoveD_(this.downFaceSelector);
	}
}
