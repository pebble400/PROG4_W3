using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class BoostAT : ActionTask {

		public BBParameter<float> scanRadiusBBP;
		public float boostValue = 5f;
		
		protected override void OnExecute() 
		{
			scanRadiusBBP.value += boostValue;
			EndAction(true);
		}

	}
}