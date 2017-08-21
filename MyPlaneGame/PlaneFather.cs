using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MyPlaneGame
{
    /// <summary>
    /// 抽象类：飞机对象的基类
    /// </summary>
    public abstract class PlaneFather : GameObject
    {
        private Image imgPlane; // 存储玩家或电脑飞机图片
        public PlaneFather(int x, int y, Image img, int speed, int life, Direction dir)
            :base(x, y, img.Width, img.Height, speed, life, dir)
        {
            imgPlane = img;
        }

        // 抽象方法 判断飞机是否死亡
       public abstract void IsOver();
    }
}
