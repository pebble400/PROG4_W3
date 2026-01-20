using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ScanAT : ActionTask {

        public BBParameter<float> scanRadiusBBP;
		public BBParameter<Transform> lightTowerTargetBBP;
		

		public LayerMask scanLayer;
		public Color scanColour;
		public float scanDuration;
		public int numberOfScanCirclePoints;

		private float scanTimer;
		

		
		protected override void OnExecute() 
		{
			scanTimer = 0;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() 
		{
			DrawCircle(agent.transform.position, scanRadiusBBP.value, scanColour, numberOfScanCirclePoints, 1f);
			scanTimer += Time.deltaTime;
			if (scanTimer > scanDuration)
			{
				Collider[] colliders = Physics.OverlapSphere(agent.transform.position, scanRadiusBBP.value, scanLayer);
				foreach (Collider collider in colliders) 
				{
					Blackboard blackboard = collider.GetComponentInParent<Blackboard>();
					float repairValue = blackboard.GetVariableValue<float>("repairValue");

					if(repairValue == 0)
					{
						lightTowerTargetBBP.value = blackboard.GetVariableValue<Transform>("workpad");
					}
				}
				EndAction(true);
			}
		}

		private void DrawCircle(Vector3 center, float radius, Color colour, int numberOfPoints, float duration)
		{
			Vector3 startPoint, endPoint;
			int anglePerPoint = 360 / numberOfPoints;
			for (int i = 1; i <= numberOfPoints; i++)
			{
				startPoint = new Vector3(Mathf.Cos(Mathf.Deg2Rad * anglePerPoint * (i-1)), 0, Mathf.Sin(Mathf.Deg2Rad * anglePerPoint * (i-1)));
				startPoint = center + startPoint * radius;
				endPoint = new Vector3(Mathf.Cos(Mathf.Deg2Rad * anglePerPoint * i), 0, Mathf.Sin(Mathf.Deg2Rad * anglePerPoint * i));
				endPoint = center + endPoint * radius;
				Debug.DrawLine(startPoint, endPoint, colour, duration);
			}

			
		}
	}
}