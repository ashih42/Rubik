using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMove
{
	protected FaceSelector faceSelector;

	public abstract void DoInstantly();
	public abstract void UndoInstantly();
	public abstract void DoGradually();
	public abstract void UndoGradually();
	public abstract string ToSymbol();
	public abstract int ToInt();

	protected AMove(FaceSelector faceSelector)
	{
		this.faceSelector = faceSelector;
	}	
}
