using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

class TextureBlitRenderPass : ScriptableRenderPass
{
    public RenderTexture dest;
    public Texture2D src;
    public Material blitMaterial;

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData) { }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer commandBuffer = CommandBufferPool.Get("TextureBlit");
        CameraData camData = renderingData.cameraData;

        int texId = Shader.PropertyToID("_TempTexture");
        int w = camData.camera.scaledPixelWidth;
        int h = camData.camera.scaledPixelHeight;
        commandBuffer.GetTemporaryRT(texId, w, h, 0, FilterMode.Bilinear);

        // CameraTargetを一時的なレンダテクスチャに移して、それに対してシェーダーをかける
        commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, texId);

        if (src == null && dest != null)
        {
            if (blitMaterial != null)
            {
                commandBuffer.Blit(texId, dest, blitMaterial);
            }
            else
            {
                commandBuffer.Blit(texId, dest);
            }
        }
        else if (dest == null && src != null)
        {
            if (blitMaterial != null)
            {
                commandBuffer.Blit(src, BuiltinRenderTextureType.CurrentActive, blitMaterial);
            }
            else
            {
                commandBuffer.Blit(src, BuiltinRenderTextureType.CurrentActive);
            }
        }
        else if (dest == null && src == null)
        {
            if (blitMaterial != null)
            {
                commandBuffer.Blit(texId, BuiltinRenderTextureType.CurrentActive, blitMaterial);
            }
            else
            {
                commandBuffer.Blit(texId, BuiltinRenderTextureType.CurrentActive);
            }
        }
        else
        {
            if (blitMaterial != null)
            {
                commandBuffer.Blit(src, dest, blitMaterial);
            }
            else
            {
                commandBuffer.Blit(src, dest);
            }
        }

        context.ExecuteCommandBuffer(commandBuffer);
        CommandBufferPool.Release(commandBuffer);
    }

    public void SetParam(Texture2D _src, RenderTexture _dest, Material material)
    {
        src = _src;
        dest = _dest;
        blitMaterial = material;
    }
}