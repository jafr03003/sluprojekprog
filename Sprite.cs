using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using kys;


 
class Sprite {
    private Texture2D Texture;
 
    public Vector2 vector; 
 
    public Rectangle box;
 
    public Sprite(Texture2D texture, Vector2 v, Rectangle rectangle )  
    {
        Texture = texture; 
        vector = v;
        box = rectangle;    
 
    }
 
    
 
    public void Draw(SpriteBatch spritebatch) 
    {
        spritebatch.Draw(Texture, vector, box, Color.White);    
    }
 
}
