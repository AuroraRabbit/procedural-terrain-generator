# Procedural Terrain Generator
A college project with the goal of making procedural terrain look believable.

## Instructions
All variables within the Terrain Generation script but the seed are able to be changed at runtime. Explanations of the variables are as follows:
- **X Size**: Controls the size of the mesh in X
- **Y Size**: Controls the size of the mesh in Y
- **Scale**: Zooms in and out of the noise
- **Gen Height**: Controls the height of the generation
- **Island Falloff**: Multiples the terrain height (on the Y axis) by its closeness to the centre of the terrain (on the X axis). In practice, this is used to create a smooth island falloff map.
- **Biome Dropdown**: Selects which biome to generate.
- **Biome Selection (should be avoided)**: Contains the information about the selected biome. This can technically be changed but doing so should be avoided. 
