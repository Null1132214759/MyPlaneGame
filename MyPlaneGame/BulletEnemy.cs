using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MyPlaneGame.Properties;

namespace MyPlaneGame
{
    /// <summary>
    /// 敌方子弹类
    /// </summary>
    class BulletEnemy:BulletFather
    {
        // 存储敌方飞机子弹图片
        private static Image imgBulletEnemy1 = Resources.bullet11;
        private static Image imgBulletEnemy2 = Resources.bullet21;
        // 构造函数
        public BulletEnemy(PlaneFather pf,int type,int speed,int power)
            :base(pf,GetImg(type),speed,power)
        {
            
        }
        public static Image imgBulletEnemy
        {
            get;
            set;
        }
        // 获取敌方不同飞机不同子弹样式
        public static Image GetImg(int type)
        {
            if (type == 0 || type == 1)
            {
                imgBulletEnemy = imgBulletEnemy1;
            }
            else if(type==2)
            {
                imgBulletEnemy = imgBulletEnemy2;
            }
            return imgBulletEnemy;
        }
        // 重写Draw方法
        public override void Draw(Graphics g)
        {
            base.Move();
            g.DrawImage(imgBulletEnemy, this.X + 30, this.Y + 5, this.Width / 2, this.Height / 2);
        }
    }

}
