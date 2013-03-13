namespace Tetris4VIP.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Field
    {
        public int W { get; private set; }
        public int H { get; private set; }
        public int[,] Data;

        public Field(int w = 10, int h = 20)
        {
            this.W = w;
            this.H = h;
            this.Data = new int[W,H];
        }

        public virtual void SetBlock(Block block)
        {
            int w = this.W;
            int h = this.H;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int dat = block.Data[x,y];
                    if (dat == 0)
                        continue;

                    int mx = block.X + x;
                    if (mx < 0 || w <= mx)
                    {
                        continue;
                    }
                    
                    int my = block.Y + y;
                    if (my < 0 || h <= my)
                    {
                        continue;
                    }
                    
                    this.Data[mx,my] = block.Id;
                }
            }
        }

        public virtual bool HitTest(Block block, int dx, int dy)
        {
            return HitTest(block.Data, block.X + dx, block.Y + dy);
        }

        public virtual bool HitTest(Block block, RollTypes roll)
        {
            var move = block.Clone(roll);
            return HitTest(move.Data, move.X, move.Y);
        }

        public virtual bool HitTest(int[,] data, int vx, int vy)
        {
            int w = this.W;
            int h = this.H;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int dat = data[x,y];
                    if (dat == 0)
                    {
                        continue;
                    }

                    int mx = vx + x;
                    if (mx < 0)
                    {
                        return true;
                    }
                    if (w <= mx)
                    {
                        return true;
                    }
                    
                    int my = vy + y;
                    if (my < 0)
                    {
                        continue;
                    }
                    if (h <= my)
                    {
                        return true;
                    }
                    
                    if (this.Data[mx,my] != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual int ClearLine()
        {
            int count = 0;
            int w = this.W;
            int h = this.H;

            for (int y = 0; y < h; y++)
            {
                if (h <= y)
                {
                    continue;
                }

                bool isdown = true;
                for (int x = 0; x < w; x++)
                {
                    if (this.Data[x,y] == 0)
                    {
                        count++;
                        isdown = false;
                        break;
                    }
                }

                if (isdown)
                {
                    for (int y2 = y; 0 <= y2; y2--)
                    {
                        for (int x2 = 0; x2 < w; x2++)
                        {
                            if (y2 - 1 < 0)
                            {
                                this.Data[x2,y2] = 0;
                            }
                            else
                            {
                                this.Data[x2,y2] = this.Data[x2, y2 - 1];
                            }
                        }
                    }
                }
            }
            return count;
        }

        public virtual bool IsGameOver()
        {
            int w = this.W;
            for (int x = 0; x < w; x++)
            {
                if (this.Data[x, 0] != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
