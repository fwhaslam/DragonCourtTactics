//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Tools {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Express size preferences as a percentage of screen size.
	/// </summary>
	public class ScreenScaleLayout : ILayoutElement {

		public float widthPct;
		public float heightPct;

		public float minWidth => Screen.width * widthPct / 2f;

		public float preferredWidth => Screen.width * widthPct;

		public float flexibleWidth => 1;

		public float minHeight => Screen.height * heightPct / 2f;

		public float preferredHeight => Screen.height * heightPct;

		public float flexibleHeight => 1;

		public int layoutPriority => 1;

		public void CalculateLayoutInputHorizontal() {
			throw new NotImplementedException();
		}

		public void CalculateLayoutInputVertical() {
			throw new NotImplementedException();
		}
	}

}