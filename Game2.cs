using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace kys
{
	
	public class Game2 : Game
	{
		

		GraphicsDeviceManager graphics; 
		SpriteBatch spriteBatch; 

        Sprite laser;  
        Sprite among; 

        Sprite tok;

		Sprite div;


		Texture2D amogus;
		Vector2 pos = new Vector2(10, 240); //vektor för amogus
		Texture2D lsar;
		Vector2 pos2 = new Vector2(10, 0); //vektor för laser
		Texture2D coin;
		Vector2 pos3 = new Vector2(20, 0); //vektor för coin
		Texture2D diva;
		Vector2 pos4 = new Vector2(20, 0); //vektor för diva
		Vector2 dspeed = new Vector2(7, 7); //speed vektor för diva 

		


		Rectangle lsar2 = new Rectangle(100, -200, 150, 200);
		Rectangle amogus2 = new Rectangle(100, 800, 300, 200); //Bildar en hitbox rektangel för amogus
		Rectangle coin2 = new Rectangle(50, 400, 150, 100); //Bildar en hitbox rektangel för coin
		private Rectangle diva2 = new Rectangle(-50, -800, 200, 300); //Bildar en hitbox rektangel för divan

		int speed = 7; //Farten för laser stråle
		SpriteFont ScoreFont;
		SpriteFont LevelFont;
		int score = 0; //Score poäng
		int level = 1; //Score poäng
		int pose = 0; //gör så level 2 inte upprepar randomisingen av divan
		
		


		//KOmentar
		public Game2()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 2000;  //Storlek för skärmen, bredd
			graphics.PreferredBackBufferHeight = 1200;  //Storlek för skärmen, höjd
			graphics.ApplyChanges();
			Content.RootDirectory = "Content";


		}

		
		protected override void Initialize()
		{

			base.Initialize();
		}

	
		protected override void LoadContent()
		{
            spriteBatch = new SpriteBatch(GraphicsDevice);
			amogus = Content.Load<Texture2D>("amogus");   
			lsar = Content.Load<Texture2D>("lsar");
			coin = Content.Load<Texture2D>("coin");
			ScoreFont = Content.Load<SpriteFont>("Score");
			diva = Content.Load<Texture2D>("diva");
			LevelFont = Content.Load<SpriteFont>("Level");
			
            laser = new Sprite(lsar, pos2, lsar2);
            among = new Sprite(amogus, pos, amogus2); 
            tok = new Sprite(coin, pos3, coin2);
			div = new Sprite(diva, dspeed, diva2);
		}

		
		protected override void UnloadContent()
		{
			
		}

		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();  //om man trycker exit ska man gå ut ur spelet
			{
				KeyboardState kstate = Keyboard.GetState();
				if (kstate.IsKeyDown(Keys.Right))
					amogus2.X++;
				if (kstate.IsKeyDown(Keys.Left))
					amogus2.X--;
				if (kstate.IsKeyDown(Keys.Down))
					amogus2.Y++;
				if (kstate.IsKeyDown(Keys.Up))
					amogus2.Y--;
				if (kstate.IsKeyDown(Keys.Right))
					amogus2.X += 10;
				if (kstate.IsKeyDown(Keys.Left))
					amogus2.X -= 10;
				if (kstate.IsKeyDown(Keys.Down))
					amogus2.Y += 10;
				if (kstate.IsKeyDown(Keys.Up))
					amogus2.Y -= 10;
				base.Update(gameTime);


				if (diva2.Y < 0 || diva2.Y > Window.ClientBounds.Height)
				{ 
					dspeed.Y *= -1; //när divan rör toppen av skärmen ska den studsa tillbaka
				}
				if (diva2.X < 0 || diva2.X > Window.ClientBounds.Width)
				{ 
					dspeed.X *= -1; //när divan rör botten av skärmen ska den studsa tillbaka
				}
								

				if (diva2.X <= 0)
                {
					pos4.X = -pos4.X; //vektorn omvänd
				}

				if (diva2.Y <= 0)
				{
					pos4.Y = -pos4.Y; //vektorn omvänd
				}

				if (diva2.Intersects(amogus2) || lsar2.Intersects(amogus2)) 
				{
					Exit(); //om divan eller laser rör amogus ska spelet slutas
				}

					if (true)
				{
					lsar2.Y += speed; //laser speed
				}

				if (level == 4) 
                {
					if (lsar2.Y > 1280) //lasern ska komma tillbaka efter level 4
					{
						System.Random r = new Random();
						lsar2.X = r.Next(0, 2000); //i intervallet kan laser teleportera
						lsar2.Y = 0;
						lsar2.Y = speed;
					}
				}
				
				if (score >= 11) //när score är 11 ska divan röra sig på ett sätt
                {
					diva2.Location += dspeed.ToPoint();
				}


				if (score < 10) 
				{
					if (lsar2.Y > 1280) // om scoreär under 10 och lasern nått botten av skärmen ska den teleportera en ny posititon
					{
						System.Random r = new Random();
						lsar2.X = r.Next(0, 2000); //i intervallet kan laser teleportera
						lsar2.Y = 0;
						lsar2.Y = speed++;
					}
				}
				if (amogus2.Intersects(coin2)) //om amogus rör coin ska  dne läggas till en till i score och spawna en ny coin
				{
					System.Random r = new Random();
					coin2.X = r.Next(0, 2000); //i intervallet kan coin teleportera
					coin2.Y = r.Next(0, 1080); //i intervallet kan coin teleportera
					score++;
					if (score % 10 == 0)
					{
						level++; //ökar leveln efter varje 10 poäng

						int speedup = 5; // speed för när diva studsar på väggen vilket ger den samma värde vid ökning av speed

                        if (dspeed.X >= 0)
                        {
                            dspeed.X += speedup;
                        }
                        else
						{
							dspeed.X -= speedup;
						}

						if (dspeed.Y >= 0) 
						{
							dspeed.Y += speedup;
						}
						else
						{
							dspeed.Y += speedup; 
						}

					}

				}

				if (score > 10) //om score är mer än 10 ska divan spawna.
				{
					if (pose == 0)
					{
						System.Random r = new Random();
						diva2.X = r.Next(0, 2000); //i intervallet kan divan teleportera
						diva2.Y = r.Next(0, 1040);
						pose = 1; 
						{
							
						}
						
						if (lsar2.Y > 1280) // då ska också lasern placeras utanför skärmen temporärt 
						{
							System.Random s = new Random();
							lsar2.X = s.Next(-200, -200); //i intervallet kan laser teleportera
							lsar2.Y = 0;
						}

					}

		




					base.Update(gameTime);
				}
			}
		}

		
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here.
			spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
			Matrix.CreateScale(0.9f)); //storlek för figur
            laser.Draw(spriteBatch);
            among.Draw(spriteBatch);
            tok.Draw(spriteBatch);
            div.Draw(spriteBatch);          
			spriteBatch.Draw(amogus, amogus2, Color.White); //läs ut och ge färg för figur
			spriteBatch.Draw(diva, diva2, Color.White); //läs ut och ge färg för figur
			spriteBatch.Draw(lsar, lsar2, Color.White); //läs ut och ge färg för figur			
			spriteBatch.Draw(coin, coin2, Color.Yellow); //läs ut och ge färg för figur
			spriteBatch.DrawString(ScoreFont, "Score: " + score, new Vector2(950, 100), Color.Black); //Karaktär för spritefont 
			spriteBatch.DrawString(LevelFont, "Level " + level, new Vector2(950, 200), Color.Black); //Karaktär för spritefont 
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
