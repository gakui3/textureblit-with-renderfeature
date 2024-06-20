using UnityEngine;
using UnityEngine.Rendering.Universal;


public class TextureBlitRenderPassFeature : ScriptableRendererFeature
{
    public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;
    public Material blitMaterial = null;
    public Texture2D src;
    public RenderTexture dest;

    TextureBlitRenderPass m_ScriptablePass = new TextureBlitRenderPass();


    public override void Create()
    {
        m_ScriptablePass.dest = dest;
        m_ScriptablePass.src = src;
        m_ScriptablePass.blitMaterial = blitMaterial;
        m_ScriptablePass.renderPassEvent = Event;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(m_ScriptablePass);
    }
}