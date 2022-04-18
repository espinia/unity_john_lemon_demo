using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
	public Transform player;

	bool isPlayerInRange = false;

	public GameEnding gameEnding;

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform == player)
		{
			isPlayerInRange = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform == player)
		{
			isPlayerInRange = false;
		}
	}

	private void Update()
	{
		if(isPlayerInRange)
		{
			//se suma up porque el destino tiene la posicion en los pies, por eso sube
			Vector3 direction = player.position - transform.position + Vector3.up;
			Ray ray = new Ray(transform.position, direction);
			RaycastHit raycastHit;

			Debug.DrawRay(transform.position, //origen
							direction, //direccion
							Color.green, //color
							Time.deltaTime, //tiempo lo que dure el rayo
							true //difumina el rayo si choca con algo
							);

			//le pasa una referencia para que guarde en esa variable información
			if(Physics.Raycast(ray,out raycastHit))
			{
				//ha chocado con algo
				if (raycastHit.collider.transform == player)
				{
					//se chocó con el player
					gameEnding.CatchPlayer();
				}
			}
		}
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.1f);
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, player.position + Vector3.up);
	}
}
