using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubie : MonoBehaviour
{
	[SerializeField] private MeshRenderer upMeshRenderer;
	[SerializeField] private MeshRenderer downMeshRenderer;
	[SerializeField] private MeshRenderer frontMeshRenderer;
	[SerializeField] private MeshRenderer backMeshRenderer;
	[SerializeField] private MeshRenderer rightMeshRenderer;
	[SerializeField] private MeshRenderer leftMeshRenderer;

	[SerializeField] private Material[] upMaterials;
	[SerializeField] private Material[] downMaterials;
	[SerializeField] private Material[] frontMaterials;
	[SerializeField] private Material[] backMaterials;
	[SerializeField] private Material[] rightMaterials;
	[SerializeField] private Material[] leftMaterials;

	[SerializeField] private GameObject outlineGO;
	[SerializeField] private string id;

	public string ID
	{
		get { return this.id; }
	}

	void Start()
	{
		this.Unselect();
	}

	public void Select()
	{
		this.outlineGO.SetActive(true);
	}

	public void Unselect()
	{
		this.outlineGO.SetActive(false);
	}

	public void SetMaterials(int materialIndex)
	{
		if (this.upMaterials.Length != 0)
			this.upMeshRenderer.material = this.upMaterials[materialIndex];

		if (this.downMaterials.Length != 0)
			this.downMeshRenderer.material = this.downMaterials[materialIndex];

		if (this.frontMaterials.Length != 0)
			this.frontMeshRenderer.material = this.frontMaterials[materialIndex];

		if (this.backMaterials.Length != 0)
			this.backMeshRenderer.material = this.backMaterials[materialIndex];

		if (this.rightMaterials.Length != 0)
			this.rightMeshRenderer.material = this.rightMaterials[materialIndex];

		if (this.leftMaterials.Length != 0)
			this.leftMeshRenderer.material = this.leftMaterials[materialIndex];
	}
}
