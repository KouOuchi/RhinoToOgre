# RhinoToOgre
RhinoToOgre is a data converter and Rhinoceros5 Plug-in. RhinoToOgre can convert Brep,Surface and Mesh from Rhinoceros5 to Ogre3D.

## Screenshot
![Screenshot](./screenshot.jpg?raw=true "screenshot")

## Dependencies
- Ogre3D 2.1 or higher.
- RhinoCommon
- Visual Studio 2015
- Dot.Net 4.6.2(I guess any .Net4 version is acceptable.)

## How To Build
- copy "RhinoCommon.dll" to "RhinoToOgre/lib/x64"
- Configure "OGRE_HOME" environment variable to the folder of Ogre3D SDK
- Open "RhinoToOgre.sln" with Visual Studio 2015
- Build

## How To Run
- Add "RhinoToOgre.rhp" into Rhinoceros in PluginManager.
- Load "RhinoToOgre.rhp"
- Run "RhinoToOgre" command in Rhinoceros5 Command Window
- Select Brep, Surface or Mesh
- Hit Enter

## Development Note
- RhinoToOgre spawns "ogremeshtool.exe" internally.
- In Debug, run "%OGRE_HOME%/bin/Debug/ogremeshttol_d.exe". In Release, run "%OGRE_HOME%/bin/Release/ogremeshttol.exe".
- The argument of ogremeshtool : -v2 -ts 4 -d3d
- RhinoToOgre creates Ogre Mesh Xml in advance.

## Version and History
0.1  * Initial Revision

## Licenses and Authors
  * Licensed under the [MIT License]
  * Copyright &copy; 2017 Kou Ouchi <kou.ouchi@division-engineering.com>

## Related Information
* Ogre3D : http://www.ogre3d.org
* RhinoCommon : http://developer.rhino3d.com
