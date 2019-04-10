using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveException : Exception
{
	public MoveException(string message) : base(message) { }
}
