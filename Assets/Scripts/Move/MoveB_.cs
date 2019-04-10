using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveB_ : AMove
{
	public MoveB_(FaceSelector faceSelector) : base(faceSelector) { }

	public override void DoInstantly()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateInstantly(new Vector3(90, 0, 0));
	}

	public override void UndoInstantly()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateInstantly(new Vector3(-90, 0, 0));
	}

	public override void DoGradually()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateGradually(new Vector3(RotationManager.DEGREES_INCREMENT, 0, 0));
	}

	public override void UndoGradually()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateGradually(new Vector3(-RotationManager.DEGREES_INCREMENT, 0, 0));
	}

	public override string ToSymbol()
	{
		return "B'";
	}

	public override int ToInt()
	{
		return 11;
	}
}
