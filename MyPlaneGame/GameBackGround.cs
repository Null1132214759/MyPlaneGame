using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MyPlaneGame.Properties;

namespace MyPlaneGame
{
    /// <summary>
    /// 游戏背景
    /// </summary>
    class GameBackground:GameObject
    {
        // 导入背景图片资源
        private static Image imgBackground = Resources.background;
        
        //构造函数
        public GameBackground(int x,int y,int speed)
            :base(x,y,imgBackground.Width,imgBackground.Height,speed,0,Direction.Down)
        {}

        // 重写Draw方法
        public override void Draw(Graphics g)
        {
            // 使图片不停向下移动
            this.Y += this.Speed;
            if(this.Y==0)
            {
                this.Y = -850;
            }
            g.DrawImage(imgBackground, this.X, this.Y);
        }
    }
}
