using System.Drawing;
using System.Windows.Forms;

public class Button
{
    //位置
    public int x = 0;       //按钮的位置是相对于面板的
    public int y = 0;
    public int w = 0;
    public int h = 0;
    //图片
    public string b_nomal_path;       //普通态（键盘没有选中）
    public string b_select_path;       //选择态（键盘选中但没有单击）
    public string b_press_path;        //单击态（按钮被按下）
    Bitmap b_nomal;
    Bitmap b_select;
    Bitmap b_press;
    //状态
    public enum Status
    {
        NOMAL=1,
        SELECT=2,
        PRESS=3,
    }
    public Status status = Status.NOMAL;

    //键盘控制跳转
    public class Key_ctrl
    {
        public int up = -1;
        public int down = -1;
        public int left = -1;
        public int right = -1;
    }
    public Key_ctrl key_ctrl = new Key_ctrl();

    public void set(int x0, int y0, int w0, int h0, string nomal_path, string select_path, string press_path, int key_up, int key_down, int key_left, int key_right)
    {
        x = x0;
        y = y0;
        b_nomal_path = nomal_path;
        b_press_path = press_path;
        b_select_path = select_path;
        w = w0;
        h = h0;
        key_ctrl.up = key_up;
        key_ctrl.down = key_down;
        key_ctrl.left = key_left;
        key_ctrl.right = key_right;
    }

    //加载
    public void load()
    {
        //加载
        if (b_nomal_path != null && b_nomal_path != "")             //普通状态
        {
            b_nomal = new Bitmap(b_nomal_path);
            b_nomal.SetResolution(96,96);

            if (w <= 0)
                w = b_nomal.Width;
            if (h <= 0)
                h = b_nomal.Height;
        }
        if (b_select_path != null && b_select_path != "")                  //选择状态
        {
            b_select = new Bitmap(b_select_path);
            b_select.SetResolution(96,96);
        }
        if (b_press_path != null && b_press_path != "")                      //单击状态
        {
            b_press = new Bitmap(b_press_path);
            b_press.SetResolution(96,96);
        }
    }

    //绘图
    public void draw(Graphics g, int x_offset, int y_offset)       //偏移参数x_offset，y_offset对应面板坐标
    {                                                                                  
        if (status == Status.NOMAL && b_nomal != null)            //x,y是相对于面板的坐标
            g.DrawImage(b_nomal,x_offset+x,y_offset+y);
        if (status == Status.SELECT && b_select != null)
            g.DrawImage(b_select, x_offset + x, y_offset + y);
        if (status == Status.PRESS &&b_press != null)
            g.DrawImage(b_press, x_offset + x, y_offset + y);
    }

    public delegate void Click_event();                      //定义委托
    public event Click_event click_event;                     //定义变量
    public void click()
    {
        if (click_event != null)                                      //调用
            click_event();
    }

    //判断传入坐标是否在范围内
    public bool is_collision(int collision_x, int collision_y)
    {
        Rectangle rect = new Rectangle(x,y,w,h);
        return rect.Contains(collision_x,collision_y);
    }


}








public class Panel
{
    public static Panel panel = null;
    private static Player.Status last_player_status = Player.Status.WALK;  //玩家状态

    public int x;
    public int y;

    public string bitmap_path;
    public Bitmap bitmap;

    public Button[] button;
    public int default_button = 0;             //默认按钮
    public int cancel_button = -1;           //取消按钮
    public int current_button = 0;            //当前选中状态按钮
    public void set(int x0, int y0, string path, int default_button0, int cancel_button0)
    {
        x = x0;
        y = y0;
        bitmap_path = path;
        default_button = default_button0;
        cancel_button = cancel_button0;
    }

    public void init()
    {
        //背景图
        if (bitmap_path != null && bitmap_path != "")
        {
            bitmap = new Bitmap(bitmap_path);
            bitmap.SetResolution(96,96);
        }
        //按钮
        if(button!=null)
            for (int i = 0; i < button.Length; i++)
            {
                if (button[i] == null)
                    continue;

                button[i].load();
            }
    }

    public void show()     //激活界面
    {
        panel = this;
        current_button = default_button;
        set_button_status(Button.Status.SELECT);

        if (Player.status != Player.Status.PANEL)          //保存角色状态，并设置角色当前状态为panel
            last_player_status = Player.status;
        Player.status = Player.Status.PANEL;
    }
    public void hide()        //设置面板没有处于激活状态                  
    {
        panel = null;
        Player.status = last_player_status;                 //在hide中还原角色
    }
    public void set_button_status(Button.Status status)     //实现current—button以外按钮都设为普通
    {
        if (button != null)
        {
            for (int i = 0; i < button.Length; i++)
            {
                if (button[i] == null)
                    continue;
                button[i].status = Button.Status.NOMAL;              //设为普通
            }
            if (button[current_button] != null)
                button[current_button].status = status;                      //设置对应状态
        }
    }

    //二个重要的委托
    public delegate void Draw_event(Graphics g, int x_offset, int y_offset);
    public event Draw_event draw_event;
    public delegate void Drawbg_event(Graphics g, int x_offset, int y_offset);
    public event Drawbg_event drawbg_event;

    public void draw_me(Graphics g)
    {
        if (drawbg_event != null)
            drawbg_event(g,this.x,this.y);

        if (bitmap != null)
            g.DrawImage(bitmap ,x,y);

        if (draw_event != null)
            draw_event(g,this .x,this.y);

        if(button !=null)
            for (int i = 0; i < button.Length; i++)
            {
                if (button[i] == null)
                    continue;
                button[i].draw(g,x,y);
            }
    }
    public static void draw(Graphics g)
    {
        if (panel != null)
            panel.draw_me(g);
    }

    public static void key_ctrl(KeyEventArgs e)              //调用key_ctrl_me
    {
        if (panel != null)
            panel.key_ctrl_me(e);
    }
    public void key_ctrl_me(KeyEventArgs e)
    {
        if (button == null)
            return;
        Button btn = button[current_button];           //获取按钮
        if (btn == null)
            return;
        //方向
        int newindex = -1;                                    //按下方向键
        if (e.KeyCode == Keys.Up)
        {
            newindex = btn.key_ctrl.up;
        }
        else if (e.KeyCode == Keys.Down)
        {
            newindex = btn.key_ctrl.down;
        }
        else if (e.KeyCode == Keys.Left)
        {
            newindex = btn.key_ctrl.left;
        }
        else if (e.KeyCode == Keys.Right)
        {
            newindex = btn.key_ctrl.right;
        }

        if (newindex >= 0 && newindex < button.Length && button[newindex] != null)   //判断目标按钮是否有效
        {
            current_button = newindex;
            set_button_status(Button.Status.SELECT);
        }
        //确定
        if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
        {
            set_button_status(Button.Status.PRESS);
            btn.click();
        }
        //取消
        else if (e.KeyCode == Keys.Escape)
        {
            if (cancel_button >= 0 && cancel_button < button.Length)
                button[cancel_button].click();
        }
    }

    public static void mouse_move(MouseEventArgs e)
    {
        if (panel != null)
            panel.mouse_move_me(e);
    }

    public void mouse_move_me(MouseEventArgs e)
    {
        if(button!=null)
            for (int i = 0; i < button.Length; i++)                  //遍历按钮
            {
                if (button[i] == null)
                    continue;
                if (button[i].is_collision(e.X - x, e.Y - y))                         //是否发生碰撞
                {
                    current_button = i;
                    set_button_status(Button.Status.SELECT);                   //设置为选中
                    break;
                }
            }
    }


    //响应鼠标单击事件
    public static void mouse_click(MouseEventArgs e)
    {
        if(panel!=null)
        panel.mouse_click_me(e);
    }

    public void mouse_click_me(MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
            return;
        if(button!=null)
            for (int i = 0; i < button.Length; i++)
            {
                if (button[i] == null)
                    continue;

                if (button[i].is_collision(e.X - x, e.Y - y))
                {
                    current_button = i;
                    set_button_status(Button.Status.PRESS);
                    button[i].click();
                    break;
                }
            }

    }

}