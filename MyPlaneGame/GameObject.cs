using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MyPlaneGame
{
    /// <summary>
    /// 抽象类：所有游戏对象基类，封装子类所共有的对象
    /// </summary>
    public abstract class GameObject
    {
        // 枚举类型方向，包含上下左右
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        #region 横纵坐标、宽度、高度、速度、生命值、方向
        public int X // x坐标
        {
            get;
            set;
        }
        public int Y // y坐标
        {
            get;
            set;
        }
        public int Width // 宽度
        {
            get;
            set;
        }
        public int Height // 高度
        {
            get;
            set;
        }
        public int Speed // 速度
        {
            get;
            set;
        }
        public int Life //生命值
        {
            get;
            set;
        }
        public Direction Dir // 移动方向
        {
            get;
            set;
        }
        #endregion

        // 构造函数
        public GameObject(int x,int y,int width,int height,int speed,int life,Direction dir)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
            this.Life = life;
            this.Dir = dir;
        }
        // 重载两个参数构造函数
        public GameObject(int x,int y)
        {

        }

        // 抽象方法：每个对象的绘制方式各不相同
        public abstract void Draw(Graphics g);

        // 为实现碰撞检测返回对象矩形
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }

        //虚方法:游戏对象移动方式不同。可进行override
        public virtual void Move()
        {
            // 根据移动方向进行选择
            switch(Dir)
            {
                case Direction.Up:
                    this.Y -= this.Speed;
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
                case Direction.Left:
                    this.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.X += this.Speed;
                    break;
            }
            // 判断对象是否超出窗体边界
            if(this.X<=0)
            {
                this.X = 0;
            }
            if(this.X>=480)
            {
                this.X = 460;
            }
            if(this.Y<=0)
            {
                this.Y = 0;
            }
            if(this.Y>=850)
            {
                this.Y=800;
            }
        }
    }
}
