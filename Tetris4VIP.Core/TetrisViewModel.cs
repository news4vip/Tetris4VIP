using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris4VIP.Core
{
    public class TetrisViewModel
    {
        public Random rand;
        public Field field;
        public Block block;

        public TetrisViewModel()
        {
            rand = new Random();
            field = new Field();
            block = null;
        }

        public bool MoveLeft;
        public bool MoveRight;
        public bool MoveDown;
        public bool RollLeft;
        public bool RollRight;

        public bool GameLoop()
        {
            // --------------------------------------------------------
            if (field.IsGameOver())
            {
                return false;
            }

            // --------------------------------------------------------
            if (block == null)
            {
                block = new Block(rand.Next(Block.Blocks.Count - 1) + 1, field);
            }

            // --------------------------------------------------------
            if (MoveLeft)
            {
                if (!field.HitTest(block, -1, 0))
                {
                    block.X--;
                }
            }
            if (MoveRight)
            {
                if (!field.HitTest(block, +1, 0))
                {
                    block.X++;
                }
            }
            if (RollLeft)
            {
                if (!field.HitTest(block, RollTypes.Left))
                {
                    block.Roll(RollTypes.Left);
                }
            }
            if (RollRight)
            {
                if (!field.HitTest(block, RollTypes.Right))
                {
                    block.Roll(RollTypes.Right);
                }
            }

            // --------------------------------------------------------
            if (!field.HitTest(block, 0, +1))
            {
                block.Y++;
            }
            else
            {
                field.SetBlock(block);
                block = null;
            }

            // --------------------------------------------------------
            field.ClearLine();

            // --------------------------------------------------------
            MoveLeft  = false;
            MoveRight = false;
            MoveDown  = false;
            RollLeft  = false;
            RollRight = false;

            return true;
        }

        /// <summary>
        /// デバッグ用
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            int[,] v = field.Data.Clone() as int[,];
            if (block != null)
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        int dat = block.Data[x,y];
                        if (dat == 0)
                            continue;

                        int mx = block.X + x;
                        if (mx < 0 || field.W <= mx)
                            continue;
                    
                        int my = block.Y + y;
                        if (my < 0 || field.H <= my)
                            continue;
                    
                        v[mx,my] = block.Id;
                    }
                }
            }

            string s = "";
            for (int y = 0; y < field.H; y++)
            {
                for (int x = 0; x < field.W; x++)
                {
                    if (v[x,y] == 0)
                    {
                        s += "  ";
                    }
                    else
                    {
                        s += v[x,y] + " ";
                    }
                }
                s += "|";
                s += Environment.NewLine;
            }
            for (int x = 0; x < field.W; x++)
            {
                s += "--";
            }
            return s;
        }
    }
}
