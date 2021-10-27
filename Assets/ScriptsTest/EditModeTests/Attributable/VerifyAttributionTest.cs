//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Atributable {

    using NUnit.Framework;
    using Shared;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using static NUnit.Framework.Assert;

	public class VerifyAttributionTest {

        static readonly string ResourcesFolder = "Assets/Resources/Attributable/";

        static readonly string[] AttributionFolders = {
            "Music"
		};

        // Ensure that every asset under Resources/Attributable has attribution information
        [Test]
        public void EveryResourceFileHasSomeAttribution() {

            // map by title
            var attrList = AttributionList.ReadAttributeYml();
            Dictionary<string,Attribution> map = new Dictionary<string, Attribution>();
            foreach ( var attr in attrList.Attribution ) {
                map[attr.Path] = attr;
			}

            string allKeys = "["+String.Join(",", map.Keys)+"]";

            // read file names ( testing only )
            foreach ( var folder in AttributionFolders ) { 

                var files = new DirectoryInfo( ResourcesFolder + folder ).GetFiles();
               
                foreach ( var file in files ) {

                    var path = file.FullName;
                    if (path.EndsWith(".meta")) continue;

                    int cut = path.IndexOf( folder );
                    var endPath = path.Substring( cut ).Replace("\\","/");

                    IsTrue( map.ContainsKey(endPath), "Expected Path ["+endPath+"] from "+allKeys );
				}
            }
        }

        // Ensure that every asset under Resources/Attributable has attribution information
        [Test]
        public void EveryAttributionHasSomeResourceFile() {

            // map by title
            var attrList = AttributionList.ReadAttributeYml();
            Dictionary<string,Attribution> map = new Dictionary<string, Attribution>();
            foreach ( var attr in attrList.Attribution ) {

                // find path
                var path = ResourcesFolder + attr.Path;
                var info = new FileInfo( path );

                IsTrue( info.Exists,  "Expected Filepath = ["+info.FullName+"]");
			}

        }

    }
}
