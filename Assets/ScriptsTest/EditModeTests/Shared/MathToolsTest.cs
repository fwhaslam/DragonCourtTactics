//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Shared {

    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;

	public class MathToolsTest {

        [Test]
        public void PositionToDegrees() {

            Assert.AreEqual( 45f, MathTools.PositionToDegrees( 10f, 10f ), 1f );

            Assert.AreEqual( 0f, MathTools.PositionToDegrees( 10f, 0f ), 1f );

            Assert.AreEqual( 90f, MathTools.PositionToDegrees( 0f, 10f ), 1f );
 
         }

        [Test]
        public void DegreesToPosition() {

            Assert.AreEqual( "(0.7, 0.7)", MathTools.DegreesToPosition( 45f ).ToString() );

            Assert.AreEqual( "(1.0, 0.0)", MathTools.DegreesToPosition( 0f ).ToString() );

            Assert.AreEqual( "(0.0, 1.0)", MathTools.DegreesToPosition( 90f ).ToString() );
 
         }



    }

}
