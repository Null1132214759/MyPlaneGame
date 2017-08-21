using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using MyPlaneGame.Properties;
using System.Windows.Forms;

namespace MyPlaneGame
{
    /// <summary>
    /// 玩家飞机
    /// </summary>
    class PlaneHero:PlaneFather
    {
        private static Image imgHero = Resources.hero1;
        private SoundPlayer sp = new SoundPlayer(Resources.use_bomb1);
        private SoundPlayer sp1 = new SoundPlayer(Resources.bullet2);
        // 构造函数
        public PlaneHero(int x, int y, int speed, int life, Direction dir)
            : base(x, y, imgHero, speed, life, dir)
        { }

        // 重写Draw方法
        public override void Draw(Graphics g)
        {
            g.DrawImage(imgHero, this.X, this.Y, this.Width / 2, this.Height / 2);
        }

        // 发射子弹
        public void Fire()
        {
            SingleObject.GetSingle().AddGameObject(new BulletHero(this, 60, 1));
            // 播放子弹发射音效
            sp1.Play();
        }

        // 重写IsOver方法
        public override void IsOver()
        {
            // 播放玩家飞机爆炸图片
            SingleObject.GetSingle().AddGameObject(new BoomHero(this.X, this.Y));
            // 播放玩家飞机爆炸音效
            this.sp.Play();
            if (SingleObject.GetSingle().Score >= 100)
            {
                SingleObject.GetSingle().Score -= 50;
            }  
        }

        // 鼠标控制飞机移动
        public void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            this.X = e.X;
            this.Y = e.Y;
        }

        // 键盘控制飞机移动
        public void KeyMove(System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Up || e.KeyCode==Keys.W)
            {
                this.Y -= 20;
            }
            else if(e.KeyCode==Keys.Down || e.KeyCode==Keys.S)
            {
                this.Y += 20;
            }
            else if(e.KeyCode==Keys.Left || e.KeyCode==Keys.A)
            {
                this.X -= 20;
            }
            else if(e.KeyCode==Keys.Right || e.KeyCode==Keys.D)
            {
                this.X += 20;
            }
            // 判断对象是否超出窗体边界
            if (this.X <= 0)
            {
                this.X = 0;
            }
            if (this.X >= 380)
            {
                this.X = 380;
            }
            if (this.Y <= 0)
            {
                this.Y = 0;
            }
            if (this.Y >= 850)
            {
                this.Y = 640;
            }
        }
    }
}
