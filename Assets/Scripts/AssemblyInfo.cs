//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//
//
//	This is supposed to make 'internal' fields accessible to our test asemblies

using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("PlayModeTestAssembly"),InternalsVisibleTo("PlayModeTestAssembly")]
class AssemblyInfo {}

