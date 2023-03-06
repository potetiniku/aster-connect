using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public abstract class Yell : NetworkBehaviour
{
	public Size Size { get; private set; }

	private readonly NetworkVariable<FixedString64Bytes> giverName = new();
	public string GiverName => giverName.Value.ToString();

	public virtual void Initialize(Size size, string giverName)
	{
		Size = size;
		this.giverName.Value = giverName;
	}
}
