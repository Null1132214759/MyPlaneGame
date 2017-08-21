using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Windows.Forms;
using MyPlaneGame.Properties;

namespace MyPlaneGame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            initialGame();
            button3.Text = "自动开火";
        }

        /// <summary>
        /// 初始化游戏
        /// </summary>
        public void initialGame()
        {
            // 初始化游戏背景
            SingleObject.GetSingle().AddGameObject(new GameBackground(0, -850, 1));
            // 初始化游戏标题
            SingleObject.GetSingle().AddGameObject(new GameTitle(20, 20, 1));
            // 初始化玩家飞机
            SingleObject.GetSingle().AddGameObject(new PlaneHero(this.Width / 2 - 50, this.Height * 3 / 4, 5, 3, 
                GameObject.Direction.Up));
            // 初始化敌方飞机
            this.initialPlaneEnemy();
        }

        // 判断游戏是否开始
        private static bool isStarted = false;
        private static bool mouse = false;
        private static bool keyboard = false;
        private static bool mousedown = true;
        private static bool initial = false;
        private static int time = 0;
        private static SoundPlayer sp = new SoundPlayer(Resources.button1);
        Random rd = new Random();

        // 初始化敌方飞机
        public void initialPlaneEnemy()
        {
            // 随机出现4架 0、1号敌方飞机
            for (int i = 0; i < 4; i++)
            {
                SingleObject.GetSingle().AddGameObject(new PlaneEnemy(rd.Next(0,this.Width), -400, rd.Next(0, 2)));
            }
            if(rd.Next(0,100)>=80)  // 80%几率出现敌方2号大飞机
            {
                SingleObject.GetSingle().AddGameObject(new PlaneEnemy(rd.Next(0, this.Width-80), -400, 2));
            }
        }

        private void 飞机大战_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            // 在加载时解决窗体重绘闪烁问题
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                | ControlStyles.AllPaintingInWmPaint, true);
            // 播放背景音乐
            PlayBcakgroundMusic();
        }

        // 当窗体重绘时发生此事件
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (!isStarted)
            {
                // 当窗体重绘时统一绘制初始界面
                SingleObject.GetSingle().DrawFirst(e.Graphics);
            }
            else
            {
                if (initial)
                {
                    // 在开始游戏后移除初始界面两个飞机控制选择按钮
                    this.Controls.Remove(button1);
                    this.Controls.Remove(button2);
                    initial = false;
                }
                // 绘制游戏对象
                SingleObject.GetSingle().DrawObject(e.Graphics);
                // 打印玩家分数到屏幕
                string score = SingleObject.GetSingle().Score.ToString();
                e.Graphics.DrawString(score, new Font("微软雅黑", 20, FontStyle.Bold), Brushes.Red, new Point(10, 20));
                // 判断玩家飞机是否自动开火
                if(!mousedown)
                {
                    SingleObject.GetSingle().hero.Fire();
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // 每50ms发生一次重绘事件
            this.Invalidate();
            if (isStarted)
            {
                // 若敌方飞机数量<=1,重新初始化敌方飞机
                int count = SingleObject.GetSingle().PlaneEnemyList.Count;
                if (count <= 1)
                {
                    initialPlaneEnemy();
                }
                // 每50ms进项一次碰撞检测
                SingleObject.GetSingle().MeetCheck();
            }
        }

        // 通过鼠标控制飞机方向
        private void button1_Click(object sender, EventArgs e)
        {
            isStarted = true;
            mouse = true;
            initial = true;
            sp.Play();
            timer2.Enabled = true;
        }
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ( mouse && !keyboard)
            {
                SingleObject.GetSingle().hero.MouseMove(e);
            }
        }

        // 通过键盘控制飞机方向
        private void button2_Click(object sender, EventArgs e)
        {
            isStarted = true;
            initial = true;
            keyboard = true;
            sp.Play();
            timer2.Enabled = true;
        }
     
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!mouse && keyboard )
            {
                SingleObject.GetSingle().hero.KeyMove(e);
            }
        }

        // 自动开火开关
        private void button3_Click(object sender, EventArgs e)
        {
            if(button3.Text=="自动开火")
            {
                button3.Text = "手动开火";
                mousedown = false;
            }
            else
            {
                button3.Text = "自动开火";
                mousedown = true;
            }
            sp.Play();
        }
        
        // 按下鼠标左键手动开火
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (isStarted && mousedown && e.Button == MouseButtons.Left)
            {
                SingleObject.GetSingle().hero.Fire();
            }
        }

        // 退出按钮
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否退出？", "提示消息框", MessageBoxButtons.YesNo);
            if(result==DialogResult.Yes)
            {
                Application.Exit();
            }
            sp.Play();
        }

        // 背景音乐
        private static void PlayBcakgroundMusic()
        {
            SoundPlayer sp = new SoundPlayer(Resources.game_music1);
            sp.Play();
        }

        // 游戏规定时间运行60s
        private void timer2_Tick(object sender, EventArgs e)
        {
            time++;
            if(time==60)
            {
                isStarted = false;
                SoundPlayer sp = new SoundPlayer(Resources.game_over1);
                sp.Play();
                MessageBox.Show("游戏结束！  游戏得分：" + SingleObject.GetSingle().Score);
                Application.Exit();
            }
        }

        // 覆盖默认的系统键处理方式,解决焦点处于窗体其他控件倒置方向键失效
        protected override bool ProcessDialogKey(Keys keyData)

        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
                return false;
            else
                return base.ProcessDialogKey(keyData);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            PlayBcakgroundMusic();
        }
    }
}
