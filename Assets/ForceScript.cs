using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceScript : MonoBehaviour {

	public static double balloonCharge, wallCharge;
	public static bool running;

	public InputField ballField;
	public InputField wallField;

	public static float modE = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		GameObject balloon = GameObject.FindGameObjectWithTag ("Object");
		Rigidbody rigid = balloon.GetComponent<Rigidbody> ();
		Debug.Log (rigid.velocity);

		if (running) 
		{
			AssignCharges ();
			isForceBigEnough ();
		}

	}

	void AssignCharges()
	{
		if (ballField.text.Length > 0)
			balloonCharge = double.Parse(ballField.text);
		if (wallField.text.Length > 0)
			wallCharge = double.Parse(wallField.text);

		Debug.Log ("Balloon Q: " + balloonCharge);
		Debug.Log ("Wall Q: " + wallCharge);
	}

	public static void isForceBigEnough ()
	{
		//5cm away
		double distance = .05f;
		//Density of room-temp air
		float airDensity = 1.21f;
		//10cm Radius
		float radius = .1f;
		float gravityAcceleration = 9.8f;
		float spaceConstant = Mathf.Pow(9f, 9);
		float dragConstant = 0.5f;
		double balloonArea = Mathf.PI * radius * radius;
		double balloonMass = 0.00830f;
		double airMass = 0.00675f;

		GameObject balloon = GameObject.FindGameObjectWithTag ("Object");
		Rigidbody rigid = balloon.GetComponent<Rigidbody> ();
		float speed = rigid.velocity.magnitude;
		double finalForce, eForce, dForce, bForce, gForce;

		eForce = CalculateElectricForce (spaceConstant, distance);
		dForce = CalculateDragForce (dragConstant, airDensity, balloonArea, speed);
		bForce = CalculateForce (airMass, gravityAcceleration);
		gForce = CalculateForce (balloonMass, gravityAcceleration);
		finalForce = (gForce - bForce - dForce - eForce);

		Debug.Log ("E-Force: " + eForce);
		Debug.Log ("D-Force: " + dForce);
		Debug.Log ("B-Force: " + bForce);
		Debug.Log ("G-Force: " + gForce);
		Debug.Log ("Final Force: " + finalForce);

		//Signals that our force of gravity is less than that of the opposing forces
		if (finalForce <= 0) {
			finalForce = 0;
			rigid.useGravity = false;
		}

		if (eForce < 0.01f && eForce > 0.0025f) {
			modE = 0.01f;
		}
		else
			modE = (float) eForce;
		
		float modF = (float)finalForce;
		modF = -0.1f;

		Debug.Log (modE);
		Debug.Log (modF);

		modE = Mathf.Clamp (modE, 0.0f, 15.0f);

		rigid.velocity = new Vector3 (modE, modF, 0f);
		rigid.useGravity = false;

		Debug.Log ("UPDATED: " + rigid.velocity);
	}

	public static double CalculateElectricForce (double k, double distance)
	{
		double force = k*balloonCharge*wallCharge / distance;
		return force;
	}

	public static double CalculateDragForce (double dragConstant, double density, double area, double speed)
	{
		double force = dragConstant * density * area * speed / 2;
		return force;
	}

	public static double CalculateForce (double mass, double acceleration)
	{
		double force = mass * acceleration;
		return force;
	}
}
