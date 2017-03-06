using UnityEngine;

namespace Pathogen.Scene.Veins {

	public static class CellFactory {

		private static GameObject redCellReference = null;
		private static GameObject GetRedCellReference(){
			if (redCellReference == null) {
				redCellReference = Resources.Load <GameObject>("Rails/Red Cell");
			}
			return redCellReference;
		}


		public static GameObject CreateRedBloodCell(Vector3 position) {
			return GameObject.Instantiate (GetRedCellReference(), position, Quaternion.identity);
		}

	}

}