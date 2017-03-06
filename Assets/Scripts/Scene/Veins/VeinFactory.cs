using UnityEngine;

namespace Pathogen.Scene.Veins {

	public static class VeinFactory {

		private static GameObject veinReference = null;
		private static GameObject GetVeinReference(){
			if(veinReference == null){
				veinReference = Resources.Load<GameObject> ("Rails/Vein");
			}
			return veinReference;
		}

		private static GameObject cholesterolReference = null;
		private static GameObject GetCholesterolReference(){
			if(cholesterolReference == null){
				cholesterolReference = Resources.Load<GameObject> ("Rails/Cholesterol");
			}
			return cholesterolReference;
		}

		/// <summary>
		/// Creates a section of the vein that pulses with a heartbeat
		/// </summary>
		/// <returns>The vein.</returns>
		/// <param name="position">Position of the vein</param>
		public static GameObject CreateVein(Vector3 position) {
			return GameObject.Instantiate (GetVeinReference (), position, GetVeinReference().transform.rotation);
		}

		/// <summary>
		/// Creates the vein with cholesterol colgging it
		/// </summary>
		/// <returns>The vein.</returns>
		/// <param name="position">Position.</param>
		/// <param name="cholesterolAngle">Cholesterol angle.</param>
		public static GameObject CreateVein(Vector3 position, float cholesterolAngle) {
			GameObject vein = CreateVein (position);
			return GameObject.Instantiate (GetCholesterolReference (), position, Quaternion.Euler(0,0,cholesterolAngle), vein.transform);
		}

	}

}