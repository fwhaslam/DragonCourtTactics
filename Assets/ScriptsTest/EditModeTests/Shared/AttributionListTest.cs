//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Shared {

    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using static NUnit.Framework.Assert;
 
	public class AttributionListTest {

        [Test]
        public void ReadAttributeYml() {

            AttributionList result = AttributionList.ReadAttributeYml();

            IsNotNull( result.Attribution );
            AreEqual( 4, result.Attribution.Count );
            AreEqual( "Adventure", result.Attribution[0].Title );
 
         }
	}

}
