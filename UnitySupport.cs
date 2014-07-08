using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GamesLibrary
{
	public static class Extensions
	{
		public static void DrawEx(this SpriteBatch spriteBatch, string texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color)
		{
			Texture2D t2D = TextureManager.Instance.getTexture(texture) as Texture2D;
			spriteBatch.Draw(t2D, destinationRectangle, sourceRectangle, color);
		}
		
		public static void DrawEx(this SpriteBatch spriteBatch, string texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
		{
			Texture2D t2D = TextureManager.Instance.getTexture(texture) as Texture2D;
			spriteBatch.Draw(t2D, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
		}

		public static void loadTexture(this BaseGame baseGame, string identifier, string assetName)
		{
			Texture2D tx2d = Resources.Load<Texture2D>(@assetName);
			TextureManager.Instance.setTexture(identifier, tx2d);
		}
	}

	public class BlendState
	{
		public static readonly BlendState AlphaBlend;
	}

	public enum ButtonState
	{
		Pressed, Released
	}

	public class Color
	{
		public static Color Black { get { return Color.fromUnity(UnityEngine.Color.black); } }
		public static Color Beige { get { return Color.fromUnity(new UnityEngine.Color(128, 128, 128)); } } // TODO: Get real rgb
		public static Color CornflowerBlue { get { return Color.fromUnity(UnityEngine.Color.blue); } } // TODO: Get real rgb
		public static Color DarkGray { get { return Color.fromUnity(UnityEngine.Color.gray); } } // TODO: Get real rgb
		public static Color ForestGreen { get { return Color.fromUnity(UnityEngine.Color.green); } } // TODO: Get real rgb
		public static Color Gray { get { return Color.fromUnity(UnityEngine.Color.gray); } }
		public static Color Green { get { return Color.fromUnity(UnityEngine.Color.green); } }
		public static Color LightBlue { get { return Color.fromUnity(UnityEngine.Color.blue); } } // TODO: Get real rgb
		public static Color Red { get { return Color.fromUnity(UnityEngine.Color.red); } }
		public static Color Tan { get { return Color.fromUnity(new UnityEngine.Color(128, 128, 128)); } } // TODO: Get real rgb
		public static Color White { get { return Color.fromUnity(UnityEngine.Color.white); } }
		public static Color Yellow { get { return Color.fromUnity(UnityEngine.Color.yellow); } }
		public static Color YellowGreen { get { return Color.fromUnity(UnityEngine.Color.yellow); } } // TODO: Get real rgb

		public float r { get; private set; }
		public float g { get; private set; }
		public float b { get; private set; }
		public float a { get; private set; }

		public Color(Vector3 vector) : this(vector.X, vector.Y, vector.Z) { }

		public Color(float r, float g, float b) : this(r, g, b, 0) { }

		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public Vector3 ToVector3()
		{
			return new Vector3(r, g, b);
		}

		private static Color fromUnity(UnityEngine.Color colorUnity)
		{
			return new Color(colorUnity.r, colorUnity.g, colorUnity.b, colorUnity.a);
		}
	}

	public class ContentManager
	{
		public string RootDirectory { get; set; } // TODO: Implement

		public virtual T Load<T>(string assetName) where T : new() //where T : UnityEngine.Object
		{
			//return Resources.Load<T>(assetName);
			//return default(T); // TODO: Impelement correctly
			return new T();
		}
	}

	public class Game : IDisposable
	{
		public ContentManager Content { get; set; } // TODO: Implement
		public GraphicsDevice GraphicsDevice { get; set; } // TODO: Implement

		public void Dispose()
		{
			// TODO: Probably don't need this implementation, use SceneLoad, etc.
			UnloadContent();
		}

		public void Exit()
		{
			Application.Quit();
		}

		public void Run()
		{
			// TODO: Probably don't need this implementation, use SceneLoad, etc.
			Initialize();

			while (true)
			{
				GameTime gameTime = new GameTime();
				Update(gameTime);
				Draw(gameTime);
			}
		}

		protected virtual void Initialize()
		{
		}

		protected virtual void LoadContent()
		{
		}

		protected virtual void UnloadContent()
		{
		}

		protected virtual void Update(GameTime gameTime)
		{
		}

		protected virtual void Draw(GameTime gameTime)
		{
		}
	}

	public static class GamePad
	{
		public static GamePadState GetState(PlayerIndex playerIndex)
		{
			return new GamePadState(playerIndex);
		}
	}

	public struct GamePadButtons
	{
		private PlayerIndex _playerIndex;

		public GamePadButtons(PlayerIndex playerIndex)
		{
			_playerIndex = playerIndex;
		}

		public ButtonState Back
		{
			get
			{
				return ButtonState.Released; // TODO: Implement
			}
		}
	}

	public struct GamePadState
	{
		private PlayerIndex _playerIndex;

		public GamePadState(PlayerIndex playerIndex)
		{
			_playerIndex = playerIndex;
		}

		public GamePadButtons Buttons
		{
			get
			{
				return new GamePadButtons(_playerIndex);
			}
		}
	}

	public class GameTime
	{
		public TimeSpan ElapsedGameTime { get; set; } // TODO: Implement
		public bool IsRunningSlowly { get; set; } // TODO: Implement
		public TimeSpan TotalGameTime { get; set; } // TODO: Implement

		public GameTime()
		{
			ElapsedGameTime = TimeSpan.FromSeconds(Time.deltaTime);
			IsRunningSlowly = false; // TODO: Implement
			TotalGameTime = TimeSpan.FromSeconds(Time.time);
		}
	}

	public class GraphicsDevice
	{
		public Rectangle ScissorRectangle { get; set; } // TODO: Implement

		public Viewport Viewport 
		{ 
			get
			{
				if (_Viewport == null)
					_Viewport = new Viewport();
				return _Viewport;
			}
			set { _Viewport = value; } 
		} // TODO: Implement correctly
		private Viewport _Viewport = null;

		public static void Clear(Color color)
		{
			// TODO: Implement
		}
	}

	public class GraphicsDeviceManager
	{
		public int PreferredBackBufferWidth
		{
			get	{ return Screen.width; }
			set { _newScreenWidth = value; }
		}

		public int PreferredBackBufferHeight
		{
			get { return Screen.height; }
			set { _newScreenHeight = value; }
		}

		private int _newScreenWidth = -1;
		private int _newScreenHeight = -1;

		public GraphicsDeviceManager()
		{
		}

		public GraphicsDeviceManager(Game game)
		{
		}

		public void ApplyChanges()
		{
			int screenWidth = _newScreenWidth;
			if (screenWidth == -1)
				screenWidth = Screen.width;

			int screenHeight = _newScreenHeight;
			if (screenHeight == -1)
				screenHeight = Screen.height;

			Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreen);

			_newScreenWidth = -1;
			_newScreenHeight = -1;
		}
	}

	public static class Keyboard
	{
		public static KeyboardState GetState()
		{
			return new KeyboardState();
		}
	}

	public struct KeyboardState
	{
		public bool IsKeyDown(Keys key)
		{
			return UnityEngine.Input.GetKey((KeyCode)key);
		}
	}

	public enum Keys
	{
		C = UnityEngine.KeyCode.C,
		Delete = UnityEngine.KeyCode.Delete,
		Escape = UnityEngine.KeyCode.Escape, 
		Insert = UnityEngine.KeyCode.Insert,
		PageDown = UnityEngine.KeyCode.PageDown,
		PageUp = UnityEngine.KeyCode.PageUp,
		RightControl = UnityEngine.KeyCode.RightControl, 
		RightShift = UnityEngine.KeyCode.RightShift
	}

#if false
	public class Matrix
	{
		private float _m00;
		private float _m01;
		private float _m02;
		private float _m03;
		private float _m10;
		private float _m11;
		private float _m12;
		private float _m13;
		private float _m20;
		private float _m21;
		private float _m22;
		private float _m23;
		private float _m30;
		private float _m31;
		private float _m32;
		private float _m33;

		private UnityEngine.Matrix4x4 _mxUnity;

		public static Matrix Identity { get { return Matrix.fromUnity(UnityEngine.Matrix4x4.identity); } }

		public Matrix(float m00, float m01, float m02, float m03,
		              float m10, float m11, float m12, float m13,
		              float m20, float m21, float m22, float m23,
		              float m30, float m31, float m32, float m33)
		{
			_m00 = m00;
			_m01 = m01;
			_m02 = m02;
			_m03 = m03;
			_m10 = m10;
			_m11 = m11;
			_m12 = m12;
			_m13 = m13;
			_m20 = m20;
			_m21 = m21;
			_m22 = m22;
			_m23 = m23;
			_m30 = m30;
			_m31 = m31;
			_m32 = m32;
			_m33 = m33;
		}

		public static Matrix operator *(Matrix lhs, Matrix rhs)
		{
			return Matrix.fromUnity(lhs.toUnity() * rhs.toUnity());
		}

		public static Matrix CreateRotationZ(float radians)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(UnityEngine.Vector3.zero, UnityEngine.Quaternion.AngleAxis(Mathf.Rad2Deg * radians, UnityEngine.Vector3.forward), UnityEngine.Vector3.one));
		}

		public static Matrix CreateScale(float scale)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity, new UnityEngine.Vector3(scale, scale, scale)));
		}

		public static Matrix CreateScale(float xScale, float yScale, float zScale)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity, new UnityEngine.Vector3(xScale, yScale, zScale)));
		}
		
		public static Matrix CreateScale(Vector3 scales)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity, new UnityEngine.Vector3(scales.X, scales.Y, scales.Z)));
		}
		
		public static Matrix CreateTranslation(Vector3 position)
		{
			UnityEngine.Matrix4x4 mxUnity = UnityEngine.Matrix4x4.TRS(new UnityEngine.Vector3(position.X, position.Y, position.Z), UnityEngine.Quaternion.identity, UnityEngine.Vector3.one);
			return Matrix.fromUnity(mxUnity);
		}
		
		public static Matrix CreateTranslation(float xPosition, float yPosition, float zPosition)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(new UnityEngine.Vector3(xPosition, yPosition, zPosition), UnityEngine.Quaternion.identity, UnityEngine.Vector3.one));
		}

		public static Matrix Invert(Matrix matrix)
		{
			return Matrix.fromUnity(matrix.toUnity().inverse);
		}

		private static Matrix fromUnity(UnityEngine.Matrix4x4 mxUnity)
		{
			return new Matrix(mxUnity.m00, mxUnity.m01, mxUnity.m02, mxUnity.m03,
			                  mxUnity.m10, mxUnity.m11, mxUnity.m12, mxUnity.m13,
			                  mxUnity.m20, mxUnity.m21, mxUnity.m22, mxUnity.m23,
			                  mxUnity.m30, mxUnity.m31, mxUnity.m32, mxUnity.m33);
		}

		// TODO: Was private
		internal UnityEngine.Matrix4x4 toUnity()
		{
			UnityEngine.Matrix4x4 mxUnity = new UnityEngine.Matrix4x4();
			mxUnity.m00 = _m00;
			mxUnity.m01 = _m01;
			mxUnity.m02 = _m02;
			mxUnity.m03 = _m03;
			mxUnity.m10 = _m10;
			mxUnity.m11 = _m11;
			mxUnity.m12 = _m12;
			mxUnity.m13 = _m13;
			mxUnity.m20 = _m20;
			mxUnity.m21 = _m21;
			mxUnity.m22 = _m22;
			mxUnity.m23 = _m23;
			mxUnity.m30 = _m30;
			mxUnity.m31 = _m31;
			mxUnity.m32 = _m32;
			mxUnity.m33 = _m33;
			return mxUnity;
		}
	}
#else
	public class Matrix
	{
		private UnityEngine.Matrix4x4 _mxUnity;
		
		public static Matrix Identity { get { return Matrix.fromUnity(UnityEngine.Matrix4x4.identity); } }
		
		public Matrix(UnityEngine.Matrix4x4 mxUnity)
		{
			_mxUnity = mxUnity;
		}
		
		public static Matrix operator *(Matrix lhs, Matrix rhs)
		{
			//return Matrix.fromUnity(lhs.toUnity() * rhs.toUnity());
			return Matrix.fromUnity(rhs.toUnity() * lhs.toUnity()); // TODO: Reverse rhs and lhs due to XNA v. Unity?
		}
		
		public static Matrix CreateRotationZ(float radians)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(UnityEngine.Vector3.zero, UnityEngine.Quaternion.AngleAxis(Mathf.Rad2Deg * radians, UnityEngine.Vector3.forward), UnityEngine.Vector3.one));
		}
		
		public static Matrix CreateScale(float scale)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity, new UnityEngine.Vector3(scale, scale, scale)));
		}
		
		public static Matrix CreateScale(float xScale, float yScale, float zScale)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity, new UnityEngine.Vector3(xScale, yScale, zScale)));
		}
		
		public static Matrix CreateScale(Vector3 scales)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity, new UnityEngine.Vector3(scales.X, scales.Y, scales.Z)));
		}
		
		public static Matrix CreateTranslation(Vector3 position)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(new UnityEngine.Vector3(position.X, position.Y, position.Z), UnityEngine.Quaternion.identity, UnityEngine.Vector3.one));
		}
		
		public static Matrix CreateTranslation(float xPosition, float yPosition, float zPosition)
		{
			return Matrix.fromUnity(UnityEngine.Matrix4x4.TRS(new UnityEngine.Vector3(xPosition, yPosition, zPosition), UnityEngine.Quaternion.identity, UnityEngine.Vector3.one));
		}
		
		public static Matrix Invert(Matrix matrix)
		{
			return Matrix.fromUnity(matrix.toUnity().inverse);
		}
		
		private static Matrix fromUnity(UnityEngine.Matrix4x4 mxUnity)
		{
			return new Matrix(mxUnity);
		}
		
		// TODO: Was private
		internal UnityEngine.Matrix4x4 toUnity()
		{
			return _mxUnity;
		}
	}
#endif

	public static class Mouse
	{
		public static MouseState GetState()
		{
			return new MouseState();
		}
	}

	public struct MouseState
	{
		private enum MouseButton
		{
			Left = 0, Right, Middle
		}

		public ButtonState LeftButton
		{
			get
			{
				return UnityEngine.Input.GetMouseButton((int)MouseButton.Left) ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public ButtonState MiddleButton
		{
			get
			{
				return UnityEngine.Input.GetMouseButton((int)MouseButton.Middle) ? ButtonState.Pressed : ButtonState.Released;
			}
		}
		
		public ButtonState RightButton
		{
			get
			{
				return UnityEngine.Input.GetMouseButton((int)MouseButton.Right) ? ButtonState.Pressed : ButtonState.Released;
			}
		}

		public int ScrollWheelValue
		{
			get
			{
				return 0; // TODO: Implement
			}
		}

		public int X
		{
			get
			{
				return (int)UnityEngine.Input.mousePosition.x;
			}
		}

		public int Y
		{
			get
			{
				return (Screen.height - (int)UnityEngine.Input.mousePosition.y);
			}
		}
	}

	public enum PlayerIndex
	{
		One, Two, Three, Four
	}

	public class Point
	{
		public int X { get; private set; }
		public int Y { get; private set; }

		public Point() { }

		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}
	
	public class RasterizerState
	{
		public bool ScissorTestEnable { get; set; } // TODO: Implement
	}

	public class Rectangle 
	{ 
		public int X { get { return (int)_rectUnity.x; } }
		public int Y { get { return (int)_rectUnity.y; } }
		public int Width { get { return (int)_rectUnity.width; } }
		public int Height { get { return (int)_rectUnity.height; } }
		public int Left { get { return (int)_rectUnity.x; } }
		public int Top { get { return (int)_rectUnity.y; } }
		public int Bottom { get { return (int)_rectUnity.yMax; } }

		private UnityEngine.Rect _rectUnity;

		public Rectangle(int x, int y, int width, int height)
		{
			_rectUnity = new Rect(x, y, width, height);
		}

		public Point Center
		{
			get
			{
				UnityEngine.Vector2 v2Center = _rectUnity.center;
				return new Point((int)v2Center.x, (int)v2Center.y);
			}
		}

		public bool Contains(Point pt)
		{
			return _rectUnity.Contains(new UnityEngine.Vector2(pt.X, pt.Y));
		}

		public bool Contains(int x, int y)
		{
			return _rectUnity.Contains(new UnityEngine.Vector2(x, y));
		}

		public Point Location { get { return new Point((int)_rectUnity.x, (int)_rectUnity.y); } }

		public void Offset(int offsetX, int offsetY)
		{
			_rectUnity.x += offsetX;
			_rectUnity.y += offsetY;
		}
	}
	
	public class SpriteBatch
	{
		public GraphicsDevice GraphicsDevice { get; private set; }

		private Matrix4x4 _savedMatrix;
		private bool _matrixIsSaved = false;

		public SpriteBatch(GraphicsDevice graphicsDevice)
		{
			this.GraphicsDevice = graphicsDevice;
		}

		public void Begin()
		{
			_matrixIsSaved = false;
		}

		public void Begin(SpriteSortMode spriteSortMode, object blendState, object samplerState, object depthStencilState, RasterizerState rasterizerState, object effect, Matrix matrix)
		{
			// TODO: Implement for other parameters
			_savedMatrix = GUI.matrix;
			_matrixIsSaved = true;

			GUI.matrix = matrix.toUnity();
		}

		private Rect getSourceRect(Texture2D texture, Rectangle sourceRectangle)
		{
			Rect sourceRect = new Rect((float)sourceRectangle.Left / (float)texture.width,
			                           (float)(texture.height - (sourceRectangle.Top + sourceRectangle.Height)) / (float)texture.height,
			                           (float)sourceRectangle.Width / (float)texture.width,
			                           (float)sourceRectangle.Height / (float)texture.height);
			return sourceRect;
		}

#if false
		public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
		{
			// TODO: Implement
		}
#endif
		
		public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color)
		{
			// TODO: Implement color
			Rect sourceRect = getSourceRect(texture, sourceRectangle);
			GUI.DrawTextureWithTexCoords(new Rect(destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Width, destinationRectangle.Height),
			                             texture,
			                             sourceRect);
		}

		public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
		{
			Vector3 originOffsetFromSourceRectangleW = new Vector3(origin.X * ((float)destinationRectangle.Width / (float)sourceRectangle.Width),
			                                                       origin.Y * ((float)destinationRectangle.Height / (float)sourceRectangle.Height), 
			                                                       0.0f);
			Vector3 originW = new Vector3(destinationRectangle.Left + originOffsetFromSourceRectangleW.X, 
			                              destinationRectangle.Top + originOffsetFromSourceRectangleW.Y, 
			                              0.0f);

			UnityEngine.Matrix4x4 matrixSaved = GUI.matrix;

			Matrix mx = Matrix.Identity;
			mx *= Matrix.CreateTranslation(-originW);
			mx *= Matrix.CreateRotationZ(rotation);
			mx *= Matrix.CreateTranslation(originW);
			mx *= Matrix.CreateTranslation(-originOffsetFromSourceRectangleW);

			GUI.matrix *= mx.toUnity();

			GUI.DrawTextureWithTexCoords(new Rect(destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Width, destinationRectangle.Height),
			                             texture,
			                             getSourceRect(texture, sourceRectangle));

			GUI.matrix = matrixSaved;
		}

		public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color)
		{
			// TODO: Implement correctly, including width, height, and color
			Vector2 textSize = spriteFont.MeasureString(text);
			GUI.Label(new Rect(position.X, position.Y, textSize.X + 10, textSize.Y + 10), text);
		}

		public void End()
		{
			// TODO: Implement
			if (_matrixIsSaved)
				GUI.matrix = _savedMatrix;
		}
	}

	public enum SpriteEffects
	{
		FlipHorizontally, FlipVertically, None
	}
	
	public class SpriteFont
	{
		private static GUIStyle _guiStyle = new GUIStyle();

		public Vector2 MeasureString(string text)
		{
			// TODO: Implement correctly
			UnityEngine.Vector2 v2TextSize = _guiStyle.CalcSize(new GUIContent(text));
			return new Vector2(v2TextSize.x, v2TextSize.y);
		}
	}

	public enum SpriteSortMode
	{
		BackToFront, Deferred, FrontToBack, Immediate, Texture
	}

#if false
	public class Texture2D // TODO: Remove.
	{
	}
#endif

	public struct Vector2
	{
		public float X { get { return _v2Unity.x; } set { _v2Unity.x = value; } }
		public float Y { get { return _v2Unity.y; } set { _v2Unity.y = value; } }

		private UnityEngine.Vector2 _v2Unity;

		public Vector2(float value)
		{
			_v2Unity = new UnityEngine.Vector2(value, value);
		}

		public Vector2(float x, float y)
		{
			_v2Unity = new UnityEngine.Vector2(x, y);
		}

		public override bool Equals(object obj)
		{
			return obj is Vector2 && this == (Vector2)obj;
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		public static bool operator ==(Vector2 a, Vector2 b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=(Vector2 a, Vector2 b)
		{
			return !(a == b);
		}

		public void Normalize()
		{
			UnityEngine.Vector2 v2Unity = this.toUnity();
			v2Unity.Normalize();
			this.X = v2Unity.x;
			this.Y = v2Unity.y;
		}

		public static Vector2 Transform(Vector2 position, Matrix matrix)
		{
			UnityEngine.Vector3 uv3 = matrix.toUnity().MultiplyPoint(new UnityEngine.Vector3(position.X, position.Y, 1.0f));
			return new Vector2(uv3.x, uv3.y);
		}

		public static float Distance(Vector2 a, Vector2 b)
		{
			return UnityEngine.Vector2.Distance(a.toUnity(), b.toUnity());
		}

		public static Vector2 UnitX { get { return Vector2.fromUnity(UnityEngine.Vector2.right); } }
		public static Vector2 UnitY { get { return Vector2.fromUnity(UnityEngine.Vector2.up); } }
		public static Vector2 Zero { get { return Vector2.fromUnity(UnityEngine.Vector2.zero); } }

		public static Vector2 operator *(Vector2 a, float d)
		{
			return Vector2.fromUnity(a.toUnity() * d);
		}
		
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return Vector2.fromUnity(a.toUnity() + b.toUnity());
		}
		
		public static Vector2 operator -(Vector2 a)
		{
			return Vector2.fromUnity(-(a.toUnity()));
		}

		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return Vector2.fromUnity(a.toUnity() - b.toUnity());
		}

		public static float DistanceSquared(Vector2 a, Vector2 b)
		{
			// DistanceSquared is sqrMagnitude of the distance vector.
			return (a.toUnity() - b.toUnity()).sqrMagnitude;
		}

		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return UnityEngine.Vector2.Dot(lhs.toUnity(), rhs.toUnity());
		}

		public static Vector2 Lerp(Vector2 from, Vector2 to, float t)
		{
			return Vector2.fromUnity(UnityEngine.Vector2.Lerp(from.toUnity(), to.toUnity(), t));
		}
		
		private static Vector2 fromUnity(UnityEngine.Vector2 v2Unity)
		{
			return new Vector2(v2Unity.x, v2Unity.y);
		}

		private UnityEngine.Vector2 toUnity()
		{
			return new UnityEngine.Vector2(this.X, this.Y);
		}
	}

	public struct Vector3
	{
		public float X { get { return _v3Unity.x; } set { _v3Unity.x = value; } }
		public float Y { get { return _v3Unity.y; } set { _v3Unity.y = value; } }
		public float Z { get { return _v3Unity.z; } set { _v3Unity.z = value; } }

		private UnityEngine.Vector3 _v3Unity;

		public Vector3(float value)
		{
			_v3Unity = new UnityEngine.Vector3(value, value, value);
		}

		public Vector3(float x, float y, float z)
		{
			_v3Unity = new UnityEngine.Vector3(x, y, z);
		}

		public override bool Equals(object obj)
		{
			return obj is Vector3 && this == (Vector3)obj;
		}
		
		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}
		
		public static bool operator ==(Vector3 a, Vector3 b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}
		
		public static bool operator !=(Vector3 a, Vector3 b)
		{
			return !(a == b);
		}

		public static Vector3 operator *(Vector3 a, Vector3 b)
		{
			return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
		}
		
		public static Vector3 operator /(Vector3 a, Vector3 b)
		{
			// Mathematically undefined, but XNA just divided the components.
			return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
		}
		
		public static Vector3 operator /(Vector3 a, float d)
		{
			return Vector3.fromUnity(a.toUnity() / d);
		}
		
		public static Vector3 operator -(Vector3 a)
		{
			return Vector3.fromUnity(-(a.toUnity()));
		}
		
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return Vector3.fromUnity(a.toUnity() - b.toUnity());
		}

		private static Vector3 fromUnity(UnityEngine.Vector3 v3Unity)
		{
			return new Vector3(v3Unity.x, v3Unity.y, v3Unity.z);
		}
		
		private UnityEngine.Vector3 toUnity()
		{
			return new UnityEngine.Vector3(this.X, this.Y, this.Z);
		}
	}
			
	public class Viewport
	{
		public Rectangle TitleSafeArea { get { return new Rectangle(0, 0, Screen.width, Screen.height); } }
	}
}
