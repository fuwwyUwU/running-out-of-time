using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace running_out_of_time;

public class Sprite
{
    Texture2D texture;
    public Vector2 position;
    protected Color color; 

    protected Rectangle source;

    public float rotation;
    protected Vector2 origin;

    public Vector2 scale;

    protected int layerDepth;

    protected SpriteEffects spriteEffect = SpriteEffects.None;

    public void SetColor(Color newColor)
    {
        color = newColor;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, source, color, rotation, origin, scale, spriteEffect, layerDepth);
    }   

    public Sprite(Texture2D _texture)
    {
        texture = _texture;
        color = Color.White;
        source = new Rectangle(0, 0, _texture.Width, _texture.Height);
        rotation = 0;
        origin = new Vector2(texture.Width * .5f, texture.Height * .5f);
        scale = Vector2.One;
        layerDepth = 0;
    }

    protected Sprite()
    {
        color = Color.White;
        rotation = 0;
        scale = Vector2.One;
        layerDepth = 0;
    }
}