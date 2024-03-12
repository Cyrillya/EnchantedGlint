using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnchantedGlint;

public class EffectGlobalItem : GlobalItem
{
    public class EnchantedGlint : Mod
    {
    }

    internal static readonly HashSet<int> Prefixes = new() {
        PrefixID.Legendary, PrefixID.Legendary2, PrefixID.Godly, PrefixID.Light,
        PrefixID.Demonic, PrefixID.Unreal, PrefixID.Mythical, PrefixID.Ruthless,
        PrefixID.Warding, PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing,
        PrefixID.Quick, PrefixID.Violent
    };

    public override bool PreDrawInInventory(Item item, SpriteBatch sb, Vector2 position, Rectangle frame,
        Color drawColor, Color itemColor, Vector2 origin, float scale) {
        if (!Prefixes.Contains(item.prefix)) {
            return true;
        }

        var texture = ModContent.Request<Texture2D>("EnchantedGlint/Enchanted");
        var shader = ModContent.Request<Effect>("EnchantedGlint/Transform", AssetRequestMode.ImmediateLoad).Value;

        shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly * 0.2f);
        shader.CurrentTechnique.Passes["EnchantedPass"].Apply();
        Main.instance.GraphicsDevice.Textures[1] = texture.Value; // 传入调色板

        sb.End();
        sb.Begin(SpriteSortMode.Deferred, sb.GraphicsDevice.BlendState, sb.GraphicsDevice.SamplerStates[0],
            sb.GraphicsDevice.DepthStencilState, sb.GraphicsDevice.RasterizerState, shader, Main.UIScaleMatrix);
        return true;
    }

    public override void PostDrawInInventory(Item item, SpriteBatch sb, Vector2 position, Rectangle frame,
        Color drawColor, Color itemColor, Vector2 origin, float scale) {
        if (!Prefixes.Contains(item.prefix)) {
            return;
        }

        sb.End();
        sb.Begin(SpriteSortMode.Deferred, sb.GraphicsDevice.BlendState, sb.GraphicsDevice.SamplerStates[0],
            sb.GraphicsDevice.DepthStencilState, sb.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
    }

    public override bool PreDrawInWorld(Item item, SpriteBatch sb, Color lightColor, Color alphaColor,
        ref float rotation, ref float scale, int whoAmI) {
        if (!Prefixes.Contains(item.prefix)) {
            return true;
        }

        var texture = ModContent.Request<Texture2D>("EnchantedGlint/Enchanted");
        var shader = ModContent.Request<Effect>("EnchantedGlint/Transform", AssetRequestMode.ImmediateLoad).Value;

        shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly * 0.2f);
        shader.CurrentTechnique.Passes["EnchantedPass"].Apply();
        Main.instance.GraphicsDevice.Textures[1] = texture.Value;

        sb.End();
        sb.Begin(SpriteSortMode.Deferred, sb.GraphicsDevice.BlendState,
            sb.GraphicsDevice.SamplerStates[0],
            sb.GraphicsDevice.DepthStencilState, sb.GraphicsDevice.RasterizerState, shader,
            Main.GameViewMatrix.TransformationMatrix);
        return true;
    }

    public override void PostDrawInWorld(Item item, SpriteBatch sb, Color lightColor, Color alphaColor, float rotation,
        float scale, int whoAmI) {
        if (!Prefixes.Contains(item.prefix)) {
            return;
        }

        sb.End();
        sb.Begin(SpriteSortMode.Deferred, sb.GraphicsDevice.BlendState, sb.GraphicsDevice.SamplerStates[0],
            sb.GraphicsDevice.DepthStencilState, sb.GraphicsDevice.RasterizerState, null,
            Main.GameViewMatrix.TransformationMatrix);
    }
}