using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TicTacToe;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Board board;
    private AiPlayer robot;
    private bool isOver;
    private bool isXwon;
    private bool isOwon;
    private Texture2D XTexture;
    private Texture2D OTexture;
    private int CellSize = 100;
    private SpriteFont font;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        isOver = false;
        isOwon = false;
        isXwon = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        board = new Board();
        robot = new AiPlayer();
        XTexture = Content.Load<Texture2D>("X");
        OTexture = Content.Load<Texture2D>("nula");
        font = Content.Load<SpriteFont>("Arial");
        //XTexture.Width = 100;
        // TODO: use this.Content to load your game content here

    }

    protected override void Update(GameTime gameTime)
    {
        if (isOver) return;
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        if (board.IsFilled() == false)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                var position = Mouse.GetState().Position;
                Console.WriteLine($"Mouse clicked at X: {position.X}, Y: {position.Y}");
            }

            if (board.Current_player == 'X')
            {
                var player_pos = Mouse.GetState().Position;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    int row = player_pos.Y / 100;
                    int col = player_pos.X / 100;
                    if (board.MakeMove(row, col))
                    {
                        if (board.IsWinner(board.Current_player))
                        {
                            isOver = true;
                            isXwon = true;
                            Console.WriteLine("You won");
                            return;
                        }
                        board.Current_player = 'O';  // Зміна ходу
                    }
                }
            }
            else if (board.Current_player == 'O')
            {
                (int row, int col) = robot.GetMove(board);
                if (board.MakeMove(row, col))
                {
                    if (board.IsWinner(board.Current_player))
                    {
                        isOver = true;
                        isOwon = true;
                        Console.WriteLine("You losed");
                        return;
                    }
                    board.Current_player = 'X';  // Зміна ходу
                }
            }
        }
        else
        {
            isOver = true;
        }
        Console.WriteLine("Nobody is winner");
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        GraphicsDevice.Clear(new Color(249, 245, 255));

        // TODO: Add your drawing code here
        GraphicsDevice.Clear(new Color(249, 245, 255));

        _spriteBatch.Begin();
        if (isOver)
        {
            if (isXwon)
            {
                _spriteBatch.DrawString(font, "You are winner!", new Vector2(400, 10), Color.Red);
            }
            else if (isOwon)
            {
                _spriteBatch.DrawString(font, "You are loser!", new Vector2(400, 10), Color.Red);
            }
            else
            {
                _spriteBatch.DrawString(font, "Nobody is winner!", new Vector2(400, 10), Color.Red);
            }
        }
        Texture2D gridTexture = new Texture2D(GraphicsDevice, 1, 1);
        gridTexture.SetData(new[] { Color.Black });

        Vector2 scale = new Vector2((float)CellSize / XTexture.Width, (float)CellSize / XTexture.Height);
        // Малювання сітки
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Vector2 position = new Vector2(j * CellSize, i * CellSize);
                if (board.GameBoard[i, j] == 'X')
                {
                    _spriteBatch.Draw(XTexture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
                else if (board.GameBoard[i, j] == 'O')
                {
                    _spriteBatch.Draw(OTexture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
        }
        _spriteBatch.Draw(gridTexture, new Rectangle(100, 0, 1, 300), Color.Black); // Вертикальна лінія
        _spriteBatch.Draw(gridTexture, new Rectangle(200, 0, 1, 300), Color.Black);
        _spriteBatch.Draw(gridTexture, new Rectangle(0, 100, 300, 1), Color.Black); // Горизонтальна лінія
        _spriteBatch.Draw(gridTexture, new Rectangle(0, 200, 300, 1), Color.Black);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
