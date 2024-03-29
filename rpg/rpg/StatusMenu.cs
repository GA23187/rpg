﻿using System.Drawing;
using rpg;

public class StatusMenu
{
    public static Panel status = new Panel();

    public static int menu = 0;           //0-物品面板  1-技能面板
    public static Bitmap bitmap_menu_item;
    public static Bitmap bitmap_menu_equp;

    public static int page = 1;
    public static int selnow = 1;                //指明当前选中的第几个物品或技能
    public static Bitmap bitmap_sel;

    public static void init()
    {
        bitmap_menu_item = new Bitmap(@"item/sbt2_1.png");
        bitmap_menu_item.SetResolution(96,96);
        bitmap_menu_equp = new Bitmap(@"item/sbt2_2.png");
        bitmap_menu_equp.SetResolution(96,96);
        bitmap_sel = new Bitmap(@"item/sbt7_2.png");
        bitmap_sel.SetResolution(96,96);


        Button equip_att = new Button();
        equip_att.set(41, 55, 0, 0, "item/sbt9_1.png", "item/sbt9_2.png", "item/sbt9_2.png", -1, -1, -1, -1);
        equip_att.click_event += new Button.Click_event(click_equip_att);

        Button equip_def = new Button();
        equip_def.set(41, 135, 0, 0, "item/sbt9_1.png", "item/sbt9_2.png", "item/sbt9_2.png", -1, -1, -1, -1);
        equip_def.click_event += new Button.Click_event(click_equip_def);

        Button next_player = new Button();
        next_player.set(305, 290, 0, 0, "item/sbt1_1.png", "item/sbt1_1.png", "item/sbt1_1.png", -1, -1, -1, -1);
        next_player.click_event += new Button.Click_event(click_next_player);

        Button item_menu = new Button();
        item_menu.set(679, 51, 0, 0, "item/sbt10.png", "item/sbt10.png", "item/sbt10.png", -1, -1, -1, -1);
        item_menu.click_event += new Button.Click_event(click_item_menu);

        Button skill_menu = new Button();
        skill_menu.set(679, 155, 0, 0, "item/sbt10.png", "item/sbt10.png", "item/sbt10.png", -1, -1, -1, -1);
        skill_menu.click_event += new Button.Click_event(click_skill_menu);

        Button previous_page = new Button();
        previous_page.set(372, 326, 0, 0, "item/sbt3_1.png", "item/sbt3_2.png", "item/sbt3_2.png", -1, -1, -1, -1);
        previous_page.click_event += new Button.Click_event(click_previous_page);

        Button next_page = new Button();
        next_page.set(590, 326, 0, 0, "item/sbt5_1.png", "item/sbt5_2.png", "item/sbt5_2.png", -1, -1, -1, -1);
        next_page.click_event += new Button.Click_event(click_next_page);

        Button use = new Button();
        use.set(480, 326, 0, 0, "item/sbt4_1.png", "item/sbt4_2.png", "item/sbt4_2.png", -1, -1, -1, -1);
        use.click_event += new Button.Click_event(click_use);

        Button close = new Button();
        close.set(660, 10, 0, 0, "item/sbt6_1.png", "item/sbt6_2.png", "item/sbt6_2.png", -1, -1, -1, -1);
        close.click_event += new Button.Click_event(click_close);

        Button sel1 = new Button();
        sel1.set(357, 38, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel1.click_event += new Button.Click_event(click_sel1);

        Button sel2 = new Button();
        sel2.set(357, 133, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel2.click_event += new Button.Click_event(click_sel2);

        Button sel3 = new Button();
        sel3.set(357, 226, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel3.click_event += new Button.Click_event(click_sel3);

        Button under = new Button();
        under.set(-100, -100, 2000, 2000, "", "", "", -1, -1, -1, -1);
        
        status.button=new Button[13];
        status.button[0] = equip_att;
        status.button[1] = equip_def;
        status.button[2] = next_player;
        status.button[3] = item_menu;
        status.button[4] = skill_menu;
        status.button[5] = previous_page;
        status.button[6] = next_page;
        status.button[7] = use;
        status.button[8] = close;
        status.button[9] = sel1;
        status.button[10] = sel2;
        status.button[11] = sel3;
        status.button[12] = under;
        status.set(40,25,"item/status_bg.png",2,8);
        status.draw_event += new Panel.Draw_event(draw);            //绘图
        status.init();


    }

    public static void show()
    {
        menu = 0;
        page = 1;
        status.show();
    }

    public static void click_equip_att()
    {
        Item.unequip(1);
    }
    public static void click_equip_def()
    {
        Item.unequip(2);
    }
    public static void click_next_player()
    {
        Player.select_player = Player.select_player + 1;
        for(int i=Player.select_player;i<Form1.player.Length;i++)
            if (Form1.player[i].is_active == 1)
            {
                Player.select_player = i;
                return;
            }
        for (int i = 0; i < Player.select_player; i++)
        {
            if (Form1.player[i].is_active == 1)
            {
                Player.select_player = i;
                return;
            }
        }
    }
    public static void click_item_menu()
    {
        page=1;
        selnow=1;
        StatusMenu.menu=0;
    }
    public static void click_skill_menu()
    {
        page = 1;
        selnow = 1;
        StatusMenu.menu = 1;
    }
    public static void click_previous_page()
    {
        page--;
        if (page < 1) page = 1;
    }
    public static void click_next_page()
    {
        page++;
    }
    public static void click_close()
    {
        status.hide();
    }
    public static void click_sel1()
    {
        selnow = 1;
    }
    public static void click_sel2()
    {
        selnow = 2;
    }
    public static void click_sel3()
    {
        selnow = 3;
    }

    //----------------------------------------------------
    //   （1）根据page和selnow计算出当前物品或技能的下标
    //    （2）调用物品或技能的使用事件。
    //     下标的计算：（1）定义默认物品id   index
    //                          ( 2 )定义count表示当前遍历到第几个有效物品
    //                          （3）遍历物品列表找到第（page-1）*3+selnow-1个有效物品，因为page从1开始，物品id从0开始
    //                                 所以（page-1）*3表示当前页第一个物品id,
    //                               （page-1）*3+selnow-1表示当前物品id
    //------------------------------------------------------
    public static void click_use()
    {
        //物品
        if (menu == 0)
        {
            int index = -1;
            for (int i = 0, count = 0; i < Item.item.Length; i++)
            {
                if (Item.item[i].num <= 0)
                    continue;
                count++;

                if (count <= (page - 1) * 3 + selnow - 1)
                    continue;
                index = i;
                break;
            }
            if (index >= 0)
            {
                Item.item[index].use();
            }
        }
            //0-技能
        else 
        {
            int index = -1;
            int[] pskill=Form1.player[Player.select_player].skill;
            for (int i = 0, count = 0; i < pskill.Length; i++)
            {
                if (pskill[i] < 0)
                    continue;
                count++;

                if (count <= (page - 1) * 3 + selnow - 1)
                    continue;
                index = i;
                break;
            }
            if (index >= 0)
            {
                Skill.skill[pskill[index]].use();
            }
        
        }
    }

    //-------------------------------------------------
    //                        绘图
    //-----------------------------------------------
    public static void draw(Graphics g, int x_offset, int y_offset)
    {
        //画角色状态
        Player p=Form1.player[Player.select_player];
        g.DrawImage(p.status_bitmap,x_offset+100,y_offset+100);
        //状态数字
        Font font = new Font("黑体",10);
        Brush brush = Brushes.Black;
        g.DrawString(p.hp.ToString(),font,brush,x_offset+102,y_offset+363,new StringFormat());
        g.DrawString(p.attack.ToString(), font, brush, x_offset + 102, y_offset + 383, new StringFormat());
        g.DrawString(p.fspeed.ToString(), font, brush, x_offset + 102, y_offset + 403, new StringFormat());
        g.DrawString(p.mp.ToString(), font, brush, x_offset + 246, y_offset + 363, new StringFormat());
        g.DrawString(p.defense.ToString(), font, brush, x_offset + 246, y_offset + 383, new StringFormat());
        g.DrawString(p.fortune.ToString(), font, brush, x_offset + 246, y_offset + 403, new StringFormat());

         //装备加成
        int value1 = 0;         //攻击
        int value2 = 0;        //防御
        int value3 = 0;       //速度
        int value4 = 0;       //运气

        if (p.equip_att>= 0)
        {
            value1 = Item.item[p.equip_att].value2;
            value2 = Item.item[p.equip_att].value3;
            value3 = Item.item[p.equip_att].value4;
            value4 = Item.item[p.equip_att].value5;
        }
        if (p.equip_def >= 0)
        {
            value1 += Item.item[p.equip_def].value2;
            value2 += Item.item[p.equip_def].value3;
            value3 += Item.item[p.equip_def].value4;
            value4 += Item.item[p.equip_def].value5;
        }
        Font font_eq = new Font("黑体",10);
        Brush brush_eq = Brushes.Red;
        if(value1 != 0)
            g.DrawString("+" + value1.ToString(), font_eq, brush_eq, x_offset + 120, y_offset + 383, new StringFormat());
        if(value2 != 0)
            g.DrawString("+" + value2.ToString(), font_eq, brush_eq, x_offset + 265, y_offset + 383, new StringFormat());
        if (value3 != 0)
            g.DrawString("+" + value3.ToString(), font_eq, brush_eq, x_offset + 120, y_offset + 403, new StringFormat());
        if (value4 != 0)
            g.DrawString("+" + value4.ToString(), font_eq, brush_eq, x_offset + 265, y_offset + 403, new StringFormat());

        //装备图标
        if (p.equip_att >= 0 && Item.item[p.equip_att].bitmap != null)
            g.DrawImage(Item.item[p.equip_att].bitmap,x_offset+41,y_offset+55);

        if (p.equip_def >= 0 && Item.item[p.equip_def].bitmap != null)
            g.DrawImage(Item.item[p.equip_def].bitmap, x_offset + 41, y_offset + 135);

        //绘制金钱
        Font font_m = new Font("黑体",16);
        Brush brush_m = Brushes.DarkOrange;
        g.DrawString(Player.money.ToString(),font_m,brush_m,x_offset+500,y_offset+390,new StringFormat());

        //绘制物品装备栏
        if (StatusMenu.menu == 0)
            g.DrawImage(bitmap_menu_item, x_offset + 679, y_offset + 51);
        else
            g.DrawImage(bitmap_menu_equp, x_offset + 679, y_offset + 51);

        //显示物品
        if (StatusMenu.menu == 0)
        {
            for(int i=0,count=0,showcount=0;i<Item.item.Length&&showcount<3;i++)
            {
                if(Item.item[i].num<=0)
                    continue;
                count++;

                if (count <= (page - 1) * 3)
                    continue;

                if (Item.item[i].bitmap != null)
                    g.DrawImage(Item.item[i].bitmap,x_offset+360,y_offset+48+showcount*96);
                Font font_n = new Font("黑体",12);
                Brush brush_n = Brushes.GreenYellow;
                g.DrawString(Item.item[i].name+"X"+Item.item[i].num.ToString(),font_n,brush_n,x_offset+440,y_offset+48+showcount*96,new StringFormat());
                Font font_d=new Font("黑体",10);
                Brush brush_d = Brushes.LawnGreen;
                g.DrawString(Item.item[i].description,font_d,brush_d,x_offset+440,y_offset+75+showcount*96,new StringFormat());
                showcount++;
            }
        }
        //显示技能
        else if (StatusMenu.menu == 1)
        {
            int[] pskill = Form1.player[Player.select_player].skill;
            for (int i = 0, count = 0, showcount = 0; i < pskill.Length && showcount < 3; i++)
            {
                if (pskill[i] < 0)
                    continue;
                count++;

                if (count <= (page - 1) * 3)
                    continue;

                if (Skill.skill[pskill[i]].bitmap != null)
                    g.DrawImage(Skill.skill[pskill[i]].bitmap, x_offset + 360, y_offset + 48 + showcount * 96);
                Font font_n = new Font("黑体", 12);
                Brush brush_n = Brushes.GreenYellow;
                g.DrawString(Skill.skill[pskill[i]].name, font_n, brush_n, x_offset + 440, y_offset + 48 + showcount * 96, new StringFormat());
                Font font_d = new Font("黑体", 10);
                Brush brush_d = Brushes.LawnGreen;
                g.DrawString(Skill.skill[pskill[i]].description, font_d, brush_d, x_offset + 440, y_offset + 75 + showcount * 96, new StringFormat());
                showcount++;

            }
        }

        //显示选择框
        g.DrawImage(StatusMenu.bitmap_sel,x_offset+350,y_offset+38+(selnow-1)*95);




    }
   

    
 
} 