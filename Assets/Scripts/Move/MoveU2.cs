using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveU2 : AMove
{
	public MoveU2(FaceSelector faceSelector) : base(faceSelector) { }

	public override void DoInstantly()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateInstantly(new Vector3(0, 180, 0));
	}

	public override void UndoInstantly()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateInstantly(new Vector3(0, -180, 0));
	}

	public override void DoGradually()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateGradually(new Vector3(0, 2 * RotationManager.DEGREES_INCREMENT, 0));
	}

	public override void UndoGradually()
	{
		this.faceSelector.SetCubiesParent(RotationManager.Instance.transform);
		RotationManager.Instance.RotateGradually(new Vector3(0, -2 * RotationManager.DEGREES_INCREMENT, 0));
	}

	public override string ToSymbol()
	{
		return "U2";
	}

	public override int ToInt()
	{
		return 1;
	}
}
