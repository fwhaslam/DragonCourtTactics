
# TASKS
=======

[done] create play scene, load from edit, exit back to edit

[partially done] create play structure in realm
* [done] added 'game' wrapper around puzzlemap
* need to add 'action' construction and validation
* need to apply action changes and produce event stream
* need to create events for end of agent action and end of faction turn

flag icon to turn flags on/off

add music folder under resource
* automate credits creation, resource/attribution folder?
add tests to verify that music has attribution information
create credits panel for main scene

Cleanup modal dialog appearance
* modal dialog colors + shapes need some work

Move RealmModel to a separate project.

Integrate verbose with RealmModel ( after .net5 change? )

better 'wall' tiles

better floor tiles

better camera angle and zoom 
* as you go up, look down more
* ipad support pinch zoom
* pc supports wheel zoom

better select and turning
* single click in empty space will 'drag'
* single click on agent, or double click will select/deselect
* prettier tile/agent select icon

create stupid commander in realm

can we put the test utilities from PlayModeTests into EditModeTests?  ( and still use them in PlayModeTests? )

draw flag objects in tiles for: Lever, Chest, Sack, Hostage, Door, Entry, etc.


# OBJECTIVES
============

automate credits from asset meta

[Done] Figure out why I am stuck at .net4.7.1, try to update to 5+
* Unity does not support .net5, it could come out at any time though

create unity play test suite

create play scene ( play one puzzle )

create campaign scene

Automate puzzle creation and testing

Better play tiles

Better monster graphics

Strategic collecting.

3D models for agents

3D models for tile contents
