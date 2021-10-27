//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Shared {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using UnityEngine;

	using YamlDotNet.Serialization;

	    /// <summary>
    /// Class to read from Attribute.yml for us in constructing credits.
    /// </summary>
	public class AttributionList {

		static readonly string filePath = "Attributable/Attribution";

		public List<Attribution> Attribution {  get; set; }


		static public AttributionList ReadAttributeYml() {

			System.Object loaded = Resources.Load<TextAsset>( filePath );
			string content = loaded.ToString();
			var list = new Deserializer().Deserialize<AttributionList>(content);
			return list;
		}

	}
}
