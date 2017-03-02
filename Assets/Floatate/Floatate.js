/**************************************
 * Copyright (c) 2012, Timothy Thomas *
 *        Some rights reserved.       *
 **************************************/

#pragma strict

var bobSpeed : float = 3.0;  //Bob speed
var bobHeight : float = 0.5; //Bob height
var bobOffset : float = 0.0;

var PrimaryRot : float = 80.0;  //First axies degrees per second
var SecondaryRot : float = 40.0; //Second axies degrees per second
var TertiaryRot : float = 20.0;  //Third axies degrees per second

private var bottom : float;

@script AddComponentMenu("Perflexive Media/Floatate")

function Awake () {
	if (bobSpeed < 0) {
		Debug.LogWarning("Negative object bobSpeed value! May result in undesired behavior. Continuing anyway.", gameObject);
	}
	
	if (bobHeight < 0) {
		Debug.LogWarning("Negative object bobHeight value! May result in undesired behavior. Continuing anyway.", gameObject);
	}
	
	bottom = transform.position.y;
}

function Update () {
	transform.Rotate(Vector3(0, PrimaryRot, 0) * Time.deltaTime, Space.World);
	transform.Rotate(Vector3(SecondaryRot, 0, 0) * Time.deltaTime, Space.Self);
	transform.Rotate(Vector3(0, 0, TertiaryRot) * Time.deltaTime, Space.Self);
	
	transform.position.y = bottom + (((Mathf.Cos((Time.time + bobOffset) * bobSpeed) + 1) / 2 ) * bobHeight);
}