﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OpenWorld
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private Model model;
		private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
		private Matrix view = Matrix.CreateLookAt(new Vector3(0, 10, 10), new Vector3(0, 0, 0), Vector3.UnitY);
		private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 100f);

		private Vector3 position;
		private float angle;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			Texture2D texture = Content.Load<Texture2D>("Models/AsteroidTexture");
			position = new Vector3(0, 0, 0);
			angle = 0;

			model = Content.Load<Model>("Models/Cube");
			AddTexture(texture);
			// TODO: use this.Content to load your game content here
		}

		private void AddTexture(Texture2D texture)
		{
			foreach (ModelMesh mesh in model.Meshes)			
				foreach (BasicEffect effect in mesh.Effects)
				{
					effect.TextureEnabled = true;
					effect.Texture = texture;
				}

		}


		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			/*
			position += new Vector3(0, 0.01f, 0);
			world = Matrix.CreateTranslation(position);
			*/


			angle += 0.03f;
			world = Matrix.CreateTranslation(position);
			world = Matrix.CreateRotationX(angle) * Matrix.CreateTranslation(position);

			view = Matrix.CreateLookAt(new Vector3(0, 10, 10), new Vector3(0, 0, 0), Vector3.UnitY);

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			DrawModel(model, world, view, projection);

			base.Draw(gameTime);
		}

		private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
		{
			foreach (ModelMesh mesh in model.Meshes)
			{
				foreach (BasicEffect effect in mesh.Effects)
				{


					effect.World = world;
					effect.View = view;
					effect.Projection = projection;
				}

				mesh.Draw();
			}
		}

	}
}
