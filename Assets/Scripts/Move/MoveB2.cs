using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveB2 : AMove
{
	public MoveB2(FaceSelector faceSelector) : base(faceSelector) { }

	public override void DoInstantly()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateInstantly(new Vector3(-180, 0, 0));
	}

	public override void UndoInstantly()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateInstantly(new Vector3(180, 0, 0));
	}

	public override void DoGradually()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateGradually(new Vector3(-2 * RotationManager.DEGREES_INCREMENT, 0, 0));
	}

	public override void UndoGradually()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateGradually(new Vector3(2 * RotationManager.DEGREES_INCREMENT, 0, 0));
	}

	public override string ToSymbol()
	{
		return "B2";
	}

	public override int ToInt()
	{
		return 10;
	}
}
