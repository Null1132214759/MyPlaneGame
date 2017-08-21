using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MyPlaneGame
{
    /// <summary>
    /// 飞机子弹基类
    /// </summary>
    class BulletFather:GameObject
    {
        // 存储子弹图片
        private Image imgBullte;
        // 记录子弹威力
        public int Power
        {
            get;
            set;
        }
        // 构造函数
        public BulletFather(PlaneFather pf,Image img,int speed,int power)
            :base(pf.X+pf.Width/2-30,pf.Y+pf.Height/2-50,img.Width,img.Height,speed,0,pf.Dir)
        {
            this.imgBullte = img;
            this.Power = power;
        }
        // 重写Draw方法
        public override void Draw(Graphics g)
        {
            this.Move();
            g.DrawImage(imgBullte, this.X, this.Y, this.Width / 2, this.Height / 2);
        }
        // 重写Move方法
        public override void Move()
        {
            // 根据制定方向进行移动
            switch(Dir)
            {
                case Direction.Up:
                    this.Y -= Speed;
                    break;
                case Direction.Down:
                    this.Y += Speed*2;
                    break;
            }
            // 子弹超出边界处理
            if(this.Y<=0)
            {
                this.Y = -100;
                SingleObject.GetSingle().RemoveGameObject(this);
            }
            if(this.Y>=850)
            {
                this.Y = 1000;
                SingleObject.GetSingle().RemoveGameObject(this);
            }
        }
    }
}
