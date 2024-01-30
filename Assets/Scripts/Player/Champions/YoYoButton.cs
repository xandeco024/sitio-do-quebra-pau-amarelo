using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoYoButton : MonoBehaviour
{
	private GameManager gameManager;
	private Emilia emilia;
	[SerializeField] private float magicDamage;
	[SerializeField] private float magicPenetration;
	[SerializeField] private float attackDamage;
	[SerializeField] private float attackPenetration;

	private void Start()
	{
		gameManager = GameObject.FindObjectOfType<GameManager>();
		emilia = gameManager.CurrentPlayer.GetComponent<Emilia>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject == gameManager.CurrentPlayer.gameObject){
			emilia.StartCoroutine("CooldownHabilityYoYo");
			Destroy(this.gameObject);
		}

		if (other.gameObject != gameManager.CurrentPlayer.gameObject && other.gameObject.GetComponent<Champion>() != null)
			other.gameObject.GetComponent<Champion>().
			TakeDamage(attackDamage, attackPenetration, magicDamage, magicPenetration, false,0, other.transform.position);
	}
}
