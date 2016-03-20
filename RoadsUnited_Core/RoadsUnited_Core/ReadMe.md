This file can be used to post ideas or planned updates. Figure sharing info would help to share pending updates.

Unlawful:	Reorganizing RoadsUnited_Core.cs|ReplaceNetTextures() to allow for easier navigation and some simplications.
   - Different arrows for different roads.
   - Diagonal L/R for exit ramps etc.
   - L/LR/R with or without "ONLY"
   - RR crossings
   - Network skins compatibility to accomadate alternate setups for different intersections
     - [future] alt node textures
   - Add new exception for file substitions
     - to exclude redundant texture file
       -saves file space and reduces ram usage
   - Either find way to use mip levels for dds files or generate without mipmaps

American Roads Fork:
   - New Mod Name
     - American Infrastructure
     - Avoids confusion with predecessor mod American Roads
    - Remove redundant files and substitute with overrides where practical
    - enforce geometric symetry within textures to reduce incongruent nodes
    - apr/aci/maintex revisions


Different node textures to accommodate your suggestion.

Replacing individual textures on segments to allow "true" turning lanes (NExt roads) w/ solid lines.

I've been keeping quiet about it, but if you look at my imgur posts, you will see my American styled traffic lights. Using his mod alongside would allow for true awesomeness and a whole new level of realism (graphics wise).

@Killface: Is there a way I can upload updates to the coremod page? It would help streamline updates. Also need access to update the american version and if welcomed, the german version if you would like. That way I can share my base textures with you and we can exhange psds or revisions as needed (post release of my version, need to get mine done beforehand).

Killface:  
- Getting rid of segment.lod render distance = -1 and implement real lod textures by default
- make color changer work in-game, not only onlevelloaded
- add parking space markings as props, seems more flexible and might avoid flipping
