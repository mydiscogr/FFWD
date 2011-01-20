#region File Description
//-----------------------------------------------------------------------------
// GpuSkinnedModelProcessor.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using PressPlay.FFWD;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PressPlay.FFWD.Import.Animation
{
    /// <summary>
    /// Custom processor extends the builtin framework ModelProcessor class,
    /// adding animation support.
    /// </summary>
    [ContentProcessor(DisplayName = "GPU Skinned Model")]
    public class GpuSkinnedModelProcessor : ModelProcessor
    {
        /// <summary>
        /// The main Process method converts an intermediate format content pipeline
        /// NodeContent tree to a ModelContent object with embedded animation data.
        /// </summary>
        public override ModelContent Process(NodeContent input, ContentProcessorContext context)
        {
            SkinningData skinningData = SkinningHelpers.GetSkinningData(input, context, SkinnedEffect.MaxBones);
            Microsoft.Xna.Framework.Quaternion rotation = Microsoft.Xna.Framework.Quaternion.CreateFromYawPitchRoll(MathHelper.ToRadians(RotationY), MathHelper.ToRadians(RotationX), MathHelper.ToRadians(RotationZ));
            Matrix m = Matrix.CreateScale(Scale) * Matrix.CreateFromQuaternion(rotation);

            skinningData.BakedTransform = m;

            // Reset scene and rotation values so the model processor does not take them into account
            Scale = 1;
            RotationX = 0;
            RotationY = 0;
            RotationZ = 0;

            ModelContent model = base.Process(input, context);
            model.Tag = skinningData;
            return model;
        }
    }
}
