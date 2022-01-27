using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace running_out_of_time;

public class AnimatedSprite : Sprite
{
    Animation animation;

    public AnimatedSprite(Texture2D _texture, int frameWidth, int frameHeight) : base()
    {
        origin = new Vector2(frameWidth / 2, frameHeight / 2);
        animation = new Animation(_texture, frameWidth, frameHeight, 1);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(animation.spriteSheet, position, animation.GetRectangle(), color, rotation, origin, scale, spriteEffect, layerDepth);
    }

    public virtual void Update(GameTime gameTime)
    {
        animation.UpdateFrame(gameTime);
    } 

    public void SetAnimation(Animation newAnimation)
    {
        animation.Reset();
        animation = newAnimation;
        origin = new Vector2(newAnimation.width, newAnimation.height);
    }
}