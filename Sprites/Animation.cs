using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace running_out_of_time;

public class Animation
{
    int rows, columns, currentFrame, totalFrames;
    public int width { get; private set; }
    public int height { get; private set; }

    public Texture2D spriteSheet { get; }

    float frameLength;

    float lastFrameTime;

    public Animation(Texture2D _texture, int frameWidth, int frameHeight, float _frameLength)
    {
        columns = (int)((float)_texture.Width / frameWidth);
        rows = (int)((float)_texture.Height / frameHeight);
        currentFrame = 0;
        totalFrames = rows * columns;
        width = frameWidth;
        height = frameHeight;
        spriteSheet = _texture;
        frameLength = _frameLength;
        lastFrameTime = 0;
    }

    public void Reset()
    {
        currentFrame = 0;
    }

    public Rectangle GetRectangle()
    {
        int row = currentFrame / columns;
        int column = currentFrame % columns;
        return new Rectangle(width * column, height * row, width, height);
    }

    public void UpdateFrame(GameTime gameTime)
    {
        if ((float)gameTime.TotalGameTime.TotalSeconds > lastFrameTime + frameLength)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            lastFrameTime = (float)gameTime.TotalGameTime.TotalSeconds;
        }
    }
}