using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rpg
{
    public partial class Form1 : Form
    {
       public static Player[] player = new Player[3];
       public static Map[] map = new Map[2];
       public static Npc[] npc = new Npc[8];
       public static  WMPLib.WindowsMediaPlayer music_player = new WMPLib.WindowsMediaPlayer();
        public Form1()
        {
            InitializeComponent();
        }

       private void Draw()
        {
            //Bitmap bitmap = new Bitmap(@"r1.png");
            //bitmap.SetResolution(96,96);
           //创建在pictureBox1上的图像g1
            Graphics g1 = stage.CreateGraphics();
           //将图像画在内存上，并使g为pictureBox1上的图像
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);
            Graphics g = myBuffer.Graphics;
           //自定义绘图
            if (Fight.fighting == 0)
                Map.draw(map, player, npc, g, new Rectangle(0, 0, stage.Width, stage.Height));
            else
                Fight.draw(g);

            //Player.draw(player,g);
            if (Panel.panel != null)          //调用panel的绘图
                Panel.draw(g);
            draw_mouse(g);                      //绘制鼠标
           //显示图像并释放资源
            myBuffer.Render();
            myBuffer.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Player.key_ctrl(player,map,npc,e);
            if (Panel.panel != null)             //调用panel的键盘控制方法
                Panel.key_ctrl(e);
            //Draw();
        }

        public Bitmap mc_nomal;               //普通光标
        public Bitmap mc_event;                //事件光标
        public int mc_mod = 0;                   //0—nomal,1—event
        private void Form1_Load(object sender, EventArgs e)
        {
            //光标
            mc_nomal = new Bitmap(@"ui/mc_1.png");                                   
            mc_nomal.SetResolution(96,96);
            mc_event = new Bitmap(@"ui/mc_2.png");
            mc_event.SetResolution(96, 96);

            Define.define(player,npc,map);
            Map.change_map(map,player,npc,0,30,500,1,music_player);

 /*           Button b = new Button();
            b.click_event += new Button.Click_event(tryevent);
            b.click();
 */

            Title.init();
            Title.show();

            Message.init();
            StatusMenu.init();

            Shop.init();

           // Fight.start(new int[] {0,0,-1},"fight/f_scene.png",1,0,1,1,100);            //指定2个id=0的敌人
            Fight.init();
            Save.init();

        }


 /*       public void tryevent()
        {
            MessageBox.Show("车哈哈");
        }
*/

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Player.key_ctrl_up(player,e);
           // Draw();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Player.timer_logic(player,map);
            for (int i = 0; i < npc.Length; i++)            //遍历当前场景的npc
            {
                if (npc[i] == null)
                    continue;
                if (npc[i].map != Map.current_map)
                    continue;
                npc[i].timer_logic(map);                             //调用timer_logic
            }
 
                Draw();
        }

        private void stage_MouseClick(object sender, MouseEventArgs e)
        {
            Player.mouse_click(map,player,new Rectangle(0,0,stage.Width,stage.Height),e);
            Npc.mouse_click(map,player,npc,new Rectangle(0,0,stage.Width,stage.Height),e);
            if (Panel.panel != null)
                Panel.mouse_click(e);
        }

        private void stage_MouseMove(object sender, MouseEventArgs e)
        {
            if (Panel.panel != null)
                Panel.mouse_move(e);
            mc_mod = Npc.check_mouse_collision(map,player,npc,new Rectangle(0,0,stage.Width,stage.Height),e);
        }

        private void draw_mouse(Graphics g)
        {
            Point showpoint = stage.PointToClient(Cursor.Position);
            if (mc_mod == 0)
                g.DrawImage(mc_nomal, showpoint.X, showpoint.Y);
            else
                g.DrawImage(mc_event,showpoint.X,showpoint.Y);
        }

        private void stage_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void stage_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }

        private void changemod()
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = false;

            }
        }
        //降低刷新频率
        public static void set_timer_interval(int interval)
        {
            if (ActiveForm == null)
                return;
            ((Form1)(ActiveForm)).timer1.Interval = interval;
        }
        //解决死循环block（）
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
