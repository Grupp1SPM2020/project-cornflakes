﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/JumpingState")]
public class PlayerJumpingState : PlayerAirState {

	public float JumpHeight = 6f;

	public override void Enter() {
		DebugManager.UpdateRow("STM", "PJS");

		Player.PhysicsBody.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
		
		base.Enter();
		skipEnter = true;
	}

	public override void Run() {
		if (Input.GetKeyDown(KeyCode.Space) && !Player.PhysicsBody.IsGrounded() && !StateMachine.IsPreviousState<PlayerJumpingState>()) StateMachine.Push<PlayerJumpingState>();
		
		base.Run();
	}

}