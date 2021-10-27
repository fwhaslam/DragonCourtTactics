using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared { 

    /// <summary>
    /// Class to read from Attribute.yml for us in constructing credits.
    /// </summary>
    public class Attribution {

        public string Path {  get; set; }
        public string Title {  get; set; }
        public string License {  get; set; }
        public string Source {  get; set; }
        public string Creator {  get; set; }
        public string Link {  get; set; }

    }

}
