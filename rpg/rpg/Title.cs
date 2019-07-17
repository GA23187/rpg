using System.Windows.Forms;
using System.Drawing;
using rpg;

public static class Title
{
    public static Panel title = new Panel();
    public static Panel confirm = new Panel();    //确认界面
    public static string title_music = "music/2.mp3";

    public static void init()
    {
        //主界面
        Button btn_new = new Button();                                                     //开始游戏按钮
        btn_new.set(400,200,0,0,"ui/T_start_1.png","ui/T_start_2.png","ui/T_start_2.png",2,1,-1,-1);
        btn_new.click_event += new Button.Click_event(newgame);

        Button btn_load = new Button();                                                   //读取游戏按钮
        btn_load.set(400, 250, 0, 0, "ui/T_load_1.png", "ui/T_load_2.png", "ui/T_load_2.png", 0, 2, -1, -1);
        btn_load.click_event += new Button.Click_event(loadgame);

        Button btn_exit = new Button();                                                   //退出游戏按钮
        btn_exit.set(400, 300, 0, 0, "ui/T_exit_1.png", "ui/T_exit_2.png", "ui/T_exit_2.png", 1, 0, -1, -1);
        btn_exit.click_event += new Button.Click_event(exitgame);

        title.button = new Button[3];                                    //给title添加前面定义的3个按钮
        title.button[0] = btn_new;
        title.button[1] = btn_load;
        title.button[2] = btn_exit;
        title.set(0,0,"ui/T_bg1.png",0,-1);                                //set
        title.init();                                                            //初始化

        //动态开始界面变量设置
        bg_1.SetResolution(96,96);
        bg_2.SetResolution(96, 96);
        bg_3.SetResolution(96, 96);
        bg_font.SetResolution(96,96);
        title.draw_event += new Panel.Draw_event(drawtitle);         //drawtitle方法

        //退出游戏询问框
        Button btn_yes = new Button();                                //确定按钮
        btn_yes.set(47, 75, 0, 0, "ui/confirm_yes_1.png", "ui/confirm_yes_2.png", "ui/confirm_yes_2.png",-1,1,-1,-1);
        btn_yes.click_event += new Button.Click_event(confirm_yes);

        Button btn_no = new Button();                                //取消按钮
        btn_no.set(42, 120, 0, 0, "ui/confirm_no_1.png", "ui/confirm_no_2.png", "ui/confirm_no_2.png", 0, -1, -1, -1);
        btn_no.click_event += new Button.Click_event(confirm_no);

        confirm.button = new Button[2];
        confirm.button[0] = btn_yes;
        confirm.button[1] = btn_no;
        confirm.set(340,250,"ui/confirm_bg.png",0,1);                   //询问面板设置
        confirm.init();
        confirm.drawbg_event += new Panel.Drawbg_event(drawconfirm);    //回调函数drawconfirm
    }

    //新游戏的回调函数
    public static void newgame()
    {
        Define.define(Form1.player,Form1.npc,Form1.map);
        Map.change_map(Form1.map, Form1.player, Form1.npc, 0, 30, 500, 1,Form1.music_player);
        title.hide();
    }

    //读取进度的回调函数
    public static void loadgame()
    {
        //MessageBox.Show("loadgame");
        Save.show(1);
    }

    //退出游戏的回调函数
    public static void exitgame()
    {
        confirm.show();
    }

    //显示title面板和设置音乐
    public static void show()
    {
        Form1.music_player.URL = title_music;
        title.show();
    }

    public static void confirm_yes()
    {
        Application.Exit();
    }

    public static void confirm_no()
    {
        title.show();
    }

    public static Bitmap bg_1 = new Bitmap("ui/T_bg1.png");
    public static Bitmap bg_2 = new Bitmap("ui/T_bg2.png");
    public static Bitmap bg_3 = new Bitmap("ui/T_bg3.png");
    public static Bitmap bg_font = new Bitmap("ui/T_logo.png");                      
    public static long last_change_bg_time = 0;                                            //记录上次换图片的时间
    public static int bg_now = 2;                                                                //记录当前显示的是哪张图

    public static void drawtitle(Graphics g, int x_offset, int y_offset)
    {
        //绘制背景
        if (bg_now == 0)
            g.DrawImage(bg_1, 0, 0);
        else if (bg_now == 1)
            g.DrawImage(bg_2, 0, 0);
        else if (bg_now == 2)
            g.DrawImage(bg_3,0,0);
        //绘制logo
        g.DrawImage(bg_font,320,80);
        //背景处理
        if (Comm.Time() - last_change_bg_time > 5000)
        {
            bg_now = bg_now + 1;
            if (bg_now > 2) bg_now = 0;
            last_change_bg_time = Comm.Time();
        }
    }
    public static void drawconfirm(Graphics g, int x_offset, int y_offset)
    {
        title.draw_me(g);
    }
}