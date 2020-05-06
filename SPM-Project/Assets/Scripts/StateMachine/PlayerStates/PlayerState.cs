﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Viktor Dahlberg
public abstract class PlayerState : State {
	
	public float Drag;
	public float Acceleration;
	public float TopSpeed;

	private PlayerController player;
	
	public PlayerController Player => player = player != null ? player : (PlayerController) Owner;

	protected static int jumpCount = 0;
	protected static int dashCount = 0;

	public override void Enter() {
		Player.PhysicsBody.SetSlideRate(Drag);
		if (Player.PhysicsBody.IsGrounded()) dashCount = 0;
	}

	public override void Run() {
		if (Player.Input.doDash && StateMachine.CanEnterState<PlayerDashingState>()) {
			StateMachine.TransitionTo<PlayerDashingState>();
		}
		else if (Player.Input.doJump && StateMachine.CanEnterState<PlayerJumpingState>()) {
			StateMachine.TransitionTo<PlayerJumpingState>();
		}
		
	}
}