using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MyPlaneGame.Properties;

namespace MyPlaneGame
{
    /// <summary>
    /// 游戏标题
    /// </summary>
    class GameTitle:GameObject
    {
        // 导入标题图片资源
        private static Image imgTitle = Resources.name;
        // 标题浮动边界标记
        private static bool isfinal = false;

        // 构造函数
        public GameTitle(int x,int y,int speed)
            :base(x,y,imgTitle.Width,imgTitle.Height,speed,0,Direction.Down)
        { }

        // 重写Draw方法
        public override void Draw(Graphics g)
        { 
            if(this.Y>=20 && !isfinal) // 实现标题向下浮动效果
            {
                this.Y += Speed;
                if(this.Y==100)
                {
                    isfinal = true;
                }
            }
            if(isfinal && this.Y<=100) // 实现标题向上浮动效果
            {
                this.Y -= Speed;
                if(this.Y==20)
                {
                    isfinal = false;
                }
            }
            g.DrawImage(imgTitle,this.X,this.Y);
        }
    }
}
