namespace Tetris4VIP.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Block
    {
        public static IList<int[,]> Blocks = new List<int[,]>() {
            new int[,] {
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0},
            },
            new int[,] {
                {0,1,0,0},
                {0,1,0,0},
                {0,1,0,0},
                {0,1,0,0},
            },
            new int[,] {
                {0,1,0,0},
                {0,1,0,0},
                {0,1,1,0},
                {0,0,0,0},
            },
            new int[,] {
                {0,1,0,0},
                {0,1,1,0},
                {0,0,1,0},
                {0,0,0,0},
            },
            new int[,] {
                {0,0,1,0},
                {0,1,1,0},
                {0,1,0,0},
                {0,0,0,0},
            },
            new int[,] {
                {0,0,0,0},
                {0,1,1,0},
                {0,1,1,0},
                {0,0,0,0},
            },
            new int[,] {
                {0,0,0,0},
                {0,1,0,0},
                {1,1,1,0},
                {0,0,0,0},
            },
        };

        public int[,] Data;
        public int Id;
        public int X = -2;
        public int Y = -2;

        public Block(int id = 0, Field field = null)
        {
            this.Id   = id;
            this.Data = Blocks[id].Clone() as int[,];
            if (field != null)
            {
                this.X = -2 + (field.W / 2);
                this.Y = -2;
            }
        }

        public Block Clone(RollTypes roll)
        {
            Block block = new Block();
            block.Id = this.Id;
            block.X = this.X;
            block.Y = this.Y;

            int x, y;

            if (roll == RollTypes.Left)
            {
                for (y = 0; y < 4; y++)
                {
                    for (x = 0; x < 4; x++)
                    {
                        block.Data[y, 3 - x] = this.Data[x, y];
                    }
                }
            }
            else 
            {
                for (y = 0; y < 4; y++)
                {
                    for (x = 0; x < 4; x++)
                    {
                        block.Data[3 - y, x] = this.Data[x, y];
                    }
                }
            }
            return block;
        }

        public void Roll(RollTypes roll)
        {
            var block = Clone(roll);
            this.Data = block.Data;
        }
    }
}
