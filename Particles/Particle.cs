using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.Particles
{
    class Particle
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }           
        public Vector2 Velocity { get; set; }        
        public float Angle { get; set; }            
        public float AngularVelocity { get; set; }    
        public Color Color { get; set; }      
        public float Opacity { get; set; }
        public float Size { get; set; }                
        public int TTL { get; set; }        
        Rectangle SourceRectangle { get; set; } 
        Vector2 Origin { get; set; }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int ttl, float opacity)
        {
            Setup(texture, position, velocity, angle, angularVelocity, color, size, ttl, opacity);
        }

        public void Setup(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int ttl, float opacity)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
            Opacity = opacity;

            SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

        }

        public void Update()
        {
            TTL--;
            Position += Velocity;
            Angle += AngularVelocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color * Opacity, Angle, Origin, Size, SpriteEffects.None, 0f);
        }
    }
}
