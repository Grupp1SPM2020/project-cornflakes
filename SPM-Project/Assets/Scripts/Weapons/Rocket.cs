﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour, IPooledObject
{
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;
	[SerializeField] private ParticleSystem ps;

    private Vector3 targetPos;
    private Vector3 targetSurfaceNormal;
    private float lifeTime;

    public void SetTarget(Vector3 position, Vector3 surfaceNormal)
    {
        targetPos = position;
        targetSurfaceNormal = -surfaceNormal;
    }

    private void FixedUpdate()
    {
        if (targetPos != Vector3.zero)
        {
            if (Physics.SphereCast(transform.position, 0.2f, transform.forward, out RaycastHit hit, 0.2f, collisionMask) ||
               lifeTime > maxLifeTime)
            {
				ps.Play();
                Invoke("Explode", 0.5f);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetPos - transform.position, rotationSpeed, 0.0f));
                transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
            }
        }

        lifeTime += Time.fixedDeltaTime;
    }

    private void Explode()
    {
		//Debug.Log("boom");

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, damageMask);

        //RaycastHit[] hits = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.zero, explosionRadius, damageMask);

        //Probably unnecessary
        List<IEntity> hitPawns = new List<IEntity>();

        for (int i = 0; i < hits.Length; i++)
        {
            IEntity pawn = hits[i].gameObject.GetComponent<IEntity>();

            if (pawn != null && !hitPawns.Contains(pawn)) //probably dont need to look at hitpawns
            {
                pawn.TakeDamage(damage);
                hitPawns.Add(pawn); //probably not necessary
            }

            Debug.Log(hits[i].gameObject.name);
        }

        gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {

    }
}