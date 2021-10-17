using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.TestTools;
using NUnit.Framework;

using static NUnit.Framework.Assert;

namespace Tools {

	public class ReadFieldsTool {

		/// <summary>
		/// Does the named field exist?
		/// </summary>
		/// <typeparam name="Q"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		static public bool HasField<Q>( Q source, string name ) {

			return ( null != ReadFieldInfo(source,name) );
		}

		/// <summary>
		/// Get field info.
		/// </summary>
		/// <typeparam name="Q"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		static public FieldInfo ReadFieldInfo<Q>( Q source, string name ) {
			return typeof(Q).GetField(name);
		}

		/// <summary>
		/// Retrieve the value of the named, public field.
		/// </summary>
		/// <typeparam name="Q"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		static public T ReadField<Q,T>( Q source, string name ) {

			FieldInfo fld = typeof(Q).GetField(name);

			return (T)fld.GetValue( source );
		}

	//======================================================================================================================

		public class ClassWithField {

			public int IntField;
			public string StrField;

		}
		
		[Test]
		public void test_HasField() {

			var check = new ClassWithField();
			check.IntField = 2;
			check.StrField = null;

			// invocations
			IsTrue( HasField( check, "IntField" ) );
			IsTrue( HasField( check, "StrField" ) );
			IsFalse( HasField( check, "NoField" ) );
		}

		[Test]
		public void test_ReadFieldInfo() {

			var check = new ClassWithField();
			check.IntField = 2;
			check.StrField = null;

			// invocations
			var info = ReadFieldInfo( check, "IntField" );
			IsNotNull( info );
			AreEqual( "IntField", info.Name );

			info = ReadFieldInfo( check, "StrField" );
			IsNotNull( info );
			AreEqual( "StrField", info.Name );

			info = ReadFieldInfo( check, "NoField" );
			IsNull( info );
		}

		[Test]
		public void test_ReadField() {

			var check = new ClassWithField();
			check.IntField = 2;
			check.StrField = "Hi";

			// invocations
			var intValue = ReadField<ClassWithField,int>( check, "IntField" );
			AreEqual( 2, intValue );

			var strValue = ReadField<ClassWithField,string>( check, "StrField" );
			AreEqual( "Hi", strValue );
		}

	}

}
