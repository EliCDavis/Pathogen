using UnityEngine;

namespace Pathogen.Scene.Veins {

	public class VeinBehavior : MonoBehaviour {

		[SerializeField]
		/// <summary>
		/// The light to pulse like a heart beat
		/// </summary>
		private Light lightToPulse;

		private int heartBeat = 4;

		void Start(){
			if (lightToPulse == null) {
				Destroy (this);
			}
		}

		void Update () {
			lightToPulse.intensity = 1+ (Mathf.Sin (Time.time*heartBeat)*2) - (Mathf.Pow(Mathf.Sin(Time.time*heartBeat), 4)/.8f);
		}
	}

}