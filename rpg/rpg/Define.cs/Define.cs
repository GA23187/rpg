using System.Drawing;


public static class Define
{

    public static void define(Player[] player, Npc[] npc, Map[] map)
    {
        Player.current_player = 0;//默认角色
        //设置角色属性
        player[0] = new Player();
        player[0].bitmap = new Bitmap(@"role/r1.png");
        player[0].bitmap.SetResolution(96, 96);
        player[0].is_active = 1;

        player[1] = new Player();
        player[1].bitmap = new Bitmap(@"role/r2.png");
        player[1].bitmap.SetResolution(96, 96);
        player[1].is_active = 1;

        player[2] = new Player();
        player[2].bitmap = new Bitmap(@"role/r2.png");
        player[2].bitmap.SetResolution(96, 96);
        player[2].is_active = 1;


        //map define
        map[0] = new Map();
        map[0].bitmap_path = "map/map1.jpg";
        map[0].shade_path = "map/map1_shade.png";
        map[0].block_path = "map/map1_block.png";
        map[0].music = "music/1.mp3";

        map[1] = new Map();
        map[1].bitmap_path = "map/map2.jpg";
        map[1].shade_path = "map/map2_shade.png";
        map[1].block_path = "map/map2_block.png";
        map[1].music = "music/3.mp3";

        //npc define
        npc[0] = new Npc();
        npc[0].map = 0;
        npc[0].x = 200;
        npc[0].y = 600;
        npc[0].bitmap_path = "role/npc1.png";

        npc[1] = new Npc();
        npc[1].map = 0;
        npc[1].x = 800;
        npc[1].y = 600;
        npc[1].bitmap_path = "role/npc2.png";

        npc[2] = new Npc();
        npc[2].map = 0;
        npc[2].x = 450;
        npc[2].y = 150;
        npc[2].region_x = 100;
        npc[2].region_y = 100;
        npc[2].collision_type = Npc.Collosion_type.ENTER;

        npc[3] = new Npc();
        npc[3].map = 1;
        npc[3].x = 100;
        npc[3].y = 150;
        npc[3].region_x = 40;
        npc[3].region_y = 40;
        npc[3].collision_type = Npc.Collosion_type.ENTER;

        npc[4] = new Npc();
        npc[4].map = 0;
        npc[4].x = 500;
        npc[4].y = 200;
        npc[4].bitmap_path = "role/npc3.png";
        npc[4].collision_type = Npc.Collosion_type.KEY;
        Animation npc4anm1 = new Animation();
        npc4anm1.bitmap_path = "role/anm1.png";
        npc4anm1.row = 2;
        npc4anm1.col = 2;
        npc4anm1.max_frame = 3;
        npc4anm1.anm_rate = 4;
        npc[4].anm = new Animation[1];
        npc[4].anm[0] = npc4anm1;

        npc[5] = new Npc();
        npc[5].map = 1;
        npc[5].x = 500;
        npc[5].y = 500;
        npc[5].bitmap_path = "role/npc4.png";
        npc[5].collision_type = Npc.Collosion_type.KEY;
        npc[5].npc_type = Npc.Npc_type.CHARACTER;
        npc[5].idle_walk_direction = Comm.Direction.LEFT;
        npc[5].idle_walk_time = 50;

        npc[6] = new Npc();
        npc[6].map = 0;
        npc[6].x = 800;
        npc[6].y = 1000;
        npc[6].region_x = 50;
        npc[6].region_y = 60;
        npc[6].x_offset = -15;
        npc[6].y_offset = -15;
        npc[6].mc_xoffset = 0;
        npc[6].mc_yoffset = 0;
        npc[6].mc_w = 90;
        npc[6].mc_h = 50;
        npc[6].bitmap_path = "role/npc_shoe.png";
        npc[6].collision_type = Npc.Collosion_type.KEY;

        npc[7] = new Npc();
        npc[7].map = 0;
        npc[7].x = 500;
        npc[7].y = 600;
        npc[7].bitmap_path = "role/npc2.png";
       


        //角色人物面板头像
        player[0].status_bitmap = new Bitmap(@"item/face1.png");
        player[0].status_bitmap.SetResolution(96,96);
        player[1].status_bitmap = new Bitmap(@"item/face2.png");
        player[1].status_bitmap.SetResolution(96, 96);
       // player[1].equip_att = 3;
        //player[1].equip_def = 4;
        player[2].status_bitmap = new Bitmap(@"item/face3.png");
        player[2].status_bitmap.SetResolution(96, 96);

        //item
        Item.item=new Item[6];

        Item.item[0] = new Item();
        Item.item[0].set("红药水","恢复少量hp","item/item1.png",1,30,0,0,0,0);
        Item.item[0].cost = 30;
        Item.item[0].use_event += new Item.Use_event(Item.add_hp);

        Item.item[1] = new Item();
        Item.item[1].set("蓝药水", "恢复少量mp", "item/item2.png", 1, 30, 0, 0, 0, 0);
        Item.item[1].use_event += new Item.Use_event(Item.add_mp);

        Item.item[2] = new Item();
        Item.item[2].set("短剑", "一把假的", "item/item3.png", 1, 1, 10, 0, 0, 5);
        Item.item[2].use_event += new Item.Use_event(Item.equip);

        Item.item[3] = new Item();
        Item.item[3].set("斧头", "666的斧头", "item/item4.png", 1, 1, 3, 0, 0, 50);
        Item.item[3].use_event += new Item.Use_event(Item.equip);

        Item.item[4] = new Item();
        Item.item[4].set("盾", "666的盾，没有人可以来来来\n他", "item/item5.png", 1, 2, 0, 20, 5, 0);
        Item.item[4].use_event += new Item.Use_event(Item.equip);

        Item.item[5] = new Item();
        Item.item[5].set("九阴真经", "一本的懂的书", "item/item6.png", 0, 30, 0, 0, 0, 0);
        Item.item[5].use_event += new Item.Use_event(Item.lpybook);

        Item.add_item(0,3);
        Item.add_item(1,3);
        Item.add_item(2, 2);
        Item.add_item(3, 1);
        Item.add_item(4, 1);
       // Item.add_item(5,1);

        //skill
        Skill.skill=new Skill[2];
        Skill.skill[0] = new Skill();
        Skill.skill[0].set("治疗术","恢复少量hp\nmp:20","item/skill1.png",20,20,0,0,0,0);
        Skill.skill[0].use_event += new Skill.Use_event(Skill.add_hp);

        Skill.skill[1] = new Skill();
        Skill.skill[1].set("黑洞术", "攻击技能，将敌人吸入漩涡hp\nmp:20", "item/skill2.png", 20, 20, 0, 0, 0, 0);
        Skill.skill[1].use_event += new Skill.Use_event(Skill.low_hp);

        Skill.learn_skill(0,0,1);
        Skill.learn_skill(0, 1, 1);
        Skill.learn_skill(1, 0, 1);
        Skill.learn_skill(2,1,1);

        //定义战斗
        Animation anm_att = new Animation();                        //攻击动画，6帧
        anm_att.bitmap_path = "fight/anm_att.png";
        anm_att.row = 3;
        anm_att.col = 2;
        anm_att.max_frame = 6;
        anm_att.anm_rate = 1;

        Animation anm_item = new Animation();                    //使用物品动画，3帧
        anm_item.bitmap_path = "fight/anm_item.png";
        anm_item.row = 3;
        anm_item.col = 1;
        anm_item.max_frame = 3;
        anm_item.anm_rate = 1;

        Animation anm_skill = new Animation();              //使用技能动画，4帧
        anm_skill.bitmap_path = "fight/anm_skill.png";
        anm_skill.row = 2;
        anm_skill.col = 2;
        anm_skill.max_frame = 4;
        anm_skill.anm_rate = 1;

        player[0].fset("主角","fight/p1.png",-120,-120,"fight/fm_face1.png",anm_att,anm_item,anm_skill);
        player[1].fset("辅助", "fight/p2.png", -120, -120, "fight/fm_face2.png", anm_att, anm_item, anm_skill);
        player[2].fset("辅助2", "fight/p3.png", -120, -120, "fight/fm_face3.png", anm_att, anm_item, anm_skill);

        //敌人设置
        Enemy.enemy=new Enemy[2];
        Enemy.enemy[0]=new Enemy();
        Enemy.enemy[0].set("老虎","fight/enemy.png",-160,-120,100,200,10,15,10,anm_att,anm_skill,new int[]{-1,-1,-1,-1,-1});

        Enemy.enemy[1] = new Enemy();
        Enemy.enemy[1].set("鞋精","fight/enemy2.png",-160,-120,300,40,10,15,10,anm_att,anm_skill,new int[]{-1,-1,1,1,1});

  
        //fight_item
       Animation anm_item0 = new Animation();//血药
        anm_item0.bitmap_path = "fight/anm_heal.png";
        anm_item0.row = 8;
        anm_item0.col = 1;
        anm_item0.max_frame = 4;
        anm_item0.anm_rate = 1;
        Item.item[0].fset(anm_item0,-50,20);

        Animation anm_item2 = new Animation(); //短剑
        anm_item2.bitmap_path = "fight/anm_sword.png";
        anm_item2.row = 4;
        anm_item2.col = 1;
        anm_item2.max_frame = 4;
        anm_item2.anm_rate = 1;
        Item.item[2].fset(anm_item2, 50, 20);

        //fight_skill
        Animation anm_skill0 = new Animation(); //治疗术
        anm_skill0.bitmap_path = "fight/anm_heal.png";
        anm_skill0.row = 4;
        anm_skill0.col = 1;
        anm_skill0.max_frame = 4;
        anm_skill0.anm_rate = 1;
        Skill.skill[0].fset(anm_skill0,-50,10);

        Animation anm_skill1 = new Animation();//黑暗术
        anm_skill1.bitmap_path = "fight/anm_drak.png";
        anm_skill1.row = 4;
        anm_skill1.col = 1;
        anm_skill1.max_frame = 4;
        anm_skill1.anm_rate = 1;
        Skill.skill[1].fset(anm_skill1, 60, 10);


        //定义任务
        //Task
        Task.task=new Task[100];
        //地图1
        Task.task[0] = new Task();
        Task.task[0].set(2, new Task.Task_event(Task.change_map), 1, 725, 250, 2);
        //地图2
        Task.task[1] = new Task();
        Task.task[1].set(3,new Task.Task_event(Task.change_map),0, 30, 500, 3);
        //陌生人见面
        Task.task[2] = new Task();
        Task.task[2].set(7,new Task.Task_event(Story.meet),0,0,Task.VARTYPE.ANY,0,1,Task.VARRESULT.ASSIGN);
        //陌生人催促
        Task.task[3] = new Task();
        Task.task[3].set(7,new Task.Task_event(Story.aftermeet),0,1,Task.VARTYPE.EQUAL,0,0,Task.VARRESULT.NOTHING);
        //陌生人给予奖励
        Task.task[4] = new Task();
        Task.task[4].set(7,new Task.Task_event(Story.reward),0,2,Task.VARTYPE.EQUAL,0,3,Task.VARRESULT.ASSIGN);
        //给予奖励之后
        Task.task[5] = new Task();
        Task.task[5].set(7,new Task.Task_event(Story.afterreward),0,3,Task.VARTYPE.EQUAL,0,0,Task.VARRESULT.NOTHING);
        //鞋子默认
        Task.task[6] = new Task();
        Task.task[6].set(6,new Task.Task_event(Story.shoe),0,0,Task.VARTYPE.ANY,0,0,Task.VARRESULT.NOTHING);
        //鞋子战斗
        Task.task[7] = new Task();
        Task.task[7].set(6,new Task.Task_event(Story.shoefight),0,1,Task.VARTYPE.EQUAL,0,0,Task.VARRESULT.NOTHING);
 
        //保存游戏
        Task.task[8] = new Task();
        Task.task[8].set(1,new Task.Task_event(Story.save));
        //商店                                                   Shop.show(new int[] {0,1,2,3,-1,-1,-1});  //商人所卖物品
        Task.task[9] = new Task();
        Task.task[9].set(0, new Task.Task_event(Story.shop));
        //npc动画
        Task.task[10] = new Task();
        Task.task[10].set(4, new Task.Task_event(Story.play_anm));
        //tip
        Task.task[11] = new Task();
        Task.task[11].set(5, new Task.Task_event(Story.tip));

    }


}