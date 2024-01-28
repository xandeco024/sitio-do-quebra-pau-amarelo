using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoYoButton : MonoBehaviour
{
	private GameManager gameManager;
	[SerializeField] private float magicDamage;
	[SerializeField] private float magicPenetration;
	[SerializeField] private float attackDamage;
	[SerializeField] private float attackPenetration;

	private void Update()
	{
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject != gameManager.CurrentPlayer.gameObject && other.gameObject.GetComponent<Champion>() != null)
			other.gameObject.GetComponent<Champion>().
			TakeDamage(attackDamage,attackPenetration,magicDamage,magicPenetration,false,other.transform.position);

		if (other.gameObject == gameManager.CurrentPlayer.gameObject)
			Destroy(this.gameObject);

		
	}
}
