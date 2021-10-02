# DragonCourtTactics
A tactical, dungeon puzzle game flavored with memories from Ye Aulde Dragon Court.


## Build Notes

You will have to run the DragonCourtTactics.sln from VisualStudio 2019 in order to construct 
and deploy the DLLs necessary for the Unity project.   Unity will go into 'safe' mode until 
that is done.

Here are the specific steps necessary:

1. Open the unity project.

2. From 'safe mode' open one of the scripts to enter Visual Studio 2019

3. Rebuild the RealmModel project which will copy DLLs into Assets/_DLLs folder.

4. Switch back to unity, let it update.

5. You still might need to quit out of everything and come back in, but it eventually settles down.
