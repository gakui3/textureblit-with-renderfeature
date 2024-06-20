# Unity URP Texture Blit Render Feature

This project implements a functionality similar to `Graphics.Blit` in the built-in render pipeline using a render feature in Unity's Universal Render Pipeline (URP).  
<img width="512" alt="image" src="https://github.com/gakui3/textureblit-with-renderfeature/assets/65954422/32883d70-7d9a-4533-a680-3f2177008de0">

<br>
<br>

You can configure the timing, Material, Src, and Dest for rendering using the "Texture Blit Render Pass Feature" in the Renderer Data.

- **Src**: If not set, it reads from the current RenderTarget.
- **Dest**: If not set, it writes to RenderTarget0.
- **Material**: If not set, it copies Src to Dest.

For testing, a shader material that converts the image to grayscale is set.

These properties should also be configurable from C#, but this has not been verified.

<img width="512" alt="image" src="https://github.com/gakui3/textureblit-with-renderfeature/assets/65954422/1b76f3ef-d8c8-441a-92b8-e116e00c2731">
