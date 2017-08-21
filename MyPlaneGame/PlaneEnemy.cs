using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Media;
using MyPlaneGame.Properties;

namespace MyPlaneGame
{
    /// <summary>
    /// 敌方飞机
    /// </summary>
    class PlaneEnemy:PlaneFather
    {
        // 存储敌方三种飞机图片
        private static Image imgEnemy0 = Resources.enemy0;
        private static Image imgEnemy1 = Resources.enemy1;
        private static Image imgEnemy2 = Resources.enemy2;

        // 存放敌人爆炸音效
        private SoundPlayer sp = new SoundPlayer(Resources.enemy0_down1);

        // 构造函数
        public PlaneEnemy(int x, int y, int type)
            : base(x,y,GetImage(type),GetSpeed(type),GetLife(type),Direction.Down)
        {
            this.EnemyType = type;
        }

        // 敌方飞机类型
        public int EnemyType
        {
            get;
            set;
        }

        // 获取敌方不同飞机图片
        private static Image GetImage(int type)
        {
            switch(type)
            {
                case 0:
                    return imgEnemy0;
                case 1:
                    return imgEnemy1;
                case 2:
                    return imgEnemy2;
                default:
                    return null;
            }
        }

        // 获取敌方不同飞机速度
        private static int GetSpeed(int type)
        {
            Random rd = new Random();
            switch(type)
            {
                case 0:
                    return 7;
                case 1:
                    return 6;
                case 2:
                    return 5;
                default:
                    return 0;
            }
        }

        // 获取敌方不同飞机生命值
        private static int GetLife(int type)
        {
            
            switch(type)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 3:
                    return 3;
                default:
                    return 0;
            }
        }

        // 重写Draw方法
        public override void Draw(Graphics g)
        {
            // 根据敌方飞机类型选择绘图片
            switch(this.EnemyType)
            {
                case 0:
                    g.DrawImage(imgEnemy0, this.X, this.Y);
                    break;
                case 1:
                    g.DrawImage(imgEnemy1, this.X, this.Y);
                    break;
                case 2:
                    g.DrawImage(imgEnemy2, this.X, this.Y);
                    break;
            }
            this.Move();
        }

        // 重写Move方法
        public override void Move()
        {
            switch (Dir)
            {
                case Direction.Up:
                    this.Y -= Speed;
                    break;
                case Direction.Down:
                    this.Y += Speed;
                    break;
                case Direction.Left:
                    this.X -= Speed;
                    break;
                case Direction.Right:
                    this.X += Speed;
                    break;
            }
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
                // 当敌方飞机超过边界，销毁敌方飞机对象
                this.Y = 870;
                SingleObject.GetSingle().RemoveGameObject(this);
            }
            // 实现敌方小飞机在一定坐标区域左右移动
            Random rd = new Random();
            if (this.EnemyType == 0 && this.Y >= 300)
            {
                if(this.X>=0 && this.X<=240)
                {
                    this.X += rd.Next(0, 50);
                }
                else
                {
                    this.X -= rd.Next(0, 50);
                }
            }
            else   // 如果不是小飞机就加速前进
            {
                this.Speed += 0;
            }
            // 调整敌方飞机发射子弹概率
            if (rd.Next(0, 100) > 90)
            {
                this.Fire();
            }
        }

       // private static Random rd = new Random();

        // 敌方飞机发射子弹
        public void Fire()
        {
            // 根据不同飞机类型发射不同子弹
            if (this.EnemyType == 0)
            {
                SingleObject.GetSingle().AddGameObject(new BulletEnemy(this, this.EnemyType, 20, 1));
            }
            else if (this.EnemyType == 1)
            {
                SingleObject.GetSingle().AddGameObject(new BulletEnemy(this, this.EnemyType, 20, 2));
            }
            else
            {
                SingleObject.GetSingle().AddGameObject(new BulletEnemy(this, this.EnemyType, 20, 3));
            }
        }

        // 重写IsOver方法敌方飞机毁灭
        public override void IsOver()
        {
            if (this.Life <= 0)
            {
                // 1. 移除敌方飞机自身对象
                SingleObject.GetSingle().RemoveGameObject(this);
                // 2. 播放敌方飞机毁坏图片
                SingleObject.GetSingle().AddGameObject(new BoomEnemy(this.X, this.Y, this.EnemyType));
                // 3.播放敌方飞机爆炸音效
                sp.Play();
                // 根据不同敌方飞机类型增加分数
                switch (this.EnemyType)
                {
                    case 0:
                        SingleObject.GetSingle().Score += 100;
                        break;
                    case 1:
                        SingleObject.GetSingle().Score += 200;
                        break;
                    case 2:
                        SingleObject.GetSingle().Score += 300;
                        break;
                }
            }
        }
    }
}
