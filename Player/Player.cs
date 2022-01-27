using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace running_out_of_time;
public class Player : AnimatedSprite
{
    public Vector2 velocity;
    public float speed = 3;
    public float jumpStrength = 100;
    public InputWrapper input;
    public Vector2 gravity = Vector2.Zero;
    public Vector2 gravityScale = Vector2.One;
    public Animation running;

    private bool showingCollider;
    private Texture2D colliderSprite;

    CollisionManager.AABB collider;
    public Player(Texture2D texture) : base(texture, texture.Width, texture.Height)
    {
        input = new InputWrapper()
        {
        jump = Keys.Up,
        left = Keys.Left,
        right = Keys.Right
        };

        collider.HalfExtents = new Vector2(texture.Width / 2, texture.Height / 2);
    }

    public override void Update(GameTime gametime)
    {

        KeyboardState ks = Keyboard.GetState();
        
        if (ks.IsKeyDown(input.jump))
        {
            velocity.Y -= jumpStrength;
        }

        if (ks.IsKeyDown(input.left))
        {
            velocity.X -= speed;
        }

        if (ks.IsKeyDown(Keys.Right))
        {
            velocity.X += speed;
        }

        spriteEffect = velocity.X >= 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        velocity += gravity * gravityScale;
        velocity *= (float)gametime.ElapsedGameTime.TotalSeconds;

        var sweep = CollisionManager.TryMoveAABB(collider, velocity);
        if (!sweep.hit.valid || sweep.time <= 0)
            collider.Position += velocity;

        else 
            collider.Position = sweep.position;
        position = collider.Position;
        velocity.X = 0;
        base.Update(gametime);
    }
}


