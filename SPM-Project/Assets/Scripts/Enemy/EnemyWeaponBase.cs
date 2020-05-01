﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBase : MonoBehaviour, IDamaging {

	public Enemy owner;
	private float fireRate;
	private float damagePerShot;
	private float weaponSpread;
	private float attackRange;
	[SerializeField] private LayerMask playerLayer;
	private LineRenderer shotLine;
	private WaitForSeconds lineDuration = new WaitForSeconds(0.25f);
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private bool useBulletProjectile;

	public void SetParams(Enemy owner, float rof, float damage, float radius, float range) {
		this.owner = owner;
		fireRate = rof;
		damagePerShot = damage;
		weaponSpread = radius;
		attackRange = range;
	}

	private void Start() {
		shotLine = GetComponent<LineRenderer>();
		shotLine.enabled = false;
	}

	public float GetDamage() {
		return damagePerShot;
	}

	public float GetFireRate() {
		return 60.0f / fireRate;
	}

	public void DoAttack() {
		Vector3 attackVector = owner.GetVectorToTarget(owner.Target.transform, owner.gunTransform);
		Vector3 spreadedAttack = AddSpread(attackVector.normalized)* attackRange;
		if (useBulletProjectile) {
			Bullet bullet = Instantiate(bulletPrefab, owner.gunTransform.position, Quaternion.LookRotation(owner.gunTransform.right));
			bullet.Initialize(owner.gunTransform.position + spreadedAttack, this);
		}
		if (!useBulletProjectile) {
			shotLine.SetPosition(0, owner.gunTransform.position);
			shotLine.SetPosition(1, owner.gunTransform.position + spreadedAttack);
			if (Physics.Raycast(owner.gunTransform.position, spreadedAttack, out RaycastHit hit, attackRange, playerLayer)) {
				if (hit.collider.gameObject.GetComponent<PlayerController>()) {
					shotLine.SetPosition(1, hit.point);
					EventSystem.Current.FireEvent(new HitEvent {
						Description = " " + owner.gameObject.name + " hit " + owner.Target.name,
						Source = gameObject,
						Target = owner.Target.gameObject
					});
				}
			}
		StartCoroutine(ShotEffect());
		}
	}

	private Vector3 AddSpread(Vector3 direction) {
		return new Vector3(
			Random.Range(-weaponSpread, weaponSpread) + direction.x,
			Random.Range(-weaponSpread, weaponSpread) + direction.y,
			direction.z
			).normalized;
	}

	private IEnumerator ShotEffect() {
		shotLine.enabled = true;
		yield return lineDuration;
		shotLine.enabled = false;
	}

}

