
# TASKS
=======

Check that modal panel is being used correctly.
* is the modal panel displayed to prevent background clicks ?
Cleanup modal dialog appearance
* modal dialog colors + shapes need some work

[done] figure out the dependency tree
* why are we using System.Runtime.CompilerServices.Unsafe
* https://stackoverflow.com/questions/6653715/view-nuget-package-dependency-hierarchy
* turns out it is a dependency of System.Memory

Can we check the console in PlayModeTests to assert there are no failures?

create play structure in realm
* need to add 'action' construction and validation
* need to create 'move-to' projection when selecting owned agent
* need to apply action changes and produce event stream
* need to create events for end of agent action and end of faction turn

flag icon to turn flags on/off

create credits panel for main scene

[done] Move RealmModel to a separate project.
* [need] figure out how to copy DLLs based on developer local settings

Integrate verbose with RealmModel ( after .net5 change? )

better 'wall' tiles
better floor tiles

better camera angle and zoom 
* as you go up, look down more
* ipad support pinch zoom
* pc supports wheel zoom

better select and turning
* single click on agent, or double click will select/deselect
* prettier tile/agent select icon

create stupid commander in realm

can we put the test utilities from PlayModeTests into EditModeTests?  ( and still use them in PlayModeTests? )

draw flag objects in tiles for: Lever, Chest, Sack, Hostage, Door, Entry, etc.

Move 'edit' panels to bottom of screen, similar to Hexonia

# OBJECTIVES
============

automate credits from asset meta

[Done] Figure out why I am stuck at .net4.7.1, try to update to 5+
* Unity does not support .net5, it could come out at any time though
* [Done] turns out I can 'double' target, so I am doing that on my support projects

create unity play test suite

create play scene ( play one puzzle )

create campaign scene

Automate puzzle creation and testing

Better play tiles

Better monster graphics

Strategic collecting.

3D models for agents

3D models for tile contents
