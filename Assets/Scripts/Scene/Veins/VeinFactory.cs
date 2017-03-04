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

		public static GameObject CreateVein(Vector3 position) {
			return GameObject.Instantiate (GetVeinReference (), position, GetVeinReference().transform.rotation);
		}

	}

}